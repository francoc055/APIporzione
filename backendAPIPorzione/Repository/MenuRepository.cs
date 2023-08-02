using backendAPIPorzione.Datos;
using backendAPIPorzione.Models;
using backendAPIPorzione.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace backendAPIPorzione.Repository
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        readonly PorzioneapiContext _context;

        public MenuRepository(PorzioneapiContext context) : base (context)
        {
            _context = context;
        }

        public async Task<IQueryable<dynamic>> GetMenu(int idUsuario)
        {
            Menu menu = await _context.Menus.FirstOrDefaultAsync(m => m.IdUsuario == idUsuario);
            //IQueryable<Menu> queryMenu = _context.Menus;
            //IQueryable<Detalle> queryDetalle = _context.Detalles;

            if (menu is null)
                return null;

            var lista = from menus in _context.Menus
                        join detalles in _context.Detalles
                        on menus.Id equals detalles.IdMenu
                        join productos in _context.Productos
                        on detalles.IdProducto equals productos.Id
                        where menus.IdUsuario == idUsuario && menus.Id == menu.Id
                        select new
                        {
                            menus.Categoria,
                            menus.Precio,
                            productos.NombreProducto
                        };


            return lista;
        }

        public Task UpdateMenu(Menu menu)
        {
            throw new NotImplementedException();
        }
    }
}
