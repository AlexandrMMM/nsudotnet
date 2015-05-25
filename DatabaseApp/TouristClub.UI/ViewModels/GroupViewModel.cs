using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TouristClub.Data.Entity;

namespace TouristClub.UI.ViewModels
{
    class GroupViewModel : PropertyChangedBase
    {
        private TrainerViewModel _trainerViewModel;
        private SectionViewModel _sectionViewModel;

        public GroupViewModel()
        {
            GroupEntity = new Group();
        }

        public GroupViewModel(Group groupEntity)
        {
            GroupEntity = groupEntity;
            _trainerViewModel = new TrainerViewModel(GroupEntity.Trainer);
            _sectionViewModel = new SectionViewModel(GroupEntity.Section);
        }

        public Group GroupEntity { get; set; }
        
        public string Name
        {
            get { return GroupEntity.Name; }
            set
            {
                if (value == GroupEntity.Name)
                    return;
                GroupEntity.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
        public TrainerViewModel Trainer
        {
            get
            {
                return _trainerViewModel;
            }
            set
            {
                if (value != _trainerViewModel)
                {
                    _trainerViewModel = value;
                    GroupEntity.TrainerId = _trainerViewModel.TrainerEntity.Id;
                    GroupEntity.Trainer = _trainerViewModel.TrainerEntity;
                    NotifyOfPropertyChange(() => Trainer);
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
                    GroupEntity.SectionId = _sectionViewModel.SectionEntity.Id;
                    GroupEntity.Section = _sectionViewModel.SectionEntity;
                    NotifyOfPropertyChange(() => Section);
                }
            }
        }
    }
}