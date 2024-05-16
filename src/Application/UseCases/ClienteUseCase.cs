using Application.Models.Request;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases
{
    public class ClienteUseCase(IClienteRepository clienteRepository) : IClienteUseCase
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;

        public async Task<bool> CadastrarClienteAsync(ClienteRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var clienteExistente = await _clienteRepository.FindByIdAsync(request.Id, cancellationToken);

            if (clienteExistente != null)
            {
                return false;
            }

            var cliente = new Cliente(request.Id, request.Nome, request.Email, request.Cpf, request.Ativo);

            await _clienteRepository.InsertAsync(cliente, cancellationToken);

            return await _clienteRepository.UnitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<bool> AtualizarClienteAsync(ClienteRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var clienteExistente = await _clienteRepository.FindByIdAsync(request.Id, cancellationToken);

            if (clienteExistente == null)
            {
                return false;
            }

            var cliente = new Cliente(request.Id, request.Nome, request.Email, request.Cpf, request.Ativo);

            await _clienteRepository.UpdateAsync(cliente, cancellationToken);

            return await _clienteRepository.UnitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<bool> DeletarClienteAsync(Guid id, CancellationToken cancellationToken)
        {
            var clienteExistente = await _clienteRepository.FindByIdAsync(id, cancellationToken);

            if (clienteExistente == null)
            {
                return false;
            }

            await _clienteRepository.DeleteAsync(id, cancellationToken);

            return await _clienteRepository.UnitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<IEnumerable<Cliente>> ObterTodosClientesAsync(CancellationToken cancellationToken) =>
            await _clienteRepository.ObterTodosClientesAsync();
    }
}
