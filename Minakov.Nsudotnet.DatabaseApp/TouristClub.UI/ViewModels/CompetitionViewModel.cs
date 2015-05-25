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
    class CompetitionViewModel : PropertyChangedBase
    {
        private ICollection<SportsmanViewModel> _sportsman;

        public CompetitionViewModel()
        {
            CompetitionEntity = new Competition();
        }

        public CompetitionViewModel(Competition competitionEntity)
        {
            CompetitionEntity = competitionEntity;
        }

        public Competition CompetitionEntity { get; set; }

        public string Name
        {
            get { return CompetitionEntity.Name; }
            set
            {
                if (value == CompetitionEntity.Name)
                    return;
                CompetitionEntity.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public ICollection<SportsmanViewModel> Sportsman
        {
            get
            {
                if (_sportsman == null)
                {
                    _sportsman = new ObservableCollection<SportsmanViewModel>();
                    foreach (var sportsman in CompetitionEntity.Sportsman)
                    {
                        _sportsman.Add(new SportsmanViewModel(sportsman));
                    }
                }
                return _sportsman;
            }
            set
            {
                if (!Equals(value, _sportsman))
                {
                    _sportsman = value;
                    NotifyOfPropertyChange(() => Sportsman);
                }
            }
        }
    }
}