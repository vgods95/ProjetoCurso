using GerenciadorCondominios.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorCondominios.DAL.Mapeamentos
{
    public class ServicoMap : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.HasKey(s => s.Codigo);
            builder.Property(s => s.Nome).IsRequired().HasMaxLength(30);
            builder.Property(s => s.Valor).IsRequired();
            builder.Property(s => s.Status).IsRequired();
            builder.Property(s => s.CodigoUsuario).IsRequired();

            builder.HasOne(s => s.Usuario).WithMany(s => s.Servicos).HasForeignKey(s => s.CodigoUsuario);
            builder.HasMany(s => s.ServicosPredios).WithOne(s => s.Servico);

            builder.ToTable("Servicos");
        }
    }
}
