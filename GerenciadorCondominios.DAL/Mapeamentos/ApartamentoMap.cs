using GerenciadorCondominios.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorCondominios.DAL.Mapeamentos
{
    public class ApartamentoMap : IEntityTypeConfiguration<Apartamento>
    {
        public void Configure(EntityTypeBuilder<Apartamento> builder)
        {
            builder.HasKey(a => a.Codigo);
            builder.Property(a => a.Numero).IsRequired();
            builder.Property(a => a.Andar).IsRequired();
            builder.Property(a => a.Foto).IsRequired();
            builder.Property(a => a.CodigoProprietario).IsRequired();
            builder.Property(a => a.CodigoMorador).IsRequired(false);

            builder.HasOne(a => a.Proprietario).WithMany(a => a.ProprietariosApartamento).HasForeignKey(a => a.CodigoProprietario).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(a => a.Morador).WithMany(a => a.MoradoresApartamento).HasForeignKey(a => a.CodigoMorador).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Apartamentos");
        }
    }
}
