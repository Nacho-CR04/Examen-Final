using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenFinal.Models
{
    public class DetalleFactura
    {
        [Key]
        public int IdDetalle { get; set; }
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }
        public virtual Factura Factura { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
