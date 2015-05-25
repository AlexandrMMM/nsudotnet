using System;
using System.Linq;
using TouristClub.Data.Entity;

namespace TouristClub.Logic.Interface
{
    public interface IHeadCrudService : ICrudService<Head>
    {
        IQueryable<Head> GetHeadsOnBirthDate(DateTime date);

        IQueryable<Head> GetHeadsOnEmployDate(DateTime date);

        IQueryable<Head> GetHeadsFromGroupOnAge(int age);
    }
}