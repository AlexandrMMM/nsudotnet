using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class CompetitionCrudService : CrudService<Competition>, ICompetitionCrudService
    {
        private readonly DataContext _myContext;

        public CompetitionCrudService(DataContext context) : base(context)
        {
            _myContext = context;
        }

        public IQueryable<Competition> GetCompetitionWhereSportsmanParticipate()
        {
            return _myContext.CompetitionSet.Where(e => e.Sportsman.Count > 0);
        }

        public IQueryable<Competition> GetCompetitionWhereSportsmanParticipateOnSection(int sectionId)
        {
            throw new System.NotImplementedException();
        }

        public int GetCountCompetitionWhereSportsmanParticipate()
        {
            return _myContext.CompetitionSet.Count(e => e.Sportsman.Count > 0);
        }

        public int GetCountCompetitionWhereSportsmanParticipateOnSection(int sectionId)
        {
            throw new System.NotImplementedException();
        }
    }
}