using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenFinal.Models
{
    public class Proveedor
    {
        [Key]
        public int idProveedor { get; set; }
        public string Empresa { get; set; }
        public string NombreContacto { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string SitioWeb { get; set; }

        public Proveedor()
        {
            Producto = new List<Producto>();
        }
        public virtual List<Producto> Producto { get; set; }
    }
}
