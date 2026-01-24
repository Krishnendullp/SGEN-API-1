
using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Contexts;
using SGen.Framework.Services;

namespace Framework.Repositories.Implementations
{
    public class BaseRepository<TEntity, TKey>: IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected readonly SGenDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IIdentityServices _identityService;

        protected BaseRepository(SGenDbContext context, IIdentityServices identityService)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _identityService = identityService;
        }
        public IQueryable<TEntity> Query()
        {
            throw new NotImplementedException();
        }
    }
}

