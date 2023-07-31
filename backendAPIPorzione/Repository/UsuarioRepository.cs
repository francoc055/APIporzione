using backendAPIPorzione.Datos;
using backendAPIPorzione.Models;
using backendAPIPorzione.Repository.IRepository;

namespace backendAPIPorzione.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        readonly PorzioneapiContext _context;

        public UsuarioRepository(PorzioneapiContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            _context.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
