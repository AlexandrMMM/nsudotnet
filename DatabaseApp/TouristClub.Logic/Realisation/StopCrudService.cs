using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class StopCrudService : CrudService<Stop>, IStopCrudService
    {
        public StopCrudService(DataContext context) : base(context)
        {
        }
    }
}