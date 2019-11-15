using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenFinal.Models;
using ExamenFinal.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExamenFinal.Controllers
{
    public class FacturadorController : Controller
    {
        private readonly ApplicationDbContext context;

        public FacturadorController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View(ListarFacturas());
        }

            public JsonResult BuscarProducto(string nombre)
            {
                return Json(ListarNombresProductos(nombre));
            }

            public ActionResult Registrar()
            {
                ViewData["IdProducto"] = new SelectList(context.Producto, "IdNombre", "Nombre");
                return View(new FacturaViewModel());
            }

            [HttpPost]
            public ActionResult Registrar(FacturaViewModel model, string action)
            {
                if (action == "generar")
                {
                    if (!string.IsNullOrEmpty(model.CabeceraIdCliente.ToString()))
                {
                        if (RegistrarFactura(model.ToModel()))
                        {
                            return Redirect("~/");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("cliente", "Debe agregar un cliente para el comprobante");
                    }
                }
                else if (action == "agregar_producto")
                {
                    // Si no ha pasado nuestra validación, mostramos el mensaje personalizado de error
                    if (!model.SeAgregoUnProductoValido())
                    {
                        ModelState.AddModelError("producto_agregar", "Solo puede agregar un producto válido al detalle");
                    }
                    else if (model.ExisteEnDetalle(model.CabeceraIdProducto))
                    {
                        ModelState.AddModelError("producto_agregar", "El producto elegido ya existe en el detalle");
                    }
                    else
                    {
                        model.AgregarItemADetalle();
                    }
                }
                else if (action == "retirar_producto")
                {
                    model.RetirarItemDeDetalle();
                }
                else
                {
                    throw new Exception("Acción no definida ..");
                }

                return View(model);
            }

            public string ProductosJSON(int id)
            {
                var query = from p in context.Producto
                            where p.IdProducto == id
                            select new
                            {
                                Id = p.IdProducto,
                                Nombre = p.nombreProducto,
                                Precio = p.PrecioVenta
                            };

                return Newtonsoft.Json.JsonConvert.SerializeObject(query);
            }


            #region NoAction
            public bool RegistrarFactura(Factura factura)
            {
                try
                {
                context.Entry(factura).State = EntityState.Added;
                context.AddRange(factura.DetalleFactura);
                // context.Factura.AddRange(ventas.Factura);
                context.SaveChanges();
            }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }

            public List<Factura> ListarFacturas()
            {
                return context.Factura.OrderByDescending(x => x.Fecha)
                                          .ToList();
            }
            public List<Producto> ListarNombresProductos(string nombre)
            {
                var producto = context.Producto.OrderBy(x => x.nombreProducto)
                                        .Where(x => x.nombreProducto.Contains(nombre))
                                        .Take(10)
                                        .ToList();
                return producto;
            }
            #endregion

        }
    }