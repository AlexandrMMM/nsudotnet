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
    class TrainingGrudViewModel : PropertyChangedBase
    {
        private readonly ITrainerCrudService _trainerCrudService;
        private readonly ITrainigCrudService _trainigCrudService;

        public TrainingGrudViewModel(ITrainerCrudService trainerCrudService, ITrainigCrudService trainigCrudService)
        {
            _trainerCrudService = trainerCrudService;
            _trainigCrudService = trainigCrudService;
           
            SelectTrainig = new TraningViewModel();
            NewTrainig = new TraningViewModel();
            SelectTrainer = new TrainerViewModel();

            TraningList = new BindableCollection<TraningViewModel>();
            TrainerList = new BindableCollection<TrainerViewModel>();
            RefreshList();
            RefreshTrainerList();
        }

        public IObservableCollection<TraningViewModel> TraningList { get; set; }
        public IObservableCollection<TrainerViewModel> TrainerList { get; set; }


        private TraningViewModel _selectTraning;

        private TraningViewModel _newTraining;

        private TrainerViewModel _selectTrainer;

        public TraningViewModel NewTrainig
        {
            get { return _newTraining; }
            set
            {
                if (_newTraining != value)
                {
                    _newTraining = value;
                    NotifyOfPropertyChange(() => NewTrainig);
                }
            }
        }

        public TraningViewModel SelectTrainig
        {
            get { return _selectTraning; }
            set
            {
                if (_selectTraning != value)
                {
                    _selectTraning = value;
                    NotifyOfPropertyChange(() => SelectTrainig);
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

        public void RefreshTrainerList()
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
            if (NewTrainig == null || NewTrainig.TreningTimeInMinutes == 0)
            {
                MessageBox.Show("Нужно запольнить поле.");
                return;
            }
            try
            {
                if (NewTrainig.Place == null || NewTrainig.Place.Length > 30 || NewTrainig.Place.Length < 1)
                {
                    MessageBox.Show("Названи места не может быть больше 30 букв и пустым.");
                    return;
                }
                if (SelectTrainer.TrainerEntity.Id == 0)
                {
                    MessageBox.Show("Выберите тренера.");
                    return;
                }
                Mapper.CreateMap<TraningViewModel, Traning>();
                Traning traning = Mapper.Map<TraningViewModel, Traning>(NewTrainig);
                traning.TrainerId = SelectTrainer.TrainerEntity.Id;
                _trainigCrudService.Create(traning);
                RefreshList();
                NewTrainig = new TraningViewModel();
                NotifyOfPropertyChange(() => NewTrainig);
                NotifyOfPropertyChange(() => TraningList);
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
            if (SelectTrainig == null || SelectTrainig.TraningEntity.Id == 0 || SelectTrainig.TreningTimeInMinutes == 0)
            {
                MessageBox.Show("Нужно заполнить поле.");
                return;
            }
            try
            {
                if (SelectTrainig.Place == null || SelectTrainig.Place.Length > 30 || SelectTrainig.Place.Length < 1)
                {
                    MessageBox.Show("Название не может быть больше 30 букв и пустым.");
                    return;
                }
                if (SelectTrainer.TrainerEntity.Id == 0)
                {
                    MessageBox.Show("Выберите тренера.");
                    return;
                }
                SelectTrainig.TraningEntity.TrainerId = SelectTrainer.TrainerEntity.Id;
                _trainigCrudService.Update(SelectTrainig.TraningEntity);
                SelectTrainig = new TraningViewModel();
                RefreshList();
                NotifyOfPropertyChange(() => SelectTrainig);
                NotifyOfPropertyChange(() => TraningList);
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
            TraningList.Clear();

            List<Traning> list = new List<Traning>(_trainigCrudService.GetAll());

            foreach (var data in list)
            {
                TraningViewModel cvm = new TraningViewModel(data);
                TraningList.Add(cvm);
            }
            NotifyOfPropertyChange(() => TraningList);
        }

        public void Delete()
        {
            if (SelectTrainig == null || SelectTrainig.TraningEntity.Id == 0)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                _trainigCrudService.Delete(SelectTrainig.TraningEntity);
                TraningList.Remove(SelectTrainig);
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
                SelectTrainig = new TraningViewModel();
                NotifyOfPropertyChange(() => SelectTrainig);
                NotifyOfPropertyChange(() => TraningList);
            }
        }
    }
}
