using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TouristClub.Logic.Interface;

namespace TouristClub.UI.ViewModels
{
    class StopCrudViewModel : PropertyChangedBase
    {
        private readonly IDiaryCrudService _diaryCrudService;
        private readonly IRoutePointCrudService _routePointCrudService;
        private readonly IStopCrudService _stopCrudService;

        public StopCrudViewModel(IDiaryCrudService diaryCrudService, IRoutePointCrudService routePointCrudService,
            IStopCrudService stopCrudService)
        {
            _diaryCrudService = diaryCrudService;
            _routePointCrudService = routePointCrudService;
            _stopCrudService = stopCrudService;
        }
    }
}
