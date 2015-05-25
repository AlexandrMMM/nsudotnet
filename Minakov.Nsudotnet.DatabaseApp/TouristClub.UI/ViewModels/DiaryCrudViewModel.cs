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
    class DiaryCrudViewModel : PropertyChangedBase
    {
        private readonly IDiaryCrudService _diaryCrudService;

        public DiaryCrudViewModel(IDiaryCrudService diaryCrudService)
        {
            _diaryCrudService = diaryCrudService;
            SelectDiary = new DiaryViewModel();
            NewDiary = new DiaryViewModel();
            DiaryList = new BindableCollection<DiaryViewModel>();
            RefreshList();
        }

        public IObservableCollection<DiaryViewModel> DiaryList { get; set; }

        private DiaryViewModel _selectDiary;

        private DiaryViewModel _newDiary;

        public DiaryViewModel NewDiary
        {
            get { return _newDiary; }
            set
            {
                if (_newDiary != value)
                {
                    _newDiary = value;
                    NotifyOfPropertyChange(() => NewDiary);
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

        public void Add()
        {
            if (NewDiary == null)
            {
                MessageBox.Show("Нужно запольнить все поля.");
                return;
            }
            try
            {
                if (NewDiary.Name == null || (NewDiary.Name.Length > 30 || NewDiary.Name.Length < 1))
                {
                    MessageBox.Show("Длина имени не может быть больше 30 символов и меньше 1.");
                    return;
                }
                Mapper.CreateMap<DiaryViewModel, Diary>();
                _diaryCrudService.Create(Mapper.Map<DiaryViewModel, Diary>(NewDiary));
                RefreshList();
                NewDiary = new DiaryViewModel();
                NotifyOfPropertyChange(() => NewDiary);
                NotifyOfPropertyChange(() => DiaryList);
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
            if (SelectDiary == null || SelectDiary.DiaryEntity.Id == 0)
            {
                MessageBox.Show("Нужно запольнить все поля.");
                return;
            }
            try
            {
                if (SelectDiary.Name == null || (SelectDiary.Name.Length > 30 || SelectDiary.Name.Length < 1))
                {
                    MessageBox.Show("Длина имени не может быть больше 30 символов и меньше 1.");
                    return;
                }
                _diaryCrudService.Update(SelectDiary.DiaryEntity);
                SelectDiary = new DiaryViewModel();
                RefreshList();
                NotifyOfPropertyChange(() => SelectDiary);
                NotifyOfPropertyChange(() => DiaryList);
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
            DiaryList.Clear();

            List<Diary> list = new List<Diary>(_diaryCrudService.GetAll());

            foreach (var data in list)
            {
                DiaryViewModel cvm = new DiaryViewModel(data);
                DiaryList.Add(cvm);
            }
            NotifyOfPropertyChange(() => DiaryList);
        }

        public void Delete()
        {
            if (SelectDiary == null || SelectDiary.DiaryEntity.Id == 0)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                if (SelectDiary.DiaryEntity.Campaign.Count == 0 && SelectDiary.DiaryEntity.Stop.Count == 0)
                {
                    _diaryCrudService.Delete(SelectDiary.DiaryEntity);
                    DiaryList.Remove(SelectDiary);
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
                SelectDiary = new DiaryViewModel();
                NotifyOfPropertyChange(() => SelectDiary);
                NotifyOfPropertyChange(() => DiaryList);
            }
        }
    }
}