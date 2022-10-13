using System;

namespace MarzanStored.Models
{
    public class Orden
    {
        public int Id { get; set; }
        public string Clientes { get; set; }
        public int IdProductos { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}
