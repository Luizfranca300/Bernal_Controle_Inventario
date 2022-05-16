

using Bernal.ERP.ControleInventario.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bernal.ERP.ControleInventario.Infra.Configuracoes
{
    public class InventarioConfiguracao : IEntityTypeConfiguration<Inventario>
    {
        public void Configure(EntityTypeBuilder<Inventario> builder)
        {
    
            builder.ToTable("inventarios");
            builder.HasKey(b => b.InventarioId);

            builder
               .Property(b => b.InventarioId)
               .HasColumnName("inventario_id")
               .IsRequired();
            builder
              .Property(b => b.IniciadoEm)
              .HasColumnName("iniciado_em")
              .IsRequired();
            builder
              .Property(b => b.FinalizadoEm)
              .HasColumnName("finalizado_em")
              .IsRequired(false);

            builder
             .Property(b => b.ExcluidoEm)
             .HasColumnName("excluido_em")
             .IsRequired(false);

            builder
                .HasMany(b => b.InventarioItems)
                .WithOne(b => b.Inventario)
                .HasForeignKey(b => b.InventarioId);

            builder
                .HasMany(b => b.InventarioDispositivos)
                .WithOne(b => b.Inventario)
                .HasForeignKey(b => b.InventarioId);
    
        
        }
    }

}
