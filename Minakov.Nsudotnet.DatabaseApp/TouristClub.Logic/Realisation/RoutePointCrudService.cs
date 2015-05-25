using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class RoutePointCrudService : CrudService<RoutePoint>, IRoutePointCrudService
    {
        public RoutePointCrudService(DataContext context) : base(context)
        {
        }
    }
}