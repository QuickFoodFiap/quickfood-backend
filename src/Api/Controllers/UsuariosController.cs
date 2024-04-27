using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IUsuarioUseCase _usuarioUseCase;

        public UsuariosController(ILogger<UsuariosController> logger,
            IUsuarioUseCase usuarioUseCase)
        {
            _logger = logger;
            _usuarioUseCase = usuarioUseCase;
        }

        [HttpGet]
        public IActionResult ObterUsuarios() =>
            Ok(_usuarioUseCase.ObterUsuarios());
    }
}
