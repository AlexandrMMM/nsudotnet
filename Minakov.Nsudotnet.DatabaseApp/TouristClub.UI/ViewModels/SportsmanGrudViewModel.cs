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
    class SportsmanGrudViewModel : PropertyChangedBase
    {
        private readonly ICategoryCrudService _categoryCrudService;
        private readonly ISectionCrudService _sectionCrudService;
        private readonly IPersonalDataCrudService _personalDataCrudService;
        private readonly ISportsmanCrudService _sportsmanCrudService;

        public SportsmanGrudViewModel(ICategoryCrudService categoryCrudService, IPersonalDataCrudService personalDataCrudService, ISportsmanCrudService sportsmanCrudService, ISectionCrudService sectionCrudService)
        {
            _categoryCrudService = categoryCrudService;
            _sectionCrudService = sectionCrudService;
            _personalDataCrudService = personalDataCrudService;
            _sportsmanCrudService = sportsmanCrudService;
            SelectSportsman = new SportsmanViewModel();
            NewSportsman = new SportsmanViewModel();
            SelectSection = new SectionViewModel();
            SelectCategory = new CategoryViewModel();
            SelectPetsonalData = new PersonalDataViewModel();
            SportsmanList = new BindableCollection<SportsmanViewModel>();
            CategoryList = new BindableCollection<CategoryViewModel>();
            PersonalDataList = new BindableCollection<PersonalDataViewModel>();
            SectionList = new BindableCollection<SectionViewModel>();
            RefreshList();
            RefreshSectionList();
            RefreshPersonalDataList();
            RefreshCategoryList();
        }
        
        public IObservableCollection<SectionViewModel> SectionList { get; set; }
        public IObservableCollection<CategoryViewModel> CategoryList { get; set; }
        public IObservableCollection<PersonalDataViewModel> PersonalDataList { get; set; }
        public IObservableCollection<SportsmanViewModel> SportsmanList { get; set; }


        private SectionViewModel _selectSection;

        private SportsmanViewModel _selectSportsman;

        private SportsmanViewModel _newSportsman;

        private PersonalDataViewModel _selectPetsonalData;

        private CategoryViewModel _selectCategory;



        public SportsmanViewModel NewSportsman
        {
            get { return _newSportsman; }
            set
            {
                if (_newSportsman != value)
                    {
                        _newSportsman = value;
                        NotifyOfPropertyChange(() => NewSportsman);
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

        public CategoryViewModel SelectCategory
        {
            get { return _selectCategory; }
            set
            {
                if (_selectCategory != value)
                {
                    _selectCategory = value;
                    NotifyOfPropertyChange(() => SelectCategory);
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

        public void RefreshCategoryList()
            {
                CategoryList.Clear();

                List<Category> list = new List<Category>(_categoryCrudService.GetAll());

                foreach (var data in list)
                {
                    CategoryViewModel cvm = new CategoryViewModel(data);
                    CategoryList.Add(cvm);
                }
                NotifyOfPropertyChange(() => CategoryList);
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

        public void RefreshList()
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
                if (NewSportsman == null)
                {
                    MessageBox.Show("Нужно запольнить поле.");
                    return;
                }
                try
                {
                    if (SelectSection == null || SelectSection.SectionEntity == null || SelectSection.SectionEntity.Id == 0)
                    {
                        MessageBox.Show("Выберите секцию");
                        return;
                    }
                    if (SelectPetsonalData == null || SelectPetsonalData.PersonalDataEntity.Id == 0 || SelectPetsonalData.PersonalDataEntity.Head.Count > 0 || SelectPetsonalData.PersonalDataEntity.Sportsman.Count > 0 || SelectPetsonalData.PersonalDataEntity.Tourist.Count > 0)
                    {
                        MessageBox.Show("Выберите Данные");
                        return;
                    }
                    if (SelectCategory == null || SelectCategory.CategoryEntity == null || SelectCategory.CategoryEntity.Id == 0)
                    {
                        MessageBox.Show("Выберите категорию");
                        return;
                    }
                    Mapper.CreateMap<SportsmanViewModel, Sportsman>();
                    Sportsman sportsman = Mapper.Map<SportsmanViewModel, Sportsman>(NewSportsman);
                    sportsman.PersonalDataId = SelectPetsonalData.PersonalDataEntity.Id;
                    sportsman.CategoryId = SelectCategory.CategoryEntity.Id;
                    sportsman.SectionId = SelectSection.SectionEntity.Id;
                    _sportsmanCrudService.Create(sportsman);
                    RefreshList();
                    NewSportsman = new SportsmanViewModel();
                    NotifyOfPropertyChange(() => NewSportsman);
                    NotifyOfPropertyChange(() => SportsmanList);
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
                if (SelectSportsman == null || SelectSportsman.SportsmanEntity == null || SelectSportsman.SportsmanEntity.Id == 0)
                {
                    MessageBox.Show("Нужно заполнить поле.");
                    return;
                }
                try
                {
                    if (SelectPetsonalData.PersonalDataEntity == null || SelectPetsonalData.PersonalDataEntity.Id == 0 || SelectSection.SectionEntity == null || SelectSection.SectionEntity.Id == 0 || SelectCategory.CategoryEntity == null || SelectCategory.CategoryEntity.Id == 0)
                    {
                        MessageBox.Show("Выберите поля данных и секций.");
                        return;
                    }
                    if ((SelectPetsonalData.PersonalDataEntity.Head.Count > 0 || SelectPetsonalData.PersonalDataEntity.Sportsman.Count > 0 || SelectPetsonalData.PersonalDataEntity.Tourist.Count > 0)  && SelectPetsonalData.PersonalDataEntity.Id != SelectSportsman.SportsmanEntity.PersonalDataId)
                    {
                        MessageBox.Show("Такой человек уже работает вовсю.");
                        return;
                    }
                    SelectSportsman.SportsmanEntity.PersonalDataId = SelectPetsonalData.PersonalDataEntity.Id;
                    SelectSportsman.SportsmanEntity.CategoryId = SelectCategory.CategoryEntity.Id;
                    SelectSportsman.SportsmanEntity.SectionId = SelectSection.SectionEntity.Id;
                    _sportsmanCrudService.Update(SelectSportsman.SportsmanEntity);
                    SelectSportsman = new SportsmanViewModel();
                    RefreshList();
                    NotifyOfPropertyChange(() => SelectSportsman);
                    NotifyOfPropertyChange(() => SportsmanList);
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

            public void Delete()
            {
                if (SelectSportsman == null || SelectSportsman.SportsmanEntity == null || SelectSportsman.SportsmanEntity.Id == 0)
                {
                    MessageBox.Show("Выберите запись");
                    return;
                }
                try
                {
                    if (SelectSportsman.SportsmanEntity.Competition.Count > 0 ||
                        SelectSportsman.SportsmanEntity.Trainer.Count > 0 ||
                        SelectSportsman.SportsmanEntity.Campaign.Count > 0)
                    {
                        MessageBox.Show("Связана с записями из таблицы походов или тренеров или созтязаний.");
                        return;
                    }
                    _sportsmanCrudService.Delete(SelectSportsman.SportsmanEntity);
                    SportsmanList.Remove(SelectSportsman);
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
                    SelectSportsman = new SportsmanViewModel();
                    NotifyOfPropertyChange(() => SelectSportsman);
                    NotifyOfPropertyChange(() => SportsmanList);
                }
            }
    }
}
