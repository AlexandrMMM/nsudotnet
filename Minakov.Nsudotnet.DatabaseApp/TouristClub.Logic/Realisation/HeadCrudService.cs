using System;
using System.Linq;
using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class HeadCrudService : CrudService<Head>, IHeadCrudService
    {
        private readonly DataContext _myContext;

        public HeadCrudService(DataContext context) : base(context)
        {
            _myContext = context;
        }

        public IQueryable<Head> GetHeadsOnBirthDate(DateTime date)
        {
            return _myContext.HeadSet.Where(e => e.PersonalData.BirthDate == date);
        }

        public IQueryable<Head> GetHeadsOnEmployDate(DateTime date)
        {
            return _myContext.HeadSet.Where(e => e.EmployDate == date);
        }

        public IQueryable<Head> GetHeadsFromGroupOnAge(int age)
        {
            return _myContext.HeadSet.Where(e => DateTime.Now.Year - e.PersonalData.BirthDate.Year == age);
        }
    }
}