using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TouristClub.Data.Entity;

namespace TouristClub.UI.ViewModels
{
    class TrainerViewModel : PropertyChangedBase
    {
        private SportsmanViewModel _sportsmanViewModel;

        public TrainerViewModel()
        {
            TrainerEntity = new Trainer();
        }

        public TrainerViewModel(Trainer trainerEntity)
        {
            TrainerEntity = trainerEntity;
            _sportsmanViewModel = new SportsmanViewModel(TrainerEntity.Sportsman);
        }

        public Trainer TrainerEntity { get; set; }

        public int Salary
        {
            get { return TrainerEntity.Salary; }
            set
            {
                if (value == TrainerEntity.Salary)
                    return;
                TrainerEntity.Salary = value;
                NotifyOfPropertyChange(() => Salary);
            }
        }

        public SportsmanViewModel Sportsman
        {
            get
            {
                return _sportsmanViewModel;
            }
            set
            {
                if (value != _sportsmanViewModel)
                {
                    _sportsmanViewModel = value;
                    TrainerEntity.SportsmanId = _sportsmanViewModel.SportsmanEntity.Id;
                    TrainerEntity.Sportsman = _sportsmanViewModel.SportsmanEntity;
                    NotifyOfPropertyChange(() => Sportsman);
                }
            }
        }
    
    }
}