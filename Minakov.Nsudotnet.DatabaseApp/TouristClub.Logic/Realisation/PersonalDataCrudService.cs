using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class PersonalDataCrudService : CrudService<PersonalData>, IPersonalDataCrudService
    {
        public PersonalDataCrudService(DataContext context) : base(context)
        {
        }
    }
}