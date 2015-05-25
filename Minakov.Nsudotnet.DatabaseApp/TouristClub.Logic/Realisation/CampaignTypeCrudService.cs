using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class CampaignTypeCrudService : CrudService<CampaignType>, ICampaignTypeCrudService
    {
        public CampaignTypeCrudService(DataContext context) : base(context)
        {
        }
    }
}