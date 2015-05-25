using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class SectionCrudService : CrudService<Section>, ISectionCrudService
    {
        public SectionCrudService(DataContext context) : base(context)
        {
        }
    }
}