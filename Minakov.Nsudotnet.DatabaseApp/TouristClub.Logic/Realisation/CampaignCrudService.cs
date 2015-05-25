using System.Linq;
using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class CampaignCrudService : CrudService<Campaign> , ICampaignCrudService
    {
        private readonly DataContext _myContext;

        public CampaignCrudService(DataContext context) : base(context)
        {
            _myContext = context;
        }

        public IQueryable<Campaign> GetCampaignFromRoutePoint(int routePointId)
        {
            return _myContext.RoutePointSet.First(e => routePointId == e.Id).Campaign.AsQueryable();
        }

        public IQueryable<Campaign> GetCampaignMoreThenLength(int length)
        {
            return _myContext.CampaignSet.Where(e => e.CampaignTimeinHour <= length);
        }

        public IQueryable<Campaign> GetCampaignOnCategory(int categoryId)
        {
            return _myContext.CampaignSet.Where(e => e.CategoryId == categoryId);
        }

        public int GetCountCampaignFromRoutePoint(int routePointId)
        {
            return _myContext.RoutePointSet.First(e => routePointId == e.Id).Campaign.Count();
        }

        public int GetCountCampaignMoreThenLength(int length)
        {
            return _myContext.CampaignSet.Count(e => e.CampaignTimeinHour <= length);
        }

        public int GetCountCampaignOnCategory(int categoryId)
        {
            return _myContext.CampaignSet.Count(e => e.CategoryId == categoryId);
        }
    }
}