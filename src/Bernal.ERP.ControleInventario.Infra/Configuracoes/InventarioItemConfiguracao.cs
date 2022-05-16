

using Bernal.ERP.ControleInventario.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bernal.ERP.ControleInventario.Infra.Configuracoes
{
    public class InventarioItemConfiguracao : IEntityTypeConfiguration<InventarioItem>
    {
        public void Configure(EntityTypeBuilder<InventarioItem> builder)
        {
            builder.ToTable("inventario_itens");
            builder.HasKey(b => b.InventarioItemId);

            builder
                .Property(b => b.InventarioItemId)
                .HasColumnName("inventario_item_id")
                .IsRequired();
            builder
                .Property(b => b.ProdutoCodigo)
                .HasColumnName("produto_codigo")
                .HasMaxLength(14)
                .IsRequired();
            builder
               .Property(b => b.ProdutoNome)
               .HasColumnName("produto_nome")
               .HasMaxLength(100)
               .IsRequired();
            builder
               .Property(b => b.QuantidadeDeEmbalagem)
               .HasColumnName("quantidade_embalagem")
               .IsRequired();
            builder
               .Property(b => b.Embalagem)
               .HasColumnName("embalagem")
               .IsRequired();
            builder
             .Property(b => b.InventarioId)
             .HasColumnName("inventario_id")
             .IsRequired();
            builder
              .Property(b => b.Precofinal)
              .HasColumnName("preco_final")
              .IsRequired();
            builder
              .Property(b => b.Precocustoembalagem)
              .HasColumnName("preco_custo_embalagem")
              .IsRequired();
            builder
              .Property(b => b.Icms)
              .HasColumnName("icms")
              .IsRequired();
            builder
              .Property(b => b.Transporte)
              .HasColumnName("transporte")
              .IsRequired();
            builder
             .Property(b => b.ExcluidoEm)
             .HasColumnName("excluido_em")
             .IsRequired(false);
            builder
                .HasMany(b => b.Contagem)
                .WithOne(b => b.InventarioItem)
                .HasForeignKey(b => b.InventarioItemId);
           
        }
    }

}
