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
    class HeadCrudViewModel : PropertyChangedBase
    {
        private readonly IHeadCrudService _headCrudService;
        private readonly ISectionCrudService _sectionCrudService;
        private readonly IPersonalDataCrudService _personalDataCrudService;

        public HeadCrudViewModel(IHeadCrudService headCrudServicehea, IPersonalDataCrudService personalDataCrudService, ISectionCrudService sectionCrudService)
        {
            _sectionCrudService = sectionCrudService;
            _headCrudService = headCrudServicehea;
            _personalDataCrudService = personalDataCrudService;
            SelectHead = new HeadViewModel();
            NewHead = new HeadViewModel();
            SelectSection = new SectionViewModel();
            SelectPetsonalData = new PersonalDataViewModel();
            SectionList = new BindableCollection<SectionViewModel>();
            HeadList = new BindableCollection<HeadViewModel>();
            PersonalDataList = new BindableCollection<PersonalDataViewModel>();
            RefreshList();
            RefreshSectionList();
            RefreshPersonalDataList();
        }
        
        public IObservableCollection<SectionViewModel> SectionList { get; set; }
        public IObservableCollection<HeadViewModel> HeadList { get; set; }
        public IObservableCollection<PersonalDataViewModel> PersonalDataList { get; set; }
        
        private SectionViewModel _selectSection;

        private HeadViewModel _selectHead;

        private HeadViewModel _newHead;

        private PersonalDataViewModel _selectPetsonalData;

        public HeadViewModel NewHead
        {
            get { return _newHead; }
            set
            {
                if (_newHead != value)
                    {
                        _newHead = value;
                        NotifyOfPropertyChange(() => NewHead);
                    }
            }
        }

        public HeadViewModel SelectHead
       {
           get { return _selectHead; }
           set
           {
               if (_selectHead != value)
               {
                   _selectHead = value;
                   NotifyOfPropertyChange(() => SelectHead);
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
                if (NewHead == null)
                {
                    MessageBox.Show("Нужно запольнить поле.");
                    return;
                }
                try
                {
                    if (NewHead.EmployDate == DateTime.MinValue || SelectSection.SectionEntity.Id == 0 || SelectPetsonalData.PersonalDataEntity.Id == 0)
                    {
                        MessageBox.Show("Выберите время, секцию и персональные данные.");
                        return;
                    }
                    if (SelectSection.SectionEntity.Head.Count > 0)
                    {
                        MessageBox.Show("У этой секции уже есть глава.");
                        return;
                    }
                    if (SelectPetsonalData.PersonalDataEntity.Head.Count > 0 || SelectPetsonalData.PersonalDataEntity.Sportsman.Count > 0 || SelectPetsonalData.PersonalDataEntity.Tourist.Count > 0)
                    {
                        MessageBox.Show("Такой человек уже работает вовсю.");
                        return;
                    }
                    Mapper.CreateMap<HeadViewModel, Head>();
                    Head head = Mapper.Map<HeadViewModel, Head>(NewHead);
                    head.PersonalDataId = SelectPetsonalData.PersonalDataEntity.Id;
                    head.SectionId = SelectSection.SectionEntity.Id;
                    _headCrudService.Create(head);
                    RefreshList();
                    NewHead = new HeadViewModel();
                    NotifyOfPropertyChange(() => NewHead);
                    NotifyOfPropertyChange(() => HeadList);
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
                if (SelectHead == null || SelectHead.HeadEntity.Id == 0)
                {
                    MessageBox.Show("Нужно заполнить поле.");
                    return;
                }
                try
                {
                    if (SelectHead.EmployDate == DateTime.MinValue || SelectSection.SectionEntity.Id == 0 || SelectPetsonalData.PersonalDataEntity.Id == 0)
                    {
                        MessageBox.Show("Выберите время, секцию и персональные данные.");
                        return;
                    }
                    if (SelectSection.SectionEntity.Head.Count > 0 && SelectSection.SectionEntity.Id != SelectHead.HeadEntity.SectionId)
                    {
                        MessageBox.Show("У этой секции уже есть глава.");
                        return;
                    }
                    if ((SelectPetsonalData.PersonalDataEntity.Head.Count > 0 || SelectPetsonalData.PersonalDataEntity.Sportsman.Count > 0 || SelectPetsonalData.PersonalDataEntity.Tourist.Count > 0)  && SelectPetsonalData.PersonalDataEntity.Id != SelectHead.HeadEntity.PersonalDataId)
                    {
                        MessageBox.Show("Такой человек уже работает вовсю.");
                        return;
                    }
                    SelectHead.HeadEntity.PersonalDataId = SelectPetsonalData.PersonalDataEntity.Id;
                    SelectHead.HeadEntity.SectionId = SelectSection.SectionEntity.Id;
                    _headCrudService.Update(SelectHead.HeadEntity);
                    SelectHead = new HeadViewModel();
                    RefreshList();
                    NotifyOfPropertyChange(() => SelectHead);
                    NotifyOfPropertyChange(() => HeadList);
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
                HeadList.Clear();

                List<Head> list = new List<Head>(_headCrudService.GetAll());

                foreach (var data in list)
                {
                    HeadViewModel cvm = new HeadViewModel(data);
                    HeadList.Add(cvm);
                }
                NotifyOfPropertyChange(() => HeadList);
            }

            public void Delete()
            {
                if (SelectHead == null || SelectHead.HeadEntity.Id == 0)
                {
                    MessageBox.Show("Выберите запись");
                    return;
                }
                try
                {
                    _headCrudService.Delete(SelectHead.HeadEntity);
                    HeadList.Remove(SelectHead);
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
                    SelectHead = new HeadViewModel();
                    NotifyOfPropertyChange(() => SelectHead);
                    NotifyOfPropertyChange(() => HeadList);
                }
            }
    }
}
