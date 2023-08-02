using backendAPIPorzione.Models;
using backendAPIPorzione.Models.Dto;

namespace backendAPIPorzione.Repository.IRepository
{
    public interface IDetalleRepository : IRepository<Detalle>
    {
        Task Crear(MenuDto menuDto, int id);
    }
}
