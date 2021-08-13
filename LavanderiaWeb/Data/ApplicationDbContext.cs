using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LavanderiaWeb.Models;
using LavanderiaWeb.Utils;

namespace LavanderiaWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options){
            
        }
        public DbSet<Producto> Productos{get; set;}
        public DbSet<Categoria> Categorias{get; set;}
    }
}
