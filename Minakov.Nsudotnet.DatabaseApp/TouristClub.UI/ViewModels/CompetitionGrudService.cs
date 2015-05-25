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
    class CompetitionGrudService : PropertyChangedBase
    {
        private readonly ISportsmanCrudService _sportsmanCrudService;
        private readonly ICompetitionCrudService _competitionCrudService;

        public CompetitionGrudService(ISportsmanCrudService sportsmanCrudService, ICompetitionCrudService competitionCrudService)
        {
            _sportsmanCrudService = sportsmanCrudService;
            _competitionCrudService = competitionCrudService;
            SelectCompetition = new CompetitionViewModel();
            NewCompetition = new CompetitionViewModel();
            CompetitionList = new BindableCollection<CompetitionViewModel>();
            SportsmanList = new BindableCollection<SportsmanViewModel>();
            RefreshList();
            RefreshSportsmanList();
        }

        public IObservableCollection<CompetitionViewModel> CompetitionList { get; set; }

        public IObservableCollection<SportsmanViewModel> SportsmanList { get; set; }

        private SportsmanViewModel _selectSportsman;

        private CompetitionViewModel _selectCompetition;

        private CompetitionViewModel _newCompetition;

        public CompetitionViewModel NewCompetition
        {
            get { return _newCompetition; }
            set
            {
                if (_newCompetition != value)
                {
                    _newCompetition = value;
                    NotifyOfPropertyChange(() => NewCompetition);
                }
            }
        }

        public CompetitionViewModel SelectCompetition
        {
            get { return _selectCompetition; }
            set
            {
                if (_selectCompetition != value)
                {
                    _selectCompetition = value;
                    NotifyOfPropertyChange(() => SelectCompetition);
                }
            }
        }

        public SportsmanViewModel SelectSportsman
        {
            get { return _selectSportsman; }
            set
            {
                if (_selectSportsman != value)
                {
                    _selectSportsman = value;
                    NotifyOfPropertyChange(() => SelectSportsman);
                }
            }
        }

        public void RefreshSportsmanList()
        {
            SportsmanList.Clear();

            List<Sportsman> list = new List<Sportsman>(_sportsmanCrudService.GetAll());

            foreach (var data in list)
            {
                SportsmanViewModel cvm = new SportsmanViewModel(data);
                SportsmanList.Add(cvm);
            }
            NotifyOfPropertyChange(() => SportsmanList);
        }

        public void Add()
        {
            if (NewCompetition == null)
            {
                MessageBox.Show("Нужно запольнить поле.");
                return;
            }
            try
            {
                if (NewCompetition.Name == null || (NewCompetition.Name.Length > 30 || NewCompetition.Name.Length < 1))
                {
                    MessageBox.Show("Длина имени не может быть больше 30 символов и меньше 1.");
                    return;
                }
                Mapper.CreateMap<CompetitionViewModel, Competition>();
                _competitionCrudService.Create(Mapper.Map<CompetitionViewModel, Competition>(NewCompetition));
                RefreshList();
                NewCompetition = new CompetitionViewModel();
                NotifyOfPropertyChange(() => NewCompetition);
                NotifyOfPropertyChange(() => CompetitionList);
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
            if (SelectCompetition == null || SelectCompetition.CompetitionEntity.Id == 0)
            {
                MessageBox.Show("Нужно заполнить поле.");
                return;
            }
            try
            {
                if (SelectCompetition.Name == null || (SelectCompetition.Name.Length > 30 || SelectCompetition.Name.Length < 1))
                {
                    MessageBox.Show("Длина имени не может быть больше 30 символов и меньше 1.");
                    return;
                }
                _competitionCrudService.Update(SelectCompetition.CompetitionEntity);
                SelectCompetition = new CompetitionViewModel();
                RefreshList();
                NotifyOfPropertyChange(() => SelectCompetition);
                NotifyOfPropertyChange(() => CompetitionList);
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
            CompetitionList.Clear();

            List<Competition> list = new List<Competition>(_competitionCrudService.GetAll());

            foreach (var data in list)
            {
                CompetitionViewModel cvm = new CompetitionViewModel(data);
                CompetitionList.Add(cvm);
            }
            NotifyOfPropertyChange(() => CompetitionList);
        }

        public void Delete()
        {
            if (SelectCompetition == null || SelectCompetition.CompetitionEntity.Id == 0)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                if (SelectCompetition.CompetitionEntity.Sportsman.Count == 0)
                {
                    _competitionCrudService.Delete(SelectCompetition.CompetitionEntity);
                    CompetitionList.Remove(SelectCompetition);
                }
                else
                {
                    MessageBox.Show("В соревнование участвует российский спортсмен.");
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
                SelectCompetition = new CompetitionViewModel();
                NotifyOfPropertyChange(() => SelectCompetition);
                NotifyOfPropertyChange(() => CompetitionList);
            }
        }

        public void SportsmanInCompetition()
        {
            if (SelectCompetition == null || SelectCompetition.Sportsman == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }

            SportsmanList.Clear();

            foreach (var data in SelectCompetition.Sportsman)
            {
                SportsmanList.Add(data);
            }
            NotifyOfPropertyChange(() => SportsmanList);
        }

        public void DeleteConnection()
        {
            if (SelectCompetition == null || SelectCompetition.CompetitionEntity.Id == 0)
            {
                MessageBox.Show("Нужно состязание");
                return;
            }
            if (SelectSportsman == null || SelectSportsman.SportsmanEntity.Id == 0)
            {
                MessageBox.Show("Нужно выбрать спортсмена");
                return;
            }
            SelectCompetition.CompetitionEntity.Sportsman.Remove(SelectSportsman.SportsmanEntity);
            Update();
            RefreshSportsmanList();
        }

        public void AddConnection()
        {
            if (SelectCompetition == null || SelectCompetition.CompetitionEntity.Id == 0)
            {
                MessageBox.Show("Нужно состязание");
                return;
            }
            if (SelectSportsman == null || SelectSportsman.SportsmanEntity.Id == 0)
            {
                MessageBox.Show("Нужно выбрать спортсмена");
                return;
            }
            SelectCompetition.CompetitionEntity.Sportsman.Add(SelectSportsman.SportsmanEntity);
            Update();
            RefreshSportsmanList();
        }
    }
}
