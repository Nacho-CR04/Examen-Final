using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenFinal.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public Cliente()
        {
            Factura = new List<Factura>();
        }
        public virtual List<Factura> Factura { get; set; }
    }
}
