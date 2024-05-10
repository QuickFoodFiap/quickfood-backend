using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioUseCase _usuarioUseCase;

        public UsuariosController(IUsuarioUseCase usuarioUseCase) => 
            _usuarioUseCase = usuarioUseCase;

        [HttpGet]
        public IActionResult ObterUsuarios() =>
            Ok(_usuarioUseCase.ObterUsuarios());
    }
}
