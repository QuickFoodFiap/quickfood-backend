using Cora.Infra.Repository;
using Domain.Entities;
using Domain.Repositories;
using Infra.Context;

namespace Infra.Repositories
{
    public class ProdutoRepository(ApplicationDbContext context) : RepositoryGeneric<Produto>(context), IProdutoRepository
    {
    }
}
