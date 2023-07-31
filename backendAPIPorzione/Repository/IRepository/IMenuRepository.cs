using backendAPIPorzione.Models;

namespace backendAPIPorzione.Repository.IRepository
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task UpdateMenu(Menu menu);
    }
}
