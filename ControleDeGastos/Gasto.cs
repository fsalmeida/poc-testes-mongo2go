using System;

namespace ControleDeGastos
{
    public class Gasto
    {
        public Gasto() => Id = Guid.NewGuid().ToString();

        public string Id { get; set; }
        public string IdUsuario { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
    }
}