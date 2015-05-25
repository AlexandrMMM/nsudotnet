using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class GroupCrudService : CrudService<Group>, IGroupCrudService
    {
        public GroupCrudService(DataContext context) : base(context)
        {
        }
    }
}