using Domain.Entities;

namespace Application.UseCases
{
    public interface IUsuarioUseCase
    {
        IList<Usuario> ObterUsuarios();
    }
}
