using System;
using System.Linq;
using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class TrainerCrudService : CrudService<Trainer>, ITrainerCrudService
    {
        private readonly DataContext _myContext;

        public TrainerCrudService(DataContext context) : base(context)
        {
            _myContext = context;
        }

        public IQueryable<Trainer> GetTrainerOnSection(int sectionId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTrainersOnGender(string gender)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTrainersOnAge(int age)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTrainersOnSalary(int salary)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTrainersOnTreningDate(DateTime begin, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tourist> GetTrainersOnTreningInGroup(int groupId)
        {
            throw new NotImplementedException();
        }

        public int GetCountTrainersOnGender(string gender)
        {
            throw new NotImplementedException();
        }

        public int GetCountTrainersOnAge(int age)
        {
            throw new NotImplementedException();
        }

        public int GetCountTrainerOnSection(int sectionId)
        {
            throw new NotImplementedException();
        }

        public int GetCountTrainerOnSalary(int salary)
        {
            throw new NotImplementedException();
        }
    }
}