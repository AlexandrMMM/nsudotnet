using System.Linq;
using TouristClub.Data.Entity;

namespace TouristClub.Logic.Interface
{
    public interface ICompetitionCrudService : ICrudService<Competition>
    {
        IQueryable<Competition> GetCompetitionWhereSportsmanParticipate();

        IQueryable<Competition> GetCompetitionWhereSportsmanParticipateOnSection(int sectionId);

        int GetCountCompetitionWhereSportsmanParticipate();

        int GetCountCompetitionWhereSportsmanParticipateOnSection(int sectionId);
    }
}