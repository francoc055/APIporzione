using backendAPIPorzione.Models;

namespace backendAPIPorzione.Repository.IRepository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task UpdateUsuario(Usuario usuario);
    }
}
