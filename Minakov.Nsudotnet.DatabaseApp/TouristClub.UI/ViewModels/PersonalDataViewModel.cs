using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TouristClub.Data.Entity;

namespace TouristClub.UI.ViewModels
{
    class PersonalDataViewModel : PropertyChangedBase
    {
        public PersonalDataViewModel()
        {
            PersonalDataEntity = new PersonalData();
        }
        
        public PersonalDataViewModel(PersonalData personalDataEntity)
        {
            PersonalDataEntity = personalDataEntity;
        }

        public PersonalData PersonalDataEntity { get; set; }
        
        public string Name
        {
            get { return PersonalDataEntity.Name; }
            set
            {
                if (value == PersonalDataEntity.Name)
                    return;
                PersonalDataEntity.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public string Surname
        {
            get { return PersonalDataEntity.Surname; }
            set
            {
                if (value == PersonalDataEntity.Surname)
                    return;
                PersonalDataEntity.Surname = value;
                NotifyOfPropertyChange(() => Surname);
            }
        }

        public string Patronymic
        {
            get { return PersonalDataEntity.Patronymic; }
            set
            {
                if (value == PersonalDataEntity.Patronymic)
                    return;
                PersonalDataEntity.Patronymic = value;
                NotifyOfPropertyChange(() => Patronymic);
            }
        }
        public string Gender
        {
            get { return PersonalDataEntity.Gender; }
            set
            {
                if (value == PersonalDataEntity.Gender)
                    return;
                PersonalDataEntity.Gender = value;
                NotifyOfPropertyChange(() => Gender);
            }
        }
        public System.DateTime BirthDate
        {
            get { return PersonalDataEntity.BirthDate; }
            set
            {
                if (value == PersonalDataEntity.BirthDate)
                    return;
                PersonalDataEntity.BirthDate = value;
                NotifyOfPropertyChange(() => BirthDate);
            }
        }
    }
}