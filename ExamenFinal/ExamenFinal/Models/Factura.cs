using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenFinal.Models
{
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public decimal SubTotal { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }


        public Factura()
        {
            DetalleFactura = new List<DetalleFactura>();
        }
        public virtual Empleado Empleado { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual List<DetalleFactura> DetalleFactura { get; set; }
    }
}
