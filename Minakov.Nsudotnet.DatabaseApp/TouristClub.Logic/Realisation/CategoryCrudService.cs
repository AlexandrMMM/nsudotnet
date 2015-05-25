using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class CategoryCrudService : CrudService<Category>, ICategoryCrudService
    {
        public CategoryCrudService(DataContext context) : base(context)
        {
        }
    }
}