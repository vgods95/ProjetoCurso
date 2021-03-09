using GerenciadorCondominios.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorCondominios.DAL.Mapeamentos
{
    public class ServicoPredioMap : IEntityTypeConfiguration<ServicoPredio>
    {
        public void Configure(EntityTypeBuilder<ServicoPredio> builder)
        {
            builder.HasKey(sp => sp.Codigo);
            builder.Property(sp => sp.CodigoServico).IsRequired();
            builder.Property(sp => sp.DataExecucao).IsRequired();

            builder.HasOne(sp => sp.Servico).WithMany(sp => sp.ServicosPredios).HasForeignKey(sp => sp.CodigoServico);

            builder.ToTable("Servico_Predios");
        }
    }
}
