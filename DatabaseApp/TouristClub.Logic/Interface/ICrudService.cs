using System.Linq;
using TouristClub.Data.Entity;

namespace TouristClub.Logic.Interface
{
    public interface ICrudService<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll ();
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int id);
    }
}