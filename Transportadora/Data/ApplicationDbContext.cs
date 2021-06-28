using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Transportadora.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "c", Name = "cliente", NormalizedName = "cliente" }, 
                                                   new IdentityRole { Id = "f", Name = "funcionario", NormalizedName = "funcionario" });
        }

        // adicionar as 'tabelas' à BD
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Encomendas> Encomendas { get; set; }
        public DbSet<Envios> Envios { get; set; }
        public DbSet<Funcionarios> Funcionarios { get; set; }






    }
}
