
using Bernal.ERP.ControleInventario.Dominio.Entidades;
using Bernal.ERP.ControleInventario.Infra.Configuracoes;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bernal.ERP.ControleInventario.Infra
{
   public class DataContext: DbContext 
    {

      

        public DataContext(DbContextOptions<DataContext> contextOptions) : base(contextOptions)
        {
            
        }

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.ApplyConfiguration(new InventarioConfiguracao());
            modelBuilder.ApplyConfiguration(new InventarioDispositivoConfiguracao());
            modelBuilder.ApplyConfiguration(new InventarioItemConfiguracao());
            modelBuilder.ApplyConfiguration(new ContagemConfiguracao());
            modelBuilder.Entity<Inventario>().HasQueryFilter(p => !p.ExcluidoEm.HasValue);
            modelBuilder.Entity<InventarioItem>().HasQueryFilter(p => !p.ExcluidoEm.HasValue);
            modelBuilder.Entity<InventarioDispositivo>().HasQueryFilter(p => !p.ExcluidoEm.HasValue);
        }

    
    }
}
