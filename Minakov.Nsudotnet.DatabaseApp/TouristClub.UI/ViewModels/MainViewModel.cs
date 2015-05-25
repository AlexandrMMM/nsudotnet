using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Animation;
using Autofac;
using Caliburn.Micro;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.UI.ViewModels
{
    class MainViewModel : Screen
    {
        private ICampaignCrudService _campaignCrudService;
        private ICampaignTypeCrudService _campaignTypeCrudService;
        private ICategoryCrudService _categoryCrudService;
        private ICompetitionCrudService _competitionCrudService;
        private IDiaryCrudService _diaryCrudService;
        private IHeadCrudService _headCrudService;
        private IGroupCrudService _groupCrudService;
        private IPersonalDataCrudService _personalDataCrudService;
        private IRoutePointCrudService _routePointCrudService;
        private ISectionCrudService _sectionCrudService;
        private ISportsmanCrudService _sportsmanCrudService;
        private IStopCrudService _stopCrudService;
        private ITouristCrudService _touristCrudService;
        private ITrainerCrudService _trainerCrudService;
        private ITrainigCrudService _trainigCrudService;

        public CategoryCrudViewModel CategoryCrudViewModel { get; set; }
        public PersonalDataCrudViewModel PersonalDataViewModel { get; set; }
        public DiaryCrudViewModel DiaryCrudViewModel { get; set; }
        public SectionCrudViewModel SectionCrudViewModel { get; set; }
        public CampaignTypeCrudViewModel CampaignTypeCrudViewModel { get; set; }

        public MainViewModel(ICampaignCrudService campaignCrudService, ICampaignTypeCrudService campaignTypeCrudService,
            ICategoryCrudService categoryCrudService, ICompetitionCrudService competitionCrudService,
            IDiaryCrudService diaryCrudService, IGroupCrudService groupCrudService,
            IHeadCrudService headCrudService, IPersonalDataCrudService personalDataCrudService,
            IRoutePointCrudService routePointCrudService, ISectionCrudService sectionCrudService,
            ISportsmanCrudService sportsmanCrudService, IStopCrudService stopCrudService,
            ITouristCrudService touristCrudService, ITrainerCrudService trainerCrudService,
            ITrainigCrudService trainigCrudService)
        {
            _categoryCrudService = categoryCrudService;
            _stopCrudService = stopCrudService;
            _touristCrudService = touristCrudService;
            _trainerCrudService = trainerCrudService;
            _trainigCrudService = trainigCrudService;
            _campaignCrudService = campaignCrudService;
            _campaignTypeCrudService = campaignTypeCrudService;
            _competitionCrudService = competitionCrudService;
            _sportsmanCrudService = sportsmanCrudService;
            _diaryCrudService = diaryCrudService;
            _groupCrudService = groupCrudService;
            _headCrudService = headCrudService;
            _personalDataCrudService = personalDataCrudService;
            _routePointCrudService = routePointCrudService;
            _sectionCrudService = sectionCrudService;

            CategoryCrudViewModel = new CategoryCrudViewModel(_categoryCrudService);
            PersonalDataViewModel = new PersonalDataCrudViewModel(_personalDataCrudService);
            DiaryCrudViewModel = new DiaryCrudViewModel(_diaryCrudService);
            CampaignTypeCrudViewModel = new CampaignTypeCrudViewModel(_campaignTypeCrudService, _sectionCrudService);
            SectionCrudViewModel = new SectionCrudViewModel(_sectionCrudService, _campaignTypeCrudService);
        }
    }
}