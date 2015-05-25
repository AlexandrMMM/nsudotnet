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
    class RoutePointCrudViewModel : PropertyChangedBase
    {
        private readonly IRoutePointCrudService _routePointCrudService;

        public RoutePointCrudViewModel(IRoutePointCrudService routePointCrudService)
        {
            _routePointCrudService = routePointCrudService;
            SelectRoutePoint = new RoutePointViewModel();
            NewRoutePoint = new RoutePointViewModel();
            RoutePointList = new BindableCollection<RoutePointViewModel>();
            RefreshList();
        }

        public IObservableCollection<RoutePointViewModel> RoutePointList { get; set; }

        private RoutePointViewModel _seletRoutePoint;

        private RoutePointViewModel _newRoutePoint;

        public RoutePointViewModel NewRoutePoint
        {
            get { return _newRoutePoint; }
            set
            {
                if (_newRoutePoint != value)
                {
                    _newRoutePoint = value;
                    NotifyOfPropertyChange(() => NewRoutePoint);
                }
            }
        }

        public RoutePointViewModel SelectRoutePoint
        {
            get { return _seletRoutePoint; }
            set
            {
                if (_seletRoutePoint != value)
                {
                    _seletRoutePoint = value;
                    NotifyOfPropertyChange(() => SelectRoutePoint);
                }
            }
        }

        public void Add()
        {
            if (NewRoutePoint == null)
            {
                MessageBox.Show("Нужно запольнить все поля.");
                return;
            }
            try
            {
                if (NewRoutePoint.Name == null || (NewRoutePoint.Name.Length > 30 || NewRoutePoint.Name.Length < 1))
                {
                    MessageBox.Show("Длина имени не может быть больше 30 символов и меньше 1.");
                    return;
                }
                Mapper.CreateMap<RoutePointViewModel, RoutePoint>();
                _routePointCrudService.Create(Mapper.Map<RoutePointViewModel, RoutePoint>(NewRoutePoint));
                RefreshList();
                NewRoutePoint = new RoutePointViewModel();
                NotifyOfPropertyChange(() => NewRoutePoint);
                NotifyOfPropertyChange(() => RoutePointList);
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
            if (SelectRoutePoint == null || SelectRoutePoint.RoutePointEntity.Id == 0)
            {
                MessageBox.Show("Нужно запольнить все поля.");
                return;
            }
            try
            {
                if (SelectRoutePoint.Name == null || (SelectRoutePoint.Name.Length > 30 || SelectRoutePoint.Name.Length < 1))
                {
                    MessageBox.Show("Длина имени не может быть больше 30 символов и меньше 1.");
                    return;
                }
                _routePointCrudService.Update(SelectRoutePoint.RoutePointEntity);
                SelectRoutePoint = new RoutePointViewModel();
                RefreshList();
                NotifyOfPropertyChange(() => SelectRoutePoint);
                NotifyOfPropertyChange(() => RoutePointList);
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
            RoutePointList.Clear();

            List<RoutePoint> list = new List<RoutePoint>(_routePointCrudService.GetAll());

            foreach (var data in list)
            {
                RoutePointViewModel cvm = new RoutePointViewModel(data);
                RoutePointList.Add(cvm);
            }
            NotifyOfPropertyChange(() => RoutePointList);
        }

        public void Delete()
        {
            if (SelectRoutePoint == null || SelectRoutePoint.RoutePointEntity.Id == 0)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                if (SelectRoutePoint.RoutePointEntity.Campaign.Count == 0 && SelectRoutePoint.RoutePointEntity.Stop.Count == 0)
                {
                    _routePointCrudService.Delete(SelectRoutePoint.RoutePointEntity);
                    RoutePointList.Remove(SelectRoutePoint);
                }
                else
                {
                    MessageBox.Show("Дневник содержит Походы и Остановки");
                }
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
                SelectRoutePoint = new RoutePointViewModel();
                NotifyOfPropertyChange(() => SelectRoutePoint);
                NotifyOfPropertyChange(() => RoutePointList);
            }
        }
    }
}

