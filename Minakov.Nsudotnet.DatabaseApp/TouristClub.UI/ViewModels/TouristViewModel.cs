using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TouristClub.Data.Entity;

namespace TouristClub.UI.ViewModels
{
    class TouristViewModel : PropertyChangedBase
    {
        private GroupViewModel _groupViewModel;
        private PersonalDataViewModel _personalDataViewModel;

        public TouristViewModel()
        {
            
        }

        public TouristViewModel(Tourist touristEntity)
        {
            TouristEntity = touristEntity;
            _groupViewModel = new GroupViewModel(TouristEntity.Group);
            _personalDataViewModel = new PersonalDataViewModel(TouristEntity.PersonalData);
        }

        public Tourist TouristEntity { get; private set; }

        public PersonalDataViewModel PersonalData
        {
            get
            {
                return _personalDataViewModel;
            }
            set
            {
                if (value != _personalDataViewModel)
                {
                    _personalDataViewModel = value;
                    TouristEntity.PersonalDataId = _personalDataViewModel.PersonalDataEntity.Id;
                    TouristEntity.PersonalData = _personalDataViewModel.PersonalDataEntity;
                    NotifyOfPropertyChange(() => PersonalData);
                }
            }
        }

        public GroupViewModel Group
        {
            get
            {
                return _groupViewModel;
            }
            set
            {
                if (value != _groupViewModel)
                {
                    _groupViewModel = value;
                    TouristEntity.GroupId = _groupViewModel.GroupEntity.Id;
                    TouristEntity.Group = _groupViewModel.GroupEntity;
                    NotifyOfPropertyChange(() => Group);
                }
            }
        }
    }
}