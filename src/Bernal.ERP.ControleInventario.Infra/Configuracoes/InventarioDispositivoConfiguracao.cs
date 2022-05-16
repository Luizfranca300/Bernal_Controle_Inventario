

using Bernal.ERP.ControleInventario.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bernal.ERP.ControleInventario.Infra.Configuracoes
{
    public class InventarioDispositivoConfiguracao : IEntityTypeConfiguration<InventarioDispositivo>
    {
        public void Configure(EntityTypeBuilder<InventarioDispositivo> builder)
        {
            builder.ToTable("inventario_dispositivos");
            builder.HasKey(b => b.InventarioDispositivoId);
            
            builder
                 .Property(b => b.InventarioDispositivoId)
                 .HasColumnName("inventario_dispositivo_id")
                 .IsRequired();
            builder
                 .Property(b => b.NumeroDeSerie)
                 .HasColumnName("numero_de_serie")
                 .HasMaxLength(80)
                 .IsRequired(false);
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
               .Property(b => b.ResponsavelPelaContagem)
               .HasColumnName("reponsavel_pela_contagem")
               .HasMaxLength(100)
               .IsRequired(false);
            builder
           .Property(b => b.InventarioId)
           .HasColumnName("inventario_id")
           .IsRequired();

        }
    }

}
