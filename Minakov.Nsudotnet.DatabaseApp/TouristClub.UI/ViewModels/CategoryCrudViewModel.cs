using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using Caliburn.Micro;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.UI.ViewModels
{
    class CategoryCrudViewModel : PropertyChangedBase
    {
        private readonly ICategoryCrudService _categoryCrudService;

        public CategoryCrudViewModel(ICategoryCrudService categoryCrudService)
        {
            _categoryCrudService = categoryCrudService;
            SelectCategory = new CategoryViewModel();
            NewCategory = new CategoryViewModel();
            CategoryList = new BindableCollection<CategoryViewModel>();
            RefreshList();
        }

        public IObservableCollection<CategoryViewModel> CategoryList { get; private set; }

        private CategoryViewModel _selectCategory;

        private CategoryViewModel _newCategory;

        public CategoryViewModel NewCategory
        {
            get { return _newCategory; }
            set
            {
                if (_newCategory != value)
                {
                    _newCategory = value;
                    NotifyOfPropertyChange(() => NewCategory);
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

        public void Add()
        {
            if (NewCategory == null)
            {
                MessageBox.Show("Должно быть числом в диапазоне от 1 до 100");
                NewCategory = new CategoryViewModel();
                NotifyOfPropertyChange(() => NewCategory);
                NotifyOfPropertyChange(() => CategoryList);
                return;
            }
            try
            {
                if (NewCategory.CategoryLevel > 100 || NewCategory.CategoryLevel < 1)
                {
                    MessageBox.Show("Должно быть числом в диапазоне от 1 до 100");
                    NewCategory = new CategoryViewModel();
                    NotifyOfPropertyChange(() => NewCategory);
                    NotifyOfPropertyChange(() => CategoryList);
                    return;
                }
                Mapper.CreateMap<CategoryViewModel, Category>();
                _categoryCrudService.Create(Mapper.Map<CategoryViewModel, Category>(NewCategory));
                RefreshList();
                NewCategory = new CategoryViewModel();
                NotifyOfPropertyChange(() => NewCategory);
                NotifyOfPropertyChange(() => CategoryList);
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
            if (SelectCategory == null || SelectCategory.CategoryEntity.Id == 0)
            {
                MessageBox.Show("Выберите запись");
                RefreshList();
                return;
            }
            try
            {
                if (SelectCategory.CategoryLevel > 100 || SelectCategory.CategoryLevel < 1)
                {
                    MessageBox.Show("Должно быть числом в диапазоне от 1 до 100");
                    SelectCategory = new CategoryViewModel();
                    RefreshList();
                    NotifyOfPropertyChange(() => SelectCategory);
                    NotifyOfPropertyChange(() => CategoryList);
                    return;
                }
                _categoryCrudService.Update(SelectCategory.CategoryEntity);
                SelectCategory = new CategoryViewModel();
                RefreshList();
                NotifyOfPropertyChange(() => SelectCategory);
                NotifyOfPropertyChange(() => CategoryList);
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
            CategoryList.Clear();

            List<Category> list = new List<Category>(_categoryCrudService.GetAll());

            foreach (var data in list)
            {
                CategoryViewModel cvm = new CategoryViewModel(data);
                CategoryList.Add(cvm);
            }
            NotifyOfPropertyChange(() => CategoryList);
        }

        public void Delete()
        {
            if (SelectCategory == null || SelectCategory.CategoryEntity.Id == 0)
            {
                MessageBox.Show("Выберите запись");
                SelectCategory = new CategoryViewModel();
                NotifyOfPropertyChange(() => SelectCategory);
                NotifyOfPropertyChange(() => CategoryList);
                return;
            }
            try
            {
                if (SelectCategory.CategoryEntity.Sportsman.Count == 0 &&
                    SelectCategory.CategoryEntity.Campaign.Count == 0)
                {
                    _categoryCrudService.Delete(SelectCategory.CategoryEntity);
                    CategoryList.Remove(SelectCategory);
                }
                else
                {
                    MessageBox.Show(
                        "Невозможно удалить запись, потому что она связана с записями из Походов и из Спортсменов");
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
                SelectCategory = new CategoryViewModel();
                NotifyOfPropertyChange(() => SelectCategory);
                NotifyOfPropertyChange(() => CategoryList);
            }
        }
    }
}