using backendAPIPorzione.Datos;
using backendAPIPorzione.Models;
using backendAPIPorzione.Models.Dto;
using backendAPIPorzione.Repository.IRepository;

namespace backendAPIPorzione.Repository
{
    public class DetalleRepository : Repository<Detalle>, IDetalleRepository
    {
        readonly PorzioneapiContext _context;

        public DetalleRepository(PorzioneapiContext context) : base (context)
        {
            _context = context;
        }

        public async Task Crear(MenuDto menuDto, int id)
        {
            Detalle modeloDetalle = new Detalle();
            modeloDetalle.IdMenu = id;

            foreach (var item in menuDto.Productos)
            {
                modeloDetalle.Id = 0;
                modeloDetalle.IdProducto = item.Id;
                await _context.AddAsync(modeloDetalle);
                await _context.SaveChangesAsync();
            }
        }
    }
}
