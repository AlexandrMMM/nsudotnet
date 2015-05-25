using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TouristClub.Data.Entity;

namespace TouristClub.UI.ViewModels
{
    class SportsmanViewModel : PropertyChangedBase
    {
        private ICollection<CompetitionViewModel> _competition;
        private PersonalDataViewModel _personalDataViewModel;
        private SectionViewModel _sectionViewModel;
        private CategoryViewModel _categoryViewModel;

        public SportsmanViewModel()
        {
            SportsmanEntity = new Sportsman();
        }

        public SportsmanViewModel(Sportsman sportsmanEntity)
        {
            SportsmanEntity = sportsmanEntity;
            _personalDataViewModel = new PersonalDataViewModel(SportsmanEntity.PersonalData);
            _sectionViewModel = new SectionViewModel(SportsmanEntity.Section);
            _categoryViewModel = new CategoryViewModel(SportsmanEntity.Category);
        }

        public Sportsman SportsmanEntity { get; private set; }
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
                    SportsmanEntity.PersonalDataId = _personalDataViewModel.PersonalDataEntity.Id;
                    SportsmanEntity.PersonalData = _personalDataViewModel.PersonalDataEntity;
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
                    SportsmanEntity.SectionId = _sectionViewModel.SectionEntity.Id;
                    SportsmanEntity.Section = _sectionViewModel.SectionEntity;
                    NotifyOfPropertyChange(() => Section);
                }
            }
        }
        public CategoryViewModel Category
        {
            get
            {
                return _categoryViewModel;
            }
            set
            {
                if (value != _categoryViewModel)
                {
                    _categoryViewModel = value;
                    SportsmanEntity.CategoryId = _categoryViewModel.CategoryEntity.Id;
                    SportsmanEntity.Category = _categoryViewModel.CategoryEntity;
                    NotifyOfPropertyChange(() => Category);
                }
            }
        }

        public ICollection<CompetitionViewModel> Competition
        {
            get
            {
                if (_competition == null)
                {
                    _competition = new ObservableCollection<CompetitionViewModel>();
                    foreach (var section in SportsmanEntity.Competition)
                    {
                        _competition.Add(new CompetitionViewModel(section));
                    }
                }
                return _competition;
            }
            set
            {
                if (!Equals(value, _competition))
                {
                    _competition = value;
                    NotifyOfPropertyChange(() => Competition);
                }
            }
        }
    }
}