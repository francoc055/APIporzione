using backendAPIPorzione.Models;

namespace backendAPIPorzione.Repository.IRepository
{
    public interface IClaveHasherRepository
    {
        Task<Usuario> Register(Usuario usuario, string clave);
        Task<Usuario> Login(string correo, string clave);
        Task<bool> ExisteUsuario(string correo);

    }
}
