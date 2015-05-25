using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class TrainigCrudService : CrudService<Traning>, ITrainigCrudService
    {
        public TrainigCrudService(DataContext context) : base(context)
        {
        }
    }
}