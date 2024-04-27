using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases
{
    public class UsuarioUseCase : IUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioUseCase(IUsuarioRepository usuarioRepository) => 
            _usuarioRepository = usuarioRepository;

        public IList<Usuario> ObterUsuarios() => 
            _usuarioRepository.ObterUsuarios();
    }
}
