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
    class CampaignTypeViewModel : PropertyChangedBase
    {
        private ICollection<SectionViewModel> _section;

        public CampaignTypeViewModel()
        {
            CampaignTypeEntity = new CampaignType();
        }

        public CampaignTypeViewModel(CampaignType campaignTypeEntity)
        {
            CampaignTypeEntity = campaignTypeEntity;
        }

        public CampaignType CampaignTypeEntity{ get; set; }

        public string Name
        {
            get { return CampaignTypeEntity.Name; }
            set
            {
                if (value == CampaignTypeEntity.Name)
                    return;
                CampaignTypeEntity.Name = value;
                NotifyOfPropertyChange(() => Name);
            } 
        }

        public ICollection<SectionViewModel> Section
        {
            get
            {
                if (_section == null)
                {
                    _section = new ObservableCollection<SectionViewModel>();
                    foreach (var section in CampaignTypeEntity.Section)
                    {
                        _section.Add(new SectionViewModel(section));
                    }
                }
                return _section;
            }
            set
            {
                if (!Equals(value, _section))
                {
                    _section = value;
                    NotifyOfPropertyChange(() => Section);
                }
            }
        }
    }
}