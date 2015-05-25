using System;
using System.Linq;
using TouristClub.Data.Entity;

namespace TouristClub.Logic.Interface
{
    public interface ITrainerCrudService : ICrudService<Trainer>
    {
        IQueryable<Trainer> GetTrainerOnSection(int sectionId);

        IQueryable<Tourist> GetTrainersOnGender(string gender);

        IQueryable<Tourist> GetTrainersOnAge(int age);

        IQueryable<Tourist> GetTrainersOnSalary(int salary);

        IQueryable<Tourist> GetTrainersOnTreningDate(DateTime begin, DateTime end);

        IQueryable<Tourist> GetTrainersOnTreningInGroup(int groupId);

        int GetCountTrainersOnGender(string gender);

        int GetCountTrainersOnAge(int age);

        int GetCountTrainerOnSection(int sectionId);

        int GetCountTrainerOnSalary(int salary);

        
    }
}