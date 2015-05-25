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
    class TouristGrudViewModel : PropertyChangedBase
    {
        private readonly ITouristCrudService _touristCrudService;
        private readonly IGroupCrudService _groupCrudService;
        private readonly IPersonalDataCrudService _personalDataCrudService;

        public TouristGrudViewModel(ITouristCrudService touristCrudServicehea, IPersonalDataCrudService personalDataCrudService, IGroupCrudService groupCrudService)
        {
            _groupCrudService = groupCrudService;
            _touristCrudService = touristCrudServicehea;
            _personalDataCrudService = personalDataCrudService;
            SelectTourist = new TouristViewModel();
            NewTourist = new TouristViewModel();
            SelectGroup = new GroupViewModel();
            SelectPetsonalData = new PersonalDataViewModel();
            GroupList = new BindableCollection<GroupViewModel>();
            TouristList = new BindableCollection<TouristViewModel>();
            PersonalDataList = new BindableCollection<PersonalDataViewModel>();
            RefreshList();
            RefreshGroupList();
            RefreshPersonalDataList();
        }
        
        public IObservableCollection<GroupViewModel> GroupList { get; set; }
        public IObservableCollection<TouristViewModel> TouristList { get; set; }
        public IObservableCollection<PersonalDataViewModel> PersonalDataList { get; set; }
        
        private GroupViewModel _selectGroup;

        private TouristViewModel _selectTourist;

        private TouristViewModel _newTourist;

        private PersonalDataViewModel _selectPetsonalData;

        public TouristViewModel NewTourist
        {
            get { return _newTourist; }
            set
            {
                if (_newTourist != value)
                    {
                        _newTourist = value;
                        NotifyOfPropertyChange(() => NewTourist);
                    }
            }
        }

        public TouristViewModel SelectTourist
       {
           get { return _selectTourist; }
           set
           {
               if (_selectTourist != value)
               {
                   _selectTourist = value;
                   NotifyOfPropertyChange(() => SelectTourist);
               }
           }
       }
        
        public PersonalDataViewModel SelectPetsonalData
            {
                get { return _selectPetsonalData; }
                set
                {
                    if (_selectPetsonalData != value)
                    {
                        _selectPetsonalData = value;
                        NotifyOfPropertyChange(() => SelectPetsonalData);
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

        public void RefreshGroupList()
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

        public void RefreshPersonalDataList()
            {
                PersonalDataList.Clear();

                List<PersonalData> list = new List<PersonalData>(_personalDataCrudService.GetAll());

                foreach (var data in list)
                {
                    PersonalDataViewModel cvm = new PersonalDataViewModel(data);
                    PersonalDataList.Add(cvm);
                }
                NotifyOfPropertyChange(() => PersonalDataList);
            }

            public void Add()
            {
                if (NewTourist == null)
                {
                    MessageBox.Show("Нужно запольнить поле.");
                    return;
                }
                try
                {
                    if (SelectGroup == null || SelectGroup.GroupEntity.Id == 0)
                    {
                        MessageBox.Show("Выберите группу");
                        return;
                    }
                    if (SelectPetsonalData == null || SelectPetsonalData.PersonalDataEntity.Id == 0 || SelectPetsonalData.PersonalDataEntity.Head.Count > 0 || SelectPetsonalData.PersonalDataEntity.Sportsman.Count > 0 || SelectPetsonalData.PersonalDataEntity.Tourist.Count > 0)
                    {
                        MessageBox.Show("Выберите Данные");
                        return;
                    }
                    Mapper.CreateMap<TouristViewModel, Tourist>();
                    Tourist tourist = Mapper.Map<TouristViewModel, Tourist>(NewTourist);
                    tourist.PersonalDataId = SelectPetsonalData.PersonalDataEntity.Id;
                    tourist.GroupId = SelectGroup.GroupEntity.Id;
                    _touristCrudService.Create(tourist);
                    RefreshList();
                    NewTourist = new TouristViewModel();
                    NotifyOfPropertyChange(() => NewTourist);
                    NotifyOfPropertyChange(() => TouristList);
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
                if (SelectTourist.TouristEntity == null || SelectTourist.TouristEntity.Id == 0)
                {
                    MessageBox.Show("Нужно заполнить поле.");
                    return;
                }
                try
                {
                    if (SelectPetsonalData.PersonalDataEntity == null || SelectPetsonalData.PersonalDataEntity.Id == 0 || SelectGroup.GroupEntity == null || SelectGroup.GroupEntity.Id == 0)
                    {
                        MessageBox.Show("Выберите поля данных и группы.");
                        return;
                    }
                    if ((SelectPetsonalData.PersonalDataEntity.Head.Count > 0 || SelectPetsonalData.PersonalDataEntity.Sportsman.Count > 0 || SelectPetsonalData.PersonalDataEntity.Tourist.Count > 0)  && SelectPetsonalData.PersonalDataEntity.Id != SelectTourist.TouristEntity.PersonalDataId)
                    {
                        MessageBox.Show("Такой человек уже работает вовсю.");
                        return;
                    }
                    SelectTourist.TouristEntity.PersonalDataId = SelectPetsonalData.PersonalDataEntity.Id;
                    SelectTourist.TouristEntity.GroupId = SelectGroup.GroupEntity.Id;
                    _touristCrudService.Update(SelectTourist.TouristEntity);
                    SelectTourist = new TouristViewModel();
                    RefreshList();
                    NotifyOfPropertyChange(() => SelectTourist);
                    NotifyOfPropertyChange(() => TouristList);
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
                TouristList.Clear();

                List<Tourist> list = new List<Tourist>(_touristCrudService.GetAll());

                foreach (var data in list)
                {
                    TouristViewModel cvm = new TouristViewModel(data);
                    TouristList.Add(cvm);
                }
                NotifyOfPropertyChange(() => TouristList);
            }

            public void Delete()
            {
                if (SelectTourist == null || SelectTourist.TouristEntity.Id == 0)
                {
                    MessageBox.Show("Выберите запись");
                    return;
                }
                try
                {
                    _touristCrudService.Delete(SelectTourist.TouristEntity);
                    TouristList.Remove(SelectTourist);
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
                    SelectTourist = new TouristViewModel();
                    NotifyOfPropertyChange(() => SelectTourist);
                    NotifyOfPropertyChange(() => TouristList);
                }
            }
    }
}
