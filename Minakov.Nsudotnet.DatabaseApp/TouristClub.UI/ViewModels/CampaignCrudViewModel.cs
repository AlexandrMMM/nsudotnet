using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using Caliburn.Micro;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.UI.ViewModels
{
    class CampaignCrudViewModel : PropertyChangedBase
    {
        private readonly ICampaignCrudService _campaignCrudService;
        private readonly ICampaignTypeCrudService _campaignTypeCrudService;
        private readonly IRoutePointCrudService _routePointCrudService;
        private readonly IPersonalDataCrudService _personalDataCrudService;
        private readonly ICategoryCrudService _categoryCrudService;
        private readonly IDiaryCrudService _diaryCrudService;
        private readonly ISportsmanCrudService _sportsmanCrudService;

        public CampaignCrudViewModel(ICampaignCrudService campaignCrudService,
            ICampaignTypeCrudService campaignTypeCrudService, IRoutePointCrudService routePointCrudService, IPersonalDataCrudService personalDataCrudService,
            ICategoryCrudService categoryCrudService, IDiaryCrudService diaryCrudService, ISportsmanCrudService sportsmanCrudService)
        {
            /*_campaignCrudService = campaignCrudService;
            _campaignTypeCrudService = campaignTypeCrudService;
            _routePointCrudService = routePointCrudService;
            _personalDataCrudService = personalDataCrudService;
            _categoryCrudService = categoryCrudService;
            _diaryCrudService = diaryCrudService;
            _sportsmanCrudService = sportsmanCrudService;

            SelectCampaign = new CampaignViewModel();
            NewCampaign = new CampaignViewModel();

            SelectRoutePoint = new RoutePointViewModel();
            SelectDiary = new DiaryViewModel();
            SelectCategory = new CategoryViewModel();
            SelectCampaignType = new CampaignTypeViewModel();
            SelectSportsman = new SportsmanViewModel();
            SelectPersonalData = new PersonalDataViewModel();

            CampaignTypeList = new BindableCollection<CampaignTypeViewModel>();
            CategoryList = new BindableCollection<CategoryViewModel>();
            RoutePointList = new BindableCollection<RoutePointViewModel>();
            DiaryList = new BindableCollection<DiaryViewModel>();
            SportsmanList = new BindableCollection<SportsmanViewModel>();
            PersonalDataList = new BindableCollection<PersonalDataViewModel>();

            RefreshRoutePointList();
            RefreshDiaryist();
            RefreshCategoryList();
            RefreshCampaignTypeList();
            RefreshSportsmanList();
            RefreshPersonalDataList();
            RefreshList();*/
        }

  
    }
}
