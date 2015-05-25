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
    class TrainerWorkViewModel : PropertyChangedBase
    {
        private readonly ISportsmanCrudService _sportsmanCrudService;
        private readonly ITrainerCrudService _trainerCrudService;

        public TrainerWorkViewModel(ISportsmanCrudService sportsmanCrudService, ITrainerCrudService trainerCrudService)
        {
            _sportsmanCrudService = sportsmanCrudService;
            _trainerCrudService = trainerCrudService;
            SelectSportsman = new SportsmanViewModel();
            NewTrainer = new TrainerViewModel();
            SelectTrainer = new TrainerViewModel();
            SportsmanList = new BindableCollection<SportsmanViewModel>();
            TrainerList = new BindableCollection<TrainerViewModel>();
            RefreshList();
            RefreshSportsmanList();
        }

        public IObservableCollection<SportsmanViewModel> SportsmanList { get; set; }
        public IObservableCollection<TrainerViewModel> TrainerList { get; set; }

        private SportsmanViewModel _selectSportsman;

        private TrainerViewModel _newTrainer;

        private TrainerViewModel _selectTrainer;

        public TrainerViewModel NewTrainer
        {
            get { return _newTrainer; }
            set
            {
                if (_newTrainer != value)
                {
                    _newTrainer = value;
                    NotifyOfPropertyChange(() => NewTrainer);
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

        public TrainerViewModel SelectTrainer
        {
            get { return _selectTrainer; }
            set
            {
                if (_selectTrainer != value)
                {
                    _selectTrainer = value;
                    NotifyOfPropertyChange(() => SelectTrainer);
                }
            }
        }


        public void RefreshList()
        {
            TrainerList.Clear();

            List<Trainer> list = new List<Trainer>(_trainerCrudService.GetAll());

            foreach (var data in list)
            {
                TrainerViewModel cvm = new TrainerViewModel(data);
                TrainerList.Add(cvm);
            }
            NotifyOfPropertyChange(() => TrainerList);
        }

        public void Add()
        {
            if (NewTrainer == null)
            {
                MessageBox.Show("Нужно запольнить поле.");
                return;
            }
            try
            {
                if (SelectSportsman == null || SelectSportsman.SportsmanEntity.Id == 0 || SelectSportsman.SportsmanEntity.Trainer.Count > 0)
                {
                    MessageBox.Show("Выберите спортсмена.");
                    return;
                }
                Mapper.CreateMap<TrainerViewModel, Trainer>();
                Trainer trainer = Mapper.Map<TrainerViewModel, Trainer>(NewTrainer);
                trainer.SportsmanId = SelectSportsman.SportsmanEntity.Id;
                _trainerCrudService.Create(trainer);
                RefreshList();
                NewTrainer = new TrainerViewModel();
                NotifyOfPropertyChange(() => NewTrainer);
                NotifyOfPropertyChange(() => TrainerList);
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
            if (SelectTrainer == null || SelectTrainer.TrainerEntity.Id == 0)
            {
                MessageBox.Show("Нужно заполнить поле.");
                return;
            }
            try
            {
                if (SelectSportsman == null || SelectSportsman.SportsmanEntity.Id == 0 || SelectSportsman.SportsmanEntity.Id == SelectTrainer.TrainerEntity.Sportsman.Id) 
                {
                    _trainerCrudService.Update(SelectTrainer.TrainerEntity);
                }
                else
                {
                    if (SelectSportsman.SportsmanEntity.Trainer.Count > 0)
                    {
                        MessageBox.Show("Выберите спортсмена.");
                        return;
                    }
                    SelectTrainer.TrainerEntity.SportsmanId = SelectSportsman.SportsmanEntity.Id;
                    _trainerCrudService.Update(SelectTrainer.TrainerEntity);
                }
                SelectTrainer = new TrainerViewModel();
                RefreshList();
                NotifyOfPropertyChange(() => SelectTrainer);
                NotifyOfPropertyChange(() => TrainerList);
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

        public void Delete()
        {
            if (SelectTrainer == null || SelectTrainer.TrainerEntity.Id == 0)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                if(SelectTrainer.TrainerEntity.Group.Count != 0 || SelectTrainer.TrainerEntity.Traning.Count != 0)
                {
                    MessageBox.Show("В группе остались туристы.");
                    return;
                }
                _trainerCrudService.Delete(SelectTrainer.TrainerEntity);
                TrainerList.Remove(SelectTrainer);
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.GetBaseException() as SqlException;

                if (sqlException != null)
                {
                    MessageBox.Show("Нереально");
                }
            }
            finally
            {
                SelectTrainer = new TrainerViewModel();
                NotifyOfPropertyChange(() => SelectTrainer);
                NotifyOfPropertyChange(() => TrainerList);
            }
        }
    }
}
