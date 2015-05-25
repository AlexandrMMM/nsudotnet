using System;
using System.Collections.Generic;
using System.Linq;
using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class SportsmanCrudService : CrudService<Sportsman>, ISportsmanCrudService
    {
        private readonly DataContext _myContext;

        public SportsmanCrudService(DataContext context) : base(context)
        {
            _myContext = context;
        }

        public IQueryable<Sportsman> GetSportsmanOnSection(int sectionId)
        {
            return _myContext.SportsmanSet.Where(e => e.SectionId == sectionId).AsQueryable();
        }

        public IQueryable<Sportsman> GetSportsmanFromSectionOnCountCampaign(int countCampaign, int sectionId)
        {
            return _myContext.SportsmanSet.Where(e => e.PersonalData.Campaign.Count == countCampaign);
        }

        public IQueryable<Sportsman> GetSportsmanFromSectionOnCampaign(int campaignId, int sectionId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Sportsman> GetSportsmanFromSectionOnRoutePoint(int routePointId, int sectionId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Sportsman> GetSportsmanFromSectionOnCategory(int categoryId, int sectionId)
        {
            return _myContext.SportsmanSet.Where(e => e.CategoryId == categoryId);
        }

        public IQueryable<Sportsman> GetSportsmanFromSectionOnTime(DateTime start, DateTime end, int sectionId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Sportsman> GetSportsmanFromSectionOnAllCampaign(int sectionId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Sportsman> GetSportsmanOnCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Sportsman> GetSportsmanFromSectionOnSomeCampaign(ICollection<Campaign> someCampaigns, int sectionId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Sportsman> GetSportsmanFromCampaign(int campaignId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Sportsman> GetSportsmanFromCountCampaign(int countCampaign)
        {
            throw new NotImplementedException();
        }

        public int GetCountSportsmanOnSection(int sectionId)
        {
            throw new NotImplementedException();
        }

        public int GetCountSportsmanFromSectionOnCountCampaign(int countCampaign, int sectionId)
        {
            throw new NotImplementedException();
        }

        public int GetCountSportsmanFromSectionOnCampaign(int campaignId, int sectionId)
        {
            throw new NotImplementedException();
        }

        public int GetCountSportsmanFromSectionOnRoutePoint(int routePointId, int sectionId)
        {
            throw new NotImplementedException();
        }

        public int GetCountSportsmanFromSectionOnCategory(int categoryId, int sectionId)
        {
            throw new NotImplementedException();
        }

        public int GetCountSportsmanFromSectionOnTime(DateTime start, DateTime end, int sectionId)
        {
            throw new NotImplementedException();
        }

        public int GetCountSportsmanFromCampaign(int campaignId)
        {
            throw new NotImplementedException();
        }

        public int GetCountSportsmanFromCountCampaign(int countCampaign)
        {
            throw new NotImplementedException();
        }

        public int GetCountSportsmanOnCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}