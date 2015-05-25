using System.Linq;
using TouristClub.Data.Entity;

namespace TouristClub.Logic.Interface
{
    public interface ICampaignCrudService : ICrudService<Campaign>
    {
        IQueryable<Campaign> GetCampaignFromRoutePoint(int routePointId);

        IQueryable<Campaign> GetCampaignMoreThenLength(int length);

        IQueryable<Campaign> GetCampaignOnCategory(int categoryId);

        int GetCountCampaignFromRoutePoint(int routePointId);

        int GetCountCampaignMoreThenLength(int length);

        int GetCountCampaignOnCategory(int categoryId);
    }
}