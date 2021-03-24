using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeGastos.Tests
{
    [TestClass]
    public class TestesRepositorioGasto : MongoDBInfraBaseTests
    {
        private RepositorioGasto repositorioGasto;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            repositorioGasto = new RepositorioGasto(mongoDB);
        }

        [TestMethod]
        public async Task Gastos_Fora_Do_Intervalo_Informado_Nao_Devem_Ser_Considerados()
        {
            string idUsuario = Guid.NewGuid().ToString();

            collectionGastos.InsertMany(new List<Gasto>()
            {
                new Gasto() { IdUsuario = idUsuario, Valor = 10, Data = DateTime.Today.AddDays(-20) },
                new Gasto() { IdUsuario = idUsuario, Valor = 11, Data = DateTime.Today.AddDays(-8) },
                new Gasto() { IdUsuario = idUsuario, Valor = 12, Data = DateTime.Today.AddDays(-7) },
                new Gasto() { IdUsuario = idUsuario, Valor = 13, Data = DateTime.Today }
            });

            var dataInicialFiltro = DateTime.Today.AddDays(-10);
            var dataFinalFiltro = DateTime.Today.AddDays(-5);

            var gastos = await repositorioGasto.BuscaGastosPeriodo(idUsuario, dataInicialFiltro, dataFinalFiltro);

            Assert.AreEqual(gastos.Count, 2, "Apenas 2 gastos deveriam ser considerados");

            var gasto11Reais = gastos.FirstOrDefault(g => g.Valor == 11);
            Assert.IsNotNull(gasto11Reais, "O gasto de 11 reais deveria ser retornado");
            var gasto12Reais = gastos.FirstOrDefault(g => g.Valor == 12);
            Assert.IsNotNull(gasto12Reais, "O gasto de 12 reais deveria ser retornado");
        }
    }
}