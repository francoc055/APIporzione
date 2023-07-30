using System.Linq.Expressions;

namespace backendAPIPorzione.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filtro = null);

        Task<T> GetEntity(Expression<Func<T, bool>>? filtro, bool tracked = true);

        Task<bool> CreateEntity(T entity);

        Task Delete(T entity);

        public Task Grabar();

    }
}
