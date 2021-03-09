using GerenciadorCondominios.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorCondominios.DAL.Mapeamentos
{
    public class AluguelMap : IEntityTypeConfiguration<Aluguel>
    {
        public void Configure(EntityTypeBuilder<Aluguel> builder)
        {
            builder.HasKey(a => a.Codigo);
            builder.Property(a => a.Valor).IsRequired();
            builder.Property(a => a.codigoMes).IsRequired();
            builder.Property(a => a.Ano).IsRequired();

            builder.HasOne(a => a.Mes).WithMany(a => a.Alugueis).HasForeignKey(a => a.codigoMes);
            builder.HasMany(a => a.Pagamentos).WithOne(a => a.Aluguel);

            builder.ToTable("Alugueis");

            //builder.Property(x => x.Ano).HasColumnName("ano"); linha para definir o nome da coluna no BD
        }
    }
}
