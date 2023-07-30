using backendAPIPorzione.Models;

namespace backendAPIPorzione.Repository.IRepository
{
    public interface IProductoRepository : IRepository<Producto>
    {
        Task UpdateProducto(Producto producto);
    }
}
