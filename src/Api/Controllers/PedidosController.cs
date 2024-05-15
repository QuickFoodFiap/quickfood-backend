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
    }
}
