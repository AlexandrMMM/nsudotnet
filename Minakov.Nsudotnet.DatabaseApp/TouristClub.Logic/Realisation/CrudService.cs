using System.Data.Entity;
using System.Linq;
using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class CrudService<TEntity> : ICrudService<TEntity> where TEntity : class, IEntity
    {
        private readonly DataContext _context;

        public CrudService(DataContext context)
        {
            _context = context;
        }
        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public void Create(TEntity entity)
        {
            if(entity == null)
                return;
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                return;
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                return;
            Delete(entity.Id);
        }

        public void Delete(int id)
        {
            var entity = _context.Set<TEntity>().First(c => c.Id == id);
            if(entity == null)
                return;
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }
    }
}