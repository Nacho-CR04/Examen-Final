using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenFinal.Models
{
    public class Factura
    {
        public Factura()
        {
            DetalleFactura = new List<DetalleFactura>();
        }
        [Key]
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public decimal SubTotal { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }



        public virtual Empleado Empleado { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual List<DetalleFactura> DetalleFactura { get; set; }
    }

    public class FacturaViewModel
    {
        #region Cabecera
        public int CabeceraIdCliente { get; set; }
        public string CabeceraNombreCliente { get; set; }
        public int CabeceraIdProducto { get; set; }
        public string CabeceraNombreProducto { get; set; }
        public int CabeceraCantidadProducto { get; set; }
        public decimal CabeceraPrecioProducto { get; set; }
        public string CabeceraIdVendedor { get; set; }
        #endregion

        #region Contenido
        public List<DetalleFacturaViewModel> DetalleFactura { get; set; }
        #endregion


        #region Pie
        public decimal Total()
        {
            return DetalleFactura.Sum(x => x.Total());
        }
        public DateTime Creado { get; set; }
        #endregion


        public FacturaViewModel()
        {
            DetalleFactura = new List<DetalleFacturaViewModel>();
            Refrescar();
        }

        public void Refrescar()
        {
            CabeceraIdProducto = 0;
            CabeceraNombreProducto = null;
            CabeceraCantidadProducto = 1;
            CabeceraPrecioProducto = 0;
        }

        public bool SeAgregoUnProductoValido()
        {
            return !(CabeceraIdProducto == 0 || string.IsNullOrEmpty(CabeceraNombreProducto) || CabeceraCantidadProducto == 0 || CabeceraPrecioProducto == 0);
        }

        public bool ExisteEnDetalle(int IdProducto)
        {
            return DetalleFactura.Any(x => x.IdProducto == IdProducto);
        }

        public void RetirarItemDeDetalle()
        {
            if (DetalleFactura.Count > 0)
            {
                var detalleARetirar = DetalleFactura.Where(x => x.Retirar)
                                                        .SingleOrDefault();

                DetalleFactura.Remove(detalleARetirar);
            }
        }

        public void AgregarItemADetalle()
        {
            DetalleFactura.Add(new DetalleFacturaViewModel
            {
                IdProducto = CabeceraIdProducto,
                NombreProducto = CabeceraNombreProducto,
                PrecioUnitario = CabeceraPrecioProducto,
                Cantidad = CabeceraCantidadProducto,
            });

            Refrescar();
        }

        public Factura ToModel()
        {
            var Factura = new Factura();
            //Factura.Cliente = this.CabeceraIdCliente;
            Factura.Fecha = DateTime.Now;
            Factura.SubTotal = this.Total();

            foreach (var d in DetalleFactura)
            {
                Factura.DetalleFactura.Add(new DetalleFactura
                {
                    IdProducto = d.IdProducto,
                    Total = d.Total(),
                    Precio = d.PrecioUnitario,
                    Cantidad = d.Cantidad
                });
            }

            return Factura;
        }

    }
}
