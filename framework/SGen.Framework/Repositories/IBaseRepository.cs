
using Framework.Entities;
using Framework.Repositories;

namespace Framework.Repositories
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        IQueryable<TEntity> Query();
    }
}


class Car : BaseEntity<int>
{
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
}

interface ICarRepository : ICrudRepository<Car, int>
{
}

