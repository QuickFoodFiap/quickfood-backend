using Application.Models.Request;
using Application.UseCases;
using Core.WebApi.Controller;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoUseCase _produtoUseCase;

        public ProdutosController(IProdutoUseCase produtoUseCase) =>
            _produtoUseCase = produtoUseCase;

        [HttpGet]
        public async Task<IActionResult> ObterTodosProdutos(CancellationToken cancellationToken)
        {
            var result = await _produtoUseCase.ObterTodosProdutosAsync(cancellationToken);

            return result != null ? SuccessOk(result) : ErrorNotFound();
        }

        [HttpGet("categoria")]
        public async Task<IActionResult> ObterProdutosCategoria(Categoria categoria, CancellationToken cancellationToken)
        {
            var result = await _produtoUseCase.ObterProdutosCategoriaAsync(categoria, cancellationToken);

            return result != null ? SuccessOk(result) : ErrorNotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarProduto(ProdutoRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ErrorBadRequestModelState(ModelState);
            }

            var result = await _produtoUseCase.CadastrarProdutoAsync(request, cancellationToken);

            return result ? SuccessCreated($"produtos/{request.Id}", request) : ErrorBadRequest(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> AtualizarProduto([FromRoute] Guid id, ProdutoRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ErrorBadRequestModelState(ModelState);
            }

            if (id != request.Id)
            {
                return ErrorBadRequestPutId();
            }

            var result = await _produtoUseCase.AtualizarProdutoAsync(request, cancellationToken);

            return result ? SuccessNoContent() : ErrorBadRequest(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletarProduto([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _produtoUseCase.DeletarProdutoAsync(id, cancellationToken);

            return result ? SuccessNoContent() : ErrorNotFound();
        }
    }
}
