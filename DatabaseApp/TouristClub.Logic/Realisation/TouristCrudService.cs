using System;
using System.Collections.Generic;
using System.Linq;
using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class TouristCrudService : CrudService<Tourist>, ITouristCrudService
    {
        private readonly DataContext _myContext;

        public TouristCrudService(DataContext context) : base(context)
        {
            _myContext = context;
        }

        public IQueryable<Tourist> GetTouristsOnGroup(int groupId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTouristsOnGender(string gender)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTouristsOnBirthDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTouristsOnAge(int age)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTouristsFromGroupOnCountCampaign(int countCampaign, int groupId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTouristsFromGroupOnCampaign(int campaignId, int groupId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTouristsFromGroupOnRoutePoint(int routePointId, int groupId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTouristsFromGroupOnTime(DateTime start, DateTime end, int groupId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTouristsFromGroupOnAllCampaign(int @group)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTouristsFromGroupOnSomeCampaign(ICollection<Campaign> someCampaigns, int @group)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTouristsFromGroupOnCampaignWithTrainer(int @group)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTouristsFromGroupOnCampaignType(int groupId, int campaignTypeId)
        {
            throw new NotImplementedException();
        }

        public int GetCountTouristsOnGroup(int groupId)
        {
            throw new NotImplementedException();
        }

        public int GetCountTouristsOnGender(string gender)
        {
            throw new NotImplementedException();
        }

        public int GetCountTouristsOnBirthDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public int GetCountTouristsOnAge(int age)
        {
            throw new NotImplementedException();
        }

        public int GetCountTouristsFromGroupOnCountCampaign(int countCampaign, int groupId)
        {
            throw new NotImplementedException();
        }

        public int GetCountTouristsFromGroupOnCampaign(int campaignId, int groupId)
        {
            throw new NotImplementedException();
        }

        public int GetCountTouristsFromGroupOnRoutePoint(int routePointId, int groupId)
        {
            throw new NotImplementedException();
        }

        public int GetCountTouristsFromGroupOnTime(DateTime start, DateTime end, int groupId)
        {
            throw new NotImplementedException();
        }

        public int GetTouristCountFromGroupOnCampaignType(int groupId, int campaignTypeId)
        {
            throw new NotImplementedException();
        }
    }
}