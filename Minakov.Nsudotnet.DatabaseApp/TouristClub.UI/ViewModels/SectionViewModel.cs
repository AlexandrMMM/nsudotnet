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
    internal class SectionViewModel : PropertyChangedBase
    {
        private ICollection<CampaignTypeViewModel> _campaignType;

        public SectionViewModel()
        {
            SectionEntity = new Section();
        }

        public SectionViewModel(Section sectionEntity)
        {
            SectionEntity = sectionEntity;
        }

        public Section SectionEntity { get; private set; }

        public string Name
        {
            get { return SectionEntity.Name; }
            set
            {
                if (value == SectionEntity.Name)
                    return;
                SectionEntity.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public ICollection<CampaignTypeViewModel> CampaignType
        {
            get
            {
                if (_campaignType == null)
                {
                    _campaignType = new ObservableCollection<CampaignTypeViewModel>();
                    foreach (var campaignType in SectionEntity.CampaignType)
                    {
                        _campaignType.Add(new CampaignTypeViewModel(campaignType));
                    }
                }
                return _campaignType;
            }
            set
            {
                if (!Equals(value, _campaignType))
                {
                    _campaignType = value;
                    NotifyOfPropertyChange(() => CampaignType);
                }
            }
        }
    }
}