using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TouristClub.Data.Entity;

namespace TouristClub.UI.ViewModels
{
    class TraningViewModel : PropertyChangedBase
    {
        private TrainerViewModel _trainerViewModel;

        public TraningViewModel()
        {
            TraningEntity = new Traning();
        }

        public TraningViewModel(Traning traningEntity)
        {
            TraningEntity = traningEntity;
            _trainerViewModel = new TrainerViewModel(TraningEntity.Trainer);
        }

        public Traning TraningEntity { get; set; }
        
        public string Place
        {
            get { return TraningEntity.Place; }
            set
            {
                if (value == TraningEntity.Place)
                    return;
                TraningEntity.Place = value;
                NotifyOfPropertyChange(() => Place);
            }
        }

        public int TreningTimeInMinutes
        {
            get { return TraningEntity.TreningTimeInMinutes; }
            set
            {
                if (value == TraningEntity.TreningTimeInMinutes)
                    return;
                TraningEntity.TreningTimeInMinutes = value;
                NotifyOfPropertyChange(() => TreningTimeInMinutes);
            }
        }
        public System.DateTime DateTime
        {
            get { return TraningEntity.DateTime; }
            set
            {
                if (value == TraningEntity.DateTime)
                    return;
                TraningEntity.DateTime = value;
                NotifyOfPropertyChange(() => DateTime);
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
                    TraningEntity.TrainerId = _trainerViewModel.TrainerEntity.Id;
                    TraningEntity.Trainer = _trainerViewModel.TrainerEntity;
                    NotifyOfPropertyChange(() => Trainer);
                }
            }
        }
    }
}