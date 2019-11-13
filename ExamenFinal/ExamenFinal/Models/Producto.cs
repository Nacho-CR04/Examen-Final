using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenFinal.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public int idProveedor { get; set; }
        public int IdCategoria { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Existencia { get; set; }
        public int NivelNuevoPedido { get; set; }
        public bool Suspendido  { get; set; }

        public Producto()
        {
            DetalleFactura = new List<DetalleFactura>();
        }
        public virtual List<DetalleFactura> DetalleFactura { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
