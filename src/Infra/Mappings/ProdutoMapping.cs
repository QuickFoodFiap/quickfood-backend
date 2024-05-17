using Domain.Entities;
using Infra.Mappings.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Mappings
{
    [ExcludeFromCodeCoverage]
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos", "dbo");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasColumnType("varchar(40)");

            builder.Property(c => c.Descricao)
                   .IsRequired()
                   .HasColumnType("varchar(200)");

            builder.Property(c => c.Preco)
                   .HasColumnType("decimal(18,2)")
                   .HasPrecision(2);

            builder.Property(c => c.Categoria)
                .HasColumnType("varchar(20)")
                .HasConversion<string>();

            // Data
            builder.HasData(ProdutoSeedData.GetSeedData());
        }
    }
}