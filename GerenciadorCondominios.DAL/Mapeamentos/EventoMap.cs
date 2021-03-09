using GerenciadorCondominios.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorCondominios.DAL.Mapeamentos
{
    public class EventoMap : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.HasKey(e => e.Codigo);
            builder.Property(e => e.Nome).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Data).IsRequired();
            builder.Property(e => e.CodigoUsuario).IsRequired();

            builder.HasOne(e => e.Usuario).WithMany(e => e.Eventos).HasForeignKey(e => e.CodigoUsuario);

            builder.ToTable("Eventos");
        }
    }
}
