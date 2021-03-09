using GerenciadorCondominios.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorCondominios.DAL.Mapeamentos
{
    class PagamentoMap : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.HasKey(a => a.Codigo);
            builder.Property(a => a.CodigoUsuario).IsRequired();
            builder.Property(a => a.CodigoAluguel).IsRequired();
            builder.Property(a => a.Status).IsRequired();

            builder.HasOne(a => a.Usuario).WithMany(a => a.Pagamentos).HasForeignKey(a => a.CodigoUsuario);
            builder.HasOne(a => a.Aluguel).WithMany(a => a.Pagamentos).HasForeignKey(a => a.CodigoAluguel);

            builder.ToTable("Pagamentos");

            //builder.Property(x => x.Ano).HasColumnName("ano"); linha para definir o nome da coluna no BD
        }
    }
}
