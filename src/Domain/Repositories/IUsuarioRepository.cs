using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUsuarioRepository
    {
        IList<Usuario> ObterUsuarios();
    }
}
