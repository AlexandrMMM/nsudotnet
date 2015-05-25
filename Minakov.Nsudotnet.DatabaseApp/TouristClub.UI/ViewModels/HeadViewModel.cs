using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TouristClub.Data.Entity;

namespace TouristClub.UI.ViewModels
{
    class HeadViewModel : PropertyChangedBase
    {
        private PersonalDataViewModel _personalDataViewModel;
        private SectionViewModel _sectionViewModel;

        public HeadViewModel(Head headEntity)
        {
            HeadEntity = headEntity;
            _personalDataViewModel = new PersonalDataViewModel(HeadEntity.PersonalData);
            _sectionViewModel = new SectionViewModel(HeadEntity.Section);
        }

        public Head HeadEntity { get; private set; }

        public System.DateTime EmployDate
        {
            get { return HeadEntity.EmployDate; }
            set
            {
                if (value == HeadEntity.EmployDate)
                    return;
                HeadEntity.EmployDate = value;
                NotifyOfPropertyChange(() => EmployDate);
            }
        }

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
                    HeadEntity.PersonalDataId = _personalDataViewModel.PersonalDataEntity.Id;
                    HeadEntity.PersonalData = _personalDataViewModel.PersonalDataEntity;
                    NotifyOfPropertyChange(() => PersonalData);
                }
            }
        }

        public SectionViewModel Section
        {
            get
            {
                return _sectionViewModel;
            }
            set
            {
                if (value != _sectionViewModel)
                {
                    _sectionViewModel = value;
                    HeadEntity.SectionId = _sectionViewModel.SectionEntity.Id;
                    HeadEntity.Section = _sectionViewModel.SectionEntity;
                    NotifyOfPropertyChange(() => Section);
                }
            }
        }
    }
}