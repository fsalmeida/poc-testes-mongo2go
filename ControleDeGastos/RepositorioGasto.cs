using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeGastos
{
    public class RepositorioGasto
    {
        public IMongoCollection<Gasto> MongoCollection { get; }

        public RepositorioGasto(IMongoDatabase mongoDatabase) => MongoCollection = mongoDatabase.GetCollection<Gasto>(nameof(Gasto));

        public async Task<List<Gasto>> BuscaGastosPeriodo(string idUsuario, DateTime dataInicial, DateTime dataFinal)
        {
            var filter = Builders<Gasto>.Filter.Eq(x => x.IdUsuario, idUsuario)
                       & Builders<Gasto>.Filter.Gte(x => x.Data, dataInicial)
                       & Builders<Gasto>.Filter.Lte(x => x.Data, dataFinal);

            return await MongoCollection.Find(filter).ToListAsync();
        }
    }
}