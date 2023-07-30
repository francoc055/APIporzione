using backendAPIPorzione.Datos;
using backendAPIPorzione.Models;
using backendAPIPorzione.Repository.IRepository;

namespace backendAPIPorzione.Repository
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        readonly PorzioneapiContext _context;

        public ProductoRepository(PorzioneapiContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateProducto(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }
    }
}
