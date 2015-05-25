using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TouristClub.Data.Entity;

namespace TouristClub.Logic.Interface
{
    public interface ITouristCrudService : ICrudService<Tourist>
    {
        IQueryable<Tourist> GetTouristsOnGroup(int groupId);

        IQueryable<Tourist> GetTouristsOnGender(string gender);
        
        IQueryable<Tourist> GetTouristsOnBirthDate(DateTime date);

        IQueryable<Tourist> GetTouristsOnAge(int age);

        IQueryable<Tourist> GetTouristsFromGroupOnCountCampaign(int countCampaign, int groupId);

        IQueryable<Tourist> GetTouristsFromGroupOnCampaign(int campaignId, int groupId);

        IQueryable<Tourist> GetTouristsFromGroupOnRoutePoint(int routePointId, int groupId);

        IQueryable<Tourist> GetTouristsFromGroupOnTime(DateTime start, DateTime end, int groupId);

        IQueryable<Tourist> GetTouristsFromGroupOnAllCampaign(int group);

        IQueryable<Tourist> GetTouristsFromGroupOnSomeCampaign(ICollection<Campaign> someCampaigns, int group);

        IQueryable<Tourist> GetTouristsFromGroupOnCampaignWithTrainer(int group);

        IQueryable<Tourist> GetTouristsFromGroupOnCampaignType(int groupId, int campaignTypeId);

        int GetCountTouristsOnGroup(int groupId);

        int GetCountTouristsOnGender(string gender);

        int GetCountTouristsOnBirthDate(DateTime date);

        int GetCountTouristsOnAge(int age);

        int GetCountTouristsFromGroupOnCountCampaign(int countCampaign, int groupId);

        int GetCountTouristsFromGroupOnCampaign(int campaignId, int groupId);

        int GetCountTouristsFromGroupOnRoutePoint(int routePointId, int groupId);

        int GetCountTouristsFromGroupOnTime(DateTime start, DateTime end, int groupId);

        int GetTouristCountFromGroupOnCampaignType(int groupId, int campaignTypeId);
    }
}