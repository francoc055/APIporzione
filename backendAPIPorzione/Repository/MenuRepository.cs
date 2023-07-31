using backendAPIPorzione.Datos;
using backendAPIPorzione.Models;
using backendAPIPorzione.Repository.IRepository;

namespace backendAPIPorzione.Repository
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        readonly PorzioneapiContext _context;

        public MenuRepository(PorzioneapiContext context) : base (context)
        {
            _context = context;
        }

        public Task UpdateMenu(Menu menu)
        {
            throw new NotImplementedException();
        }
    }
}
