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
    class PersonalDataCrudViewModel : PropertyChangedBase
    {
        private readonly IPersonalDataCrudService _personalDataCrudService;

        public PersonalDataCrudViewModel(IPersonalDataCrudService personalDataCrudService)
        {
            _personalDataCrudService = personalDataCrudService;
            SelectPersonalData = new PersonalDataViewModel();
            NewPersonalData = new PersonalDataViewModel();
            PersonalDataList = new BindableCollection<PersonalDataViewModel>();
            RefreshList();
        }

        public IObservableCollection<PersonalDataViewModel> PersonalDataList { get; set; }

        private PersonalDataViewModel _selectPersonalData;

        private PersonalDataViewModel _newPersonalData;

        public PersonalDataViewModel NewPersonalData
        {
            get { return _newPersonalData; }
            set
            {
                if (_newPersonalData != value)
                {
                    _newPersonalData = value;
                    NotifyOfPropertyChange(() => NewPersonalData);
                }
            }
        }

        public PersonalDataViewModel SelectPersonalData
        {
            get { return _selectPersonalData; }
            set
            {
                if (_selectPersonalData != value)
                {
                    _selectPersonalData = value;
                    NotifyOfPropertyChange(() => SelectPersonalData);
                }
            }
        }

        public void Add()
        {
            if (NewPersonalData == null)
            {
                MessageBox.Show("����� ���������� ��� ����.");
                return;
            }
            try
            {
                if (NewPersonalData.Name == null || (NewPersonalData.Name.Length > 30 || NewPersonalData.Name.Length < 1))
                {
                    MessageBox.Show("����� ����� �� ����� ���� ������ 30 �������� � ������ 1.");
                    return;
                }
                if (NewPersonalData.Gender == null || !(NewPersonalData.Gender.Equals("�") || NewPersonalData.Gender.Equals("�")))
                {
                    MessageBox.Show("��� ����� ���� ���� � ���� �");
                    return;
                }
                if (NewPersonalData.Patronymic == null || (NewPersonalData.Patronymic.Length > 30 || NewPersonalData.Patronymic.Length < 1))
                {
                    MessageBox.Show("����� �������� �� ����� ���� ������ 30 �������� � ������ 1.");
                    return;
                }
                if (NewPersonalData.Surname == null || (NewPersonalData.Surname.Length > 30 || NewPersonalData.Surname.Length < 1))
                {
                    MessageBox.Show("����� ������� �� ����� ���� ������ 30 �������� � ������ 1.");
                    return;
                }
                if (NewPersonalData.BirthDate > DateTime.Now || NewPersonalData.BirthDate == DateTime.MinValue)
                {
                    MessageBox.Show("����� ������� �� ����� ���� ������ 30 �������� � ������ 1.");
                    return;
                }
                Mapper.CreateMap<PersonalDataViewModel, PersonalData>();
                _personalDataCrudService.Create(Mapper.Map<PersonalDataViewModel, PersonalData>(NewPersonalData));
                RefreshList();
                NewPersonalData = new PersonalDataViewModel();
                NotifyOfPropertyChange(() => NewPersonalData);
                NotifyOfPropertyChange(() => PersonalDataList);
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.GetBaseException() as SqlException;

                if (sqlException != null)
                {
                    MessageBox.Show("���������� ������� ������");
                }
            } 
        }

        public void Update()
        {
            if (SelectPersonalData == null || SelectPersonalData.PersonalDataEntity.Id == 0)
            {
                MessageBox.Show("����� ���������� ��� ����.");
                return;
            }
            try
            {
                if (SelectPersonalData.Name == null || (SelectPersonalData.Name.Length > 30 || SelectPersonalData.Name.Length < 1))
                {
                    MessageBox.Show("����� ����� �� ����� ���� ������ 30 �������� � ������ 1.");
                    return;
                }
                if (SelectPersonalData.Gender == null || !(SelectPersonalData.Gender.Equals("�") || SelectPersonalData.Gender.Equals("�")))
                {
                    MessageBox.Show("��� ����� ���� ���� � ���� �");
                    return;
                }
                if (SelectPersonalData.Patronymic == null || (SelectPersonalData.Patronymic.Length > 30 || SelectPersonalData.Patronymic.Length < 1))
                {
                    MessageBox.Show("����� �������� �� ����� ���� ������ 30 �������� � ������ 1.");
                    return;
                }
                if ((SelectPersonalData.Surname == null) || ((SelectPersonalData.Surname.Length > 30) || (SelectPersonalData.Surname.Length < 1)))
                {
                    MessageBox.Show("����� ������� �� ����� ���� ������ 30 �������� � ������ 1.");
                    return;
                }
                if (NewPersonalData.BirthDate > DateTime.Now)
                {
                    MessageBox.Show("����� ������� �� ����� ���� ������ 30 �������� � ������ 1.");
                    return;
                }
                _personalDataCrudService.Update(SelectPersonalData.PersonalDataEntity);
                SelectPersonalData = new PersonalDataViewModel();
                RefreshList();
                NotifyOfPropertyChange(() => SelectPersonalData);
                NotifyOfPropertyChange(() => PersonalDataList);
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.GetBaseException() as SqlException;

                if (sqlException != null)
                {
                    MessageBox.Show("���������� �������� ������");
                }
            }
        }

        public void RefreshList()
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

        public void Delete()
        {
            if (SelectPersonalData == null || SelectPersonalData.PersonalDataEntity.Id == 0)
            {
                MessageBox.Show("�������� ������");
                return;
            }
            try
            {
                _personalDataCrudService.Delete(SelectPersonalData.PersonalDataEntity);
                PersonalDataList.Remove(SelectPersonalData);
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.GetBaseException() as SqlException;

                if (sqlException != null)
                {
                    MessageBox.Show(
                        "���������");
                }
            }
            finally
            {
                SelectPersonalData = new PersonalDataViewModel();
                NotifyOfPropertyChange(() => SelectPersonalData);
                NotifyOfPropertyChange(() => PersonalDataList);
            }
        }
    }
}