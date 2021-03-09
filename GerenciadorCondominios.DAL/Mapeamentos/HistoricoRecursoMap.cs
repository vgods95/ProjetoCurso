using GerenciadorCondominios.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorCondominios.DAL.Mapeamentos
{
    public class HistoricoRecursoMap : IEntityTypeConfiguration<HistoricoRecurso>
    {
        public void Configure(EntityTypeBuilder<HistoricoRecurso> builder)
        {
            builder.HasKey(hr => hr.Codigo);
            builder.Property(hr => hr.Valor).IsRequired();
            builder.Property(hr => hr.Tipo).IsRequired();
            builder.Property(hr => hr.Dia).IsRequired();
            builder.Property(hr => hr.CodigoMes).IsRequired();
            builder.Property(hr => hr.Ano).IsRequired();

            builder.HasOne(hr => hr.Mes).WithMany(e => e.HistoricoRecursos).HasForeignKey(e => e.CodigoMes);

            builder.ToTable("Historico_Recursos");
        }
    }
}