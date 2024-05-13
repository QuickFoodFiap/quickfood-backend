using Cora.Infra.Repository;
using Domain.Entities;
using Domain.Repositories;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class ClienteRepository(ApplicationDbContext context) : RepositoryGeneric<Cliente>(context), IClienteRepository
    {
        private readonly DbSet<Cliente> _clientes = context.Set<Cliente>();

        public async Task<IEnumerable<Cliente>> ObterTodosClientesAsync() =>
            await _clientes.AsNoTracking().Where(p => p.Ativo).ToListAsync();
    }
}
