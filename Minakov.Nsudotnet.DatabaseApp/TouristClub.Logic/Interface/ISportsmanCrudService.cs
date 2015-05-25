using System;
using System.Collections.Generic;
using System.Linq;
using TouristClub.Data.Entity;

namespace TouristClub.Logic.Interface
{
    public interface ISportsmanCrudService : ICrudService<Sportsman>
    {
        IQueryable<Sportsman> GetSportsmanOnSection(int sectionId);

        IQueryable<Sportsman> GetSportsmanFromSectionOnCountCampaign(int countCampaign, int sectionId);

        IQueryable<Sportsman> GetSportsmanFromSectionOnCampaign(int campaignId, int sectionId);

        IQueryable<Sportsman> GetSportsmanFromSectionOnRoutePoint(int routePointId, int sectionId);

        IQueryable<Sportsman> GetSportsmanFromSectionOnCategory(int categoryId, int sectionId);

        IQueryable<Sportsman> GetSportsmanFromSectionOnTime(DateTime start, DateTime end, int sectionId);

        IQueryable<Sportsman> GetSportsmanFromSectionOnAllCampaign(int sectionId);

        IQueryable<Sportsman> GetSportsmanOnCategory(int categoryId);

        IQueryable<Sportsman> GetSportsmanFromSectionOnSomeCampaign(ICollection<Campaign> someCampaigns, int sectionId);

        IQueryable<Sportsman> GetSportsmanFromCampaign(int campaignId);

        IQueryable<Sportsman> GetSportsmanFromCountCampaign(int countCampaign);

        int GetCountSportsmanOnSection(int sectionId);

        int GetCountSportsmanFromSectionOnCountCampaign(int countCampaign, int sectionId);

        int GetCountSportsmanFromSectionOnCampaign(int campaignId, int sectionId);

        int GetCountSportsmanFromSectionOnRoutePoint(int routePointId, int sectionId);

        int GetCountSportsmanFromSectionOnCategory(int categoryId, int sectionId);

        int GetCountSportsmanFromSectionOnTime(DateTime start, DateTime end, int sectionId);

        int GetCountSportsmanFromCampaign(int campaignId);

        int GetCountSportsmanFromCountCampaign(int countCampaign);

        int GetCountSportsmanOnCategory(int categoryId);
    }
}