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
    class GroupCrudViewModel : PropertyChangedBase
    {
        private readonly IGroupCrudService _groupCrudService;
        private readonly ISectionCrudService _sectionCrudService;
        private readonly ITrainerCrudService _trainerCrudService;

        public GroupCrudViewModel(IGroupCrudService groupCrudService, ISectionCrudService sectionCrudService, ITrainerCrudService trainerCrudService)
        {
            _sectionCrudService = sectionCrudService;
            _groupCrudService = groupCrudService;
            _trainerCrudService = trainerCrudService;
            SelectGroup = new GroupViewModel();
            NewGroup = new GroupViewModel();
            SelectSection = new SectionViewModel();
            SelectTrainer = new TrainerViewModel();
            SectionList = new BindableCollection<SectionViewModel>();
            GroupList = new BindableCollection<GroupViewModel>();
            TrainerList = new BindableCollection<TrainerViewModel>();
            RefreshList();
            RefreshSectionList();
            RefreshTrainerList();
        }

        public IObservableCollection<SectionViewModel> SectionList { get; set; }
        public IObservableCollection<GroupViewModel> GroupList { get; set; }
        public IObservableCollection<TrainerViewModel> TrainerList { get; set; }

        private SectionViewModel _selectSection;

        private GroupViewModel _selectGroup;

        private GroupViewModel _newGroup;

        private TrainerViewModel _selectTrainer;

        public GroupViewModel NewGroup
        {
            get { return _newGroup; }
            set
            {
                if (_newGroup != value)
                {
                    _newGroup = value;
                    NotifyOfPropertyChange(() => NewGroup);
                }
            }
        }

        public GroupViewModel SelectGroup
        {
            get { return _selectGroup; }
            set
            {
                if (_selectGroup != value)
                {
                    _selectGroup = value;
                    NotifyOfPropertyChange(() => SelectGroup);
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

        public SectionViewModel SelectSection
        {
            get { return _selectSection; }
            set
            {
                if (_selectSection != value)
                {
                    _selectSection = value;
                    NotifyOfPropertyChange(() => SelectSection);
                }
            }
        }

        public void RefreshSectionList()
        {
            SectionList.Clear();

            List<Section> list = new List<Section>(_sectionCrudService.GetAll());

            foreach (var data in list)
            {
                SectionViewModel cvm = new SectionViewModel(data);
                SectionList.Add(cvm);
            }
            NotifyOfPropertyChange(() => SectionList);
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
            if (NewGroup == null)
            {
                MessageBox.Show("Нужно запольнить поле.");
                return;
            }
            try
            {
                if (NewGroup.Name == null || NewGroup.Name.Length > 30 || NewGroup.Name.Length < 1)
                {
                    MessageBox.Show("Имя не может быть больше 30 букв и пустым.");
                    return;
                }
                if (SelectSection.SectionEntity.Id == 0)
                {
                    MessageBox.Show("Выберите секцию.");
                    return;
                }
                if (SelectTrainer.TrainerEntity.Id == 0)
                {
                    MessageBox.Show("Выберите тренера.");
                    return;
                }
                Mapper.CreateMap<GroupViewModel, Group>();
                Group group = Mapper.Map<GroupViewModel, Group>(NewGroup);
                group.TrainerId = SelectTrainer.TrainerEntity.Id;
                group.SectionId = SelectSection.SectionEntity.Id;
                _groupCrudService.Create(group);
                RefreshList();
                NewGroup = new GroupViewModel();
                NotifyOfPropertyChange(() => NewGroup);
                NotifyOfPropertyChange(() => GroupList);
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
            if (SelectGroup == null || SelectGroup.GroupEntity.Id == 0)
            {
                MessageBox.Show("Нужно заполнить поле.");
                return;
            }
            try
            {
                if (SelectGroup.Name == null || SelectGroup.Name.Length > 30 || SelectGroup.Name.Length < 1)
                {
                    MessageBox.Show("Имя не может быть больше 30 букв и пустым.");
                    return;
                }
                if (SelectSection.SectionEntity.Id == 0)
                {
                    MessageBox.Show("Выберите секцию.");
                    return;
                }
                if (SelectTrainer.TrainerEntity.Id == 0)
                {
                    MessageBox.Show("Выберите тренера.");
                    return;
                }
                SelectGroup.GroupEntity.TrainerId = SelectTrainer.TrainerEntity.Id;
                SelectGroup.GroupEntity.SectionId = SelectSection.SectionEntity.Id;
                _groupCrudService.Update(SelectGroup.GroupEntity);
                SelectGroup = new GroupViewModel();
                RefreshList();
                NotifyOfPropertyChange(() => SelectGroup);
                NotifyOfPropertyChange(() => GroupList);
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
            GroupList.Clear();

            List<Group> list = new List<Group>(_groupCrudService.GetAll());

            foreach (var data in list)
            {
                GroupViewModel cvm = new GroupViewModel(data);
                GroupList.Add(cvm);
            }
            NotifyOfPropertyChange(() => GroupList);
        }

        public void Delete()
        {
            if (SelectGroup == null || SelectGroup.GroupEntity.Id == 0)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                if(SelectGroup.GroupEntity.Tourist.Count != 0)
                {
                    MessageBox.Show("В группе остались туристы.");
                    return;
                }
                _groupCrudService.Delete(SelectGroup.GroupEntity);
                GroupList.Remove(SelectGroup);
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
                SelectGroup = new GroupViewModel();
                NotifyOfPropertyChange(() => SelectGroup);
                NotifyOfPropertyChange(() => GroupList);
            }
        }
    }
}
