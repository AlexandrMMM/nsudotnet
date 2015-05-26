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
            SelectRoutePoint = new RoutePointViewModel();
            SelectDiary = new DiaryViewModel();
            SelectStop = new StopViewModel();
            NewStop = new StopViewModel();
            RoutePointList = new BindableCollection<RoutePointViewModel>();
            StopList = new BindableCollection<StopViewModel>();
            DiaryList = new BindableCollection<DiaryViewModel>();
            RefreshList();
            RefreshDiaryList();
            RefreshRoutePointList();
        }

        public IObservableCollection<RoutePointViewModel> RoutePointList { get; set; }
        public IObservableCollection<StopViewModel> StopList { get; set; }
        public IObservableCollection<DiaryViewModel> DiaryList { get; set; }

        private StopViewModel _newStop;

        private StopViewModel _selectStop;

        private DiaryViewModel _selectDiary;

        private RoutePointViewModel _selectRoutePoint;

        public StopViewModel NewStop
        {
            get { return _newStop; }
            set
            {
                if (_newStop != value)
                    {
                        _newStop = value;
                        NotifyOfPropertyChange(() => NewStop);
                    }
            }
        }

        public StopViewModel SelectStop
       {
           get { return _selectStop; }
           set
           {
               if (_selectStop != value)
               {
                   _selectStop = value;
                   NotifyOfPropertyChange(() => SelectStop);
               }
           }
       }

        public DiaryViewModel SelectDiary
            {
                get { return _selectDiary; }
                set
                {
                    if (_selectDiary != value)
                    {
                        _selectDiary = value;
                        NotifyOfPropertyChange(() => SelectDiary);
                    }
                }
            }

        public RoutePointViewModel SelectRoutePoint
        {
            get { return _selectRoutePoint; }
            set
            {
                if (_selectRoutePoint != value)
                {
                    _selectRoutePoint = value;
                    NotifyOfPropertyChange(() => SelectRoutePoint);
                }
            }
        }

        public void RefreshDiaryList()
            {
                DiaryList.Clear();

                List<Diary> list = new List<Diary>(_diaryCrudService.GetAll());

                foreach (var data in list)
                {
                    DiaryViewModel cvm = new DiaryViewModel(data);
                    DiaryList.Add(cvm);
                }
                NotifyOfPropertyChange(() => DiaryList);
            }

        public void RefreshRoutePointList()
            {
                RoutePointList.Clear();

                List<RoutePoint> list = new List<RoutePoint>(_routePointCrudService.GetAll());

                foreach (var data in list)
                {
                    RoutePointViewModel cvm = new RoutePointViewModel(data);
                    RoutePointList.Add(cvm);
                }
                NotifyOfPropertyChange(() => RoutePointList);
            }

            public void Add()
            {
                if (NewStop == null)
                {
                    MessageBox.Show("Нужно запольнить поле.");
                    return;
                }
                try
                {
                    if (SelectRoutePoint == null || SelectRoutePoint.RoutePointEntity == null ||
                        SelectRoutePoint.RoutePointEntity.Id == 0 || SelectDiary == null || SelectRoutePoint.RoutePointEntity == null
                        || SelectDiary.DiaryEntity.Id == 0 || NewStop.DateTime.Year < 1800)
                    {
                        MessageBox.Show("Выберите время, план и точку.");
                        return;
                    }
                    Mapper.CreateMap<StopViewModel, Stop>();
                    Stop stop = Mapper.Map<StopViewModel, Stop>(NewStop);
                    stop.RoutePointId = SelectRoutePoint.RoutePointEntity.Id;
                    stop.DiaryId = SelectDiary.DiaryEntity.Id;
                    _stopCrudService.Create(stop);
                    RefreshList();
                    NewStop = new StopViewModel();
                    NotifyOfPropertyChange(() => NewStop);
                    NotifyOfPropertyChange(() => StopList);
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;

                    if (sqlException != null)
                    {
                        MessageBox.Show("Невозможно создать запись");
                    }
                }
            }

            public void Update()
            {
                if (SelectStop == null || SelectStop.StopEntity == null || SelectStop.StopEntity.Id == 0)
                {
                    MessageBox.Show("Нужно заполнить поле.");
                    return;
                }
                try
                {
                    if ((SelectRoutePoint == null || SelectRoutePoint.RoutePointEntity == null ||
                        SelectRoutePoint.RoutePointEntity.Id == 0 || SelectDiary == null || SelectRoutePoint.RoutePointEntity == null
                        || SelectDiary.DiaryEntity.Id == 0))
                    {
                        MessageBox.Show("Выберите время, секцию и персональные данные.");
                        return;
                    }
                    SelectStop.StopEntity.RoutePointId = SelectRoutePoint.RoutePointEntity.Id;
                    SelectStop.StopEntity.DiaryId = SelectDiary.DiaryEntity.Id;
                    _stopCrudService.Update(SelectStop.StopEntity);
                    SelectStop = new StopViewModel();
                    RefreshList();
                    NotifyOfPropertyChange(() => SelectStop);
                    NotifyOfPropertyChange(() => StopList);
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;

                    if (sqlException != null)
                    {
                        MessageBox.Show("Невозможно изменить запись");
                    }
                }
            }

            public void RefreshList()
            {
                StopList.Clear();

                List<Stop> list = new List<Stop>(_stopCrudService.GetAll());

                foreach (var data in list)
                {
                    StopViewModel cvm = new StopViewModel(data);
                    StopList.Add(cvm);
                }
                NotifyOfPropertyChange(() => StopList);
            }

            public void Delete()
            {
                if (SelectStop == null || SelectStop.StopEntity == null || SelectStop.StopEntity.Id == 0)
                {
                    MessageBox.Show("Выберите запись");
                    return;
                }
                try
                {
                    _stopCrudService.Delete(SelectStop.StopEntity);
                    StopList.Remove(SelectStop);
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;

                    if (sqlException != null)
                    {
                        MessageBox.Show(
                            "Нереально");
                    }
                }
                finally
                {
                    SelectStop = new StopViewModel();
                    NotifyOfPropertyChange(() => SelectStop);
                    NotifyOfPropertyChange(() => StopList);
                }
            }
    }
}
