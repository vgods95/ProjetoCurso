using GerenciadorCondominios.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorCondominios.DAL.Mapeamentos
{
    public class MesMap : IEntityTypeConfiguration<Mes>
    {
        public void Configure(EntityTypeBuilder<Mes> builder)
        {
            builder.HasKey(m => m.Codigo);
            builder.Property(m => m.Codigo).ValueGeneratedNever();
            builder.Property(m => m.Nome).IsRequired().HasMaxLength(30);
            builder.HasIndex(m => m.Nome).IsUnique();

            builder.HasMany(m => m.Alugueis).WithOne(m => m.Mes);
            builder.HasMany(m => m.HistoricoRecursos).WithOne(m => m.Mes);

            builder.HasData(
                new Mes
                {
                    Codigo = 1,
                    Nome = "Janeiro"
                },
                new Mes
                {
                    Codigo = 2,
                    Nome = "Fevereiro"
                },
                new Mes
                {
                    Codigo = 3,
                    Nome = "Março"
                },
                new Mes
                {
                    Codigo = 4,
                    Nome = "Abril"
                },
                new Mes
                {
                    Codigo = 5,
                    Nome = "Maio"
                },
                new Mes
                {
                    Codigo = 6,
                    Nome = "Junho"
                },
                new Mes
                {
                    Codigo = 7,
                    Nome = "Julho"
                },
                new Mes
                {
                    Codigo = 8,
                    Nome = "Agosto"
                },
                new Mes
                {
                    Codigo = 9,
                    Nome = "Setembro"
                },
                new Mes
                {
                    Codigo = 10,
                    Nome = "Outubro"
                },
                new Mes
                {
                    Codigo = 11,
                    Nome = "Novembro"
                },
                new Mes
                {
                    Codigo = 12,
                    Nome = "Dezembro"
                });

            builder.ToTable("Meses");
        }
    }
}
