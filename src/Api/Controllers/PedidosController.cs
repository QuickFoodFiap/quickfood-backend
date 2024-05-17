using Application.Models.Request;
using Application.UseCases;
using Core.WebApi.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("pedidos")]
    public class PedidosController : MainController
    {
        private readonly IPedidoUseCase _pedidoUseCase;

        public PedidosController(IPedidoUseCase pedidoUseCase) =>
            _pedidoUseCase = pedidoUseCase;

        [HttpGet]
        public async Task<IActionResult> ObterTodosPedidos(CancellationToken cancellationToken)
        {
            var result = await _pedidoUseCase.ObterTodosPedidosAsync(cancellationToken);

            return result != null ? SuccessOk(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarPedido(PedidoRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ErrorBadRequestModelState(ModelState);
            }

            var result = await _pedidoUseCase.CadastrarPedidoAsync(request, cancellationToken);

            return result ? SuccessCreated($"pedidos/{request.PedidoId}", request) : ErrorBadRequest(result);
        }

        [HttpPatch("checkout/{pedidoId:guid}")]
        public async Task<IActionResult> Checkout([FromRoute] Guid pedidoId, [FromBody] CheckoutRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ErrorBadRequestModelState(ModelState);
            }

            var result = await _pedidoUseCase.EfetuarCheckoutAsync(pedidoId, request, cancellationToken);

            return result ? SuccessNoContent() : ErrorBadRequest(result);
        }

        [HttpPatch("status/{pedidoId:guid}")]
        public async Task<IActionResult> AlterarStatusPedido([FromRoute] Guid pedidoId, [FromBody] PedidoStatusRequest pedidoStatus, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ErrorBadRequestModelState(ModelState);
            }

            var result = await _pedidoUseCase.AlterarStatusAsync(pedidoId, pedidoStatus.Status, cancellationToken);

            return result ? SuccessNoContent() : ErrorBadRequest(result);
        }
    }
}
