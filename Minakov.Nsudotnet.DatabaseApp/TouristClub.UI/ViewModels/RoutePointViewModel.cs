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
    class RoutePointViewModel : PropertyChangedBase
    {
        private ICollection<CampaignViewModel> _campaign;

        public RoutePointViewModel()
        {
            RoutePointEntity = new RoutePoint();
        }

        public RoutePointViewModel(RoutePoint routePointEntity)
        {
            RoutePointEntity = routePointEntity;
        }

        public RoutePoint RoutePointEntity { get; private set; }

        public string Name 
        {
            get { return RoutePointEntity.Name; } 
            set
            {
                if (value == RoutePointEntity.Name)
                    return;
                RoutePointEntity.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
        public ICollection<CampaignViewModel> Campaign
        {
            get
            {
                if (_campaign == null)
                {
                    _campaign = new ObservableCollection<CampaignViewModel>();
                    foreach (var campaign in RoutePointEntity.Campaign)
                    {
                        _campaign.Add(new CampaignViewModel(campaign));
                    }
                }
                return _campaign;
            }
            set
            {
                if (!Equals(value, _campaign))
                {
                    _campaign = value;
                    NotifyOfPropertyChange(() => Campaign);
                }
            }
        }
    }
}