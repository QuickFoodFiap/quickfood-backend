using Domain.Entities;
using Domain.ValuesObjects;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Mappings.SeedData
{
    [ExcludeFromCodeCoverage]
    public static class ProdutoSeedData
    {
        public static List<Produto> GetSeedData() =>
        [
            new Produto(Guid.Parse("2d034eae-b870-4cd6-a6e8-092c305931a3"), "X-Salada", "Hamburguer, pão, queijo, alface", 21, Categoria.Lanche)
        ];
    }
}