using backendAPIPorzione.Datos;
using backendAPIPorzione.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace backendAPIPorzione.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        readonly PorzioneapiContext _context;
        readonly DbSet<T> dbset;

        public Repository(PorzioneapiContext context)
        {
            _context = context;
            this.dbset = _context.Set<T>(); 
        }

        public async Task<bool> CreateEntity(T entity)
        {
            if (entity is null)
                return false;

            await dbset.AddAsync(entity);
            await Grabar();
            return true;
        }

        public async Task Delete(T entity)
        {
            dbset.Remove(entity);
            await Grabar();  
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbset;

            if(filtro is not null)
            {
                query = query.Where(filtro);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetEntity(Expression<Func<T, bool>>? filtro, bool tracked = true)
        {
            IQueryable<T> query = dbset;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro is not null)
                query = query.Where(filtro);

            return await query.FirstOrDefaultAsync();
        }

        public async Task Grabar()
        {
            await _context.SaveChangesAsync();
        }
    }
}
