using Application.Models.Request;
using Application.UseCases;
using Core.WebApi.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("clientes")]
    public class ClientesController : MainController
    {
        private readonly IClienteUseCase _clienteUseCase;

        public ClientesController(IClienteUseCase clienteUseCase) =>
            _clienteUseCase = clienteUseCase;

        [HttpGet]
        public async Task<IActionResult> ObterTodosClientes(CancellationToken cancellationToken)
        {
            var result = await _clienteUseCase.ObterTodosClientesAsync(cancellationToken);

            return result != null ? SuccessOk(result) : ErrorNotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarCliente(ClienteRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ErrorBadRequestModelState(ModelState);
            }

            var result = await _clienteUseCase.CadastrarClienteAsync(request, cancellationToken);

            return result ? SuccessCreated($"clientes/{request.Id}", request) : ErrorBadRequest(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> AtualizarCliente([FromRoute] Guid id, ClienteRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ErrorBadRequestModelState(ModelState);
            }

            if (id != request.Id)
            {
                return ErrorBadRequestPutId();
            }

            var result = await _clienteUseCase.AtualizarClienteAsync(request, cancellationToken);

            return result ? SuccessNoContent() : ErrorBadRequest(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletarCliente([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _clienteUseCase.DeletarClienteAsync(id, cancellationToken);

            return result ? SuccessNoContent() : ErrorNotFound();
        }
    }
}
