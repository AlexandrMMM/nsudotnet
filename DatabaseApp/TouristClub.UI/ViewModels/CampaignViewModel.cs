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
    class CampaignViewModel : PropertyChangedBase
    {
        private ICollection<RoutePointViewModel> _routePoint;
        private ICollection<PersonalDataViewModel> _personalData;
        private CategoryViewModel _categoryViewModel;
        private CampaignTypeViewModel _campaignTypeViewModel;
        private DiaryViewModel _diaryViewModel;
        private SportsmanViewModel _sportsmanViewModel;

        public CampaignViewModel()
        {
            CampaignEntity = new Campaign();
        }

        public CampaignViewModel(Campaign campaignEntity)
        {
            CampaignEntity = campaignEntity;
            _categoryViewModel = new CategoryViewModel(CampaignEntity.Category);
            _diaryViewModel = new DiaryViewModel(CampaignEntity.Diary);
            _sportsmanViewModel = new SportsmanViewModel(CampaignEntity.Sportsman);
            _campaignTypeViewModel = new CampaignTypeViewModel(CampaignEntity.CampaignType);
        }

        public Campaign CampaignEntity { get; private set; }

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
                    CampaignEntity.CategoryId = _categoryViewModel.CategoryEntity.Id;
                    CampaignEntity.Category = _categoryViewModel.CategoryEntity;
                    NotifyOfPropertyChange(() => Category);
                }
            }
        }
        public System.DateTime StartDateTime
        {
            get { return CampaignEntity.StartDateTime; }
            set
            {
                if (value == CampaignEntity.StartDateTime)
                    return;
                CampaignEntity.StartDateTime = value;
                NotifyOfPropertyChange(() => StartDateTime);
            }
        }
        public DiaryViewModel Diary
        {
            get
            {
                return _diaryViewModel;
            }
            set
            {
                if (value != _diaryViewModel)
                {
                    _diaryViewModel = value;
                    CampaignEntity.DiaryId = _diaryViewModel.DiaryEntity.Id;
                    CampaignEntity.Diary = _diaryViewModel.DiaryEntity;
                    NotifyOfPropertyChange(() => Diary);
                }
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
                    CampaignEntity.SportsmanId = _sportsmanViewModel.SportsmanEntity.Id;
                    CampaignEntity.Sportsman = _sportsmanViewModel.SportsmanEntity;
                    NotifyOfPropertyChange(() => Diary);
                }
            }
        }
        public int CampaignTimeinHour
        {
            get { return CampaignEntity.CampaignTimeinHour; }
            set
            {
                if (value == CampaignEntity.CampaignTimeinHour)
                    return;
                CampaignEntity.CampaignTimeinHour = value;
                NotifyOfPropertyChange(() => CampaignTimeinHour);
            }
        }
        public CampaignTypeViewModel CampaignType
        {
            get
            {
                return _campaignTypeViewModel;
            }
            set
            {
                if (value != _campaignTypeViewModel)
                {
                    _campaignTypeViewModel = value;
                    CampaignEntity.CampaignTypeId = _campaignTypeViewModel.CampaignTypeEntity.Id;
                    CampaignEntity.CampaignType = _campaignTypeViewModel.CampaignTypeEntity;
                    NotifyOfPropertyChange(() => Diary);
                }
            }
        }

        public ICollection<RoutePointViewModel> RoutePoint
        {
            get
            {
                if (_routePoint == null)
                {
                    _routePoint = new ObservableCollection<RoutePointViewModel>();
                    foreach (var routePoint in CampaignEntity.RoutePoint)
                    {
                        _routePoint.Add(new RoutePointViewModel(routePoint));
                    }
                }
                return _routePoint;
            }
            set
            {
                if (!Equals(value, _routePoint))
                {
                    _routePoint = value;
                    NotifyOfPropertyChange(() => RoutePoint);
                }
            }
        }
        public ICollection<PersonalDataViewModel> PersonalData
        {
            get
            {
                if (_personalData == null)
                {
                    _personalData = new ObservableCollection<PersonalDataViewModel>();
                    foreach (var personalData in CampaignEntity.PersonalData)
                    {
                        _personalData.Add(new PersonalDataViewModel(personalData));
                    }
                }
                return _personalData;
            }
            set
            {
                if (!Equals(value, _personalData))
                {
                    _personalData = value;
                    NotifyOfPropertyChange(() => PersonalData);
                }
            }
        }
    }
}