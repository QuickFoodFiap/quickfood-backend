using Application.Models.Request;
using Application.UseCases;
using Core.WebApi.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoUseCase _produtoUseCase;

        public ProdutosController(IProdutoUseCase produtoUseCase) =>
            _produtoUseCase = produtoUseCase;

        [HttpPost]
        public async Task<IActionResult> InserirProduto(ProdutoRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ErrorBadRequestModelState(ModelState);
            }
            var result = await _produtoUseCase.RegistrarProdutoAsync(request, cancellationToken);

            return result ? SuccessCreated($"produtos/{request.Id}", request) : BadRequest(result);
        }
    }
}
