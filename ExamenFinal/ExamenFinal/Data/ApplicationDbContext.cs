using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExamenFinal.Models;

namespace ExamenFinal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ExamenFinal.Models.Categoria> Categoria { get; set; }
        public DbSet<ExamenFinal.Models.Cliente> Cliente { get; set; }
        public DbSet<ExamenFinal.Models.Empleado> Empleado { get; set; }
        public DbSet<ExamenFinal.Models.Producto> Producto { get; set; }
        public DbSet<ExamenFinal.Models.Proveedor> Proveedor { get; set; }
    }
}
