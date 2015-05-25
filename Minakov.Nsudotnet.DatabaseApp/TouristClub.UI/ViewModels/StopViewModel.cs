using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TouristClub.Data.Entity;

namespace TouristClub.UI.ViewModels
{
    class StopViewModel : PropertyChangedBase
    {
        private RoutePointViewModel _routePointViewModel;
        private DiaryViewModel _diaryViewModel;

        public StopViewModel(Stop stopEntity)
        {
            StopEntity = stopEntity;
            _routePointViewModel = new RoutePointViewModel(StopEntity.RoutePoint);
            _diaryViewModel = new DiaryViewModel(StopEntity.Diary);
        }

        public Stop StopEntity { get; private set; }

        public System.DateTime DateTime
        {
            get { return StopEntity.DateTime; }
            set
            {
                if (value == StopEntity.DateTime)
                    return;
                StopEntity.DateTime = value;
                NotifyOfPropertyChange(() => DateTime);
            }
        }
        public int StopTimeInMinutes
        {
            get { return StopEntity.StopTimeInMinutes; }
            set
            {
                if (value == StopEntity.StopTimeInMinutes)
                    return;
                StopEntity.StopTimeInMinutes = value;
                NotifyOfPropertyChange(() => StopTimeInMinutes);
            }
        }
        public RoutePointViewModel RoutePoint
        {
            get
            {
                return _routePointViewModel;
            }
            set
            {
                if (value != _routePointViewModel)
                {
                    _routePointViewModel = value;
                    StopEntity.RoutePointId = _routePointViewModel.RoutePointEntity.Id;
                    StopEntity.RoutePoint = _routePointViewModel.RoutePointEntity;
                    NotifyOfPropertyChange(() => RoutePoint);
                }
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
                    StopEntity.DiaryId = _diaryViewModel.DiaryEntity.Id;
                    StopEntity.Diary = _diaryViewModel.DiaryEntity;
                    NotifyOfPropertyChange(() => Diary);
                }
            }
        }
    }
}