using Cora.Infra.Repository;
using Domain.Entities;
using Domain.Repositories;
using Infra.Context;

namespace Infra.Repositories
{
    public class PagamentoRepository(ApplicationDbContext context) : RepositoryGeneric<Pagamento>(context), IPagamentoRepository
    {

    }
}
