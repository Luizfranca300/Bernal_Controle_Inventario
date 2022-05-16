using Bernal.ERP.ControleInventario.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bernal.ERP.ControleInventario.Infra.Configuracoes
{
    public class ContagemConfiguracao : IEntityTypeConfiguration<Contagem>
    {

        public void Configure(EntityTypeBuilder<Contagem> builder)
        {

            builder.ToTable("tipo_de_contagem");
            builder.HasKey(b => b.ContagemId);

            builder
                 .Property(b => b.ContagemId)
                 .HasColumnName("tipo_de_contagem_id")
                 .IsRequired();
            builder
                 .Property(b => b.Sessao)
                 .HasColumnName("sessao")
                 .HasMaxLength(80)
                 .IsRequired(false);
            builder
                 .Property(b => b.Equipe)
                 .HasColumnName("equipe")
                 .HasMaxLength(80)
                 .IsRequired(false);
            builder
             .Property(b => b.InventarioItemId)
             .HasColumnName("inventario_item_id")
             .IsRequired();
            builder
                .Property(c => c.Tipo)
                .HasConversion<int>();
        }
    }
}
