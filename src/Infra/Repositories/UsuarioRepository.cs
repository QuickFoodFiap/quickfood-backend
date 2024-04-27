using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public IList<Usuario> ObterUsuarios()
        {
            var usuarios = new List<Usuario>()
            {
                new("Teste 1"),
                new("Teste 2")
            };

            return usuarios;
        }
    }
}
