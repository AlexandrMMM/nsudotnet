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
    class SectionCrudViewModel : PropertyChangedBase
    {
        private readonly ISectionCrudService _sectionCrudService;
        private readonly ICampaignTypeCrudService _campaignTypeCrudService;

        public SectionCrudViewModel(ISectionCrudService sectionCrudService, ICampaignTypeCrudService campaignTypeCrudService)
        {
            _sectionCrudService = sectionCrudService;
            _campaignTypeCrudService = campaignTypeCrudService;
            SelectSection = new SectionViewModel();
            NewSection = new SectionViewModel();
            SectionList = new BindableCollection<SectionViewModel>();
            CampaignTypeList = new BindableCollection<CampaignTypeViewModel>();
            RefreshList();
            RefreshCampaignTypeList();
        }

        public IObservableCollection<SectionViewModel> SectionList { get; set; }

        public IObservableCollection<CampaignTypeViewModel> CampaignTypeList { get; set; }

        private SectionViewModel _selectSection;

        private CampaignTypeViewModel _selectCampaignType;

        private SectionViewModel _newSection;

        public SectionViewModel NewSection
        {
            get { return _newSection; }
            set
            {
                if (_newSection != value)
                {
                    _newSection = value;
                    NotifyOfPropertyChange(() => NewSection);
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

        public CampaignTypeViewModel SelectCampaignType
        {
            get { return _selectCampaignType; }
            set
            {
                if (_selectCampaignType != value)
                {
                    _selectCampaignType = value;
                    NotifyOfPropertyChange(() => SelectCampaignType);
                }
            }
        }

        public void RefreshCampaignTypeList()
        {
            CampaignTypeList.Clear();

            List<CampaignType> list = new List<CampaignType>(_campaignTypeCrudService.GetAll());

            foreach (var data in list)
            {
                CampaignTypeViewModel cvm = new CampaignTypeViewModel(data);
                CampaignTypeList.Add(cvm);
            }
            NotifyOfPropertyChange(() => CampaignTypeList);
        }

        public void Add()
        {
            if (NewSection == null)
            {
                MessageBox.Show("Нужно запольнить поле.");
                return;
            }
            try
            {
                if (NewSection.Name == null || (NewSection.Name.Length > 30 || NewSection.Name.Length < 1))
                {
                    MessageBox.Show("Длина имени не может быть больше 30 символов и меньше 1.");
                    return;
                }
                Mapper.CreateMap<SectionViewModel, Section>();
                _sectionCrudService.Create(Mapper.Map<SectionViewModel, Section>(NewSection));
                RefreshList();
                NewSection = new SectionViewModel();
                NotifyOfPropertyChange(() => NewSection);
                NotifyOfPropertyChange(() => SectionList);
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
            if (SelectSection == null || SelectSection.SectionEntity.Id == 0)
            {
                MessageBox.Show("Нужно заполнить поле.");
                return;
            }
            try
            {
                if (SelectSection.Name == null || (SelectSection.Name.Length > 30 || SelectSection.Name.Length < 1))
                {
                    MessageBox.Show("Длина имени не может быть больше 30 символов и меньше 1.");
                    return;
                }
                _sectionCrudService.Update(SelectSection.SectionEntity);
                SelectSection = new SectionViewModel();
                RefreshList();
                NotifyOfPropertyChange(() => SelectSection);
                NotifyOfPropertyChange(() => SectionList);
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
            SectionList.Clear();

            List<Section> list = new List<Section>(_sectionCrudService.GetAll());

            foreach (var data in list)
            {
                SectionViewModel cvm = new SectionViewModel(data);
                SectionList.Add(cvm);
            }
            NotifyOfPropertyChange(() => SectionList);
        }

        public void Delete()
        {
            if (SelectSection == null || SelectSection.SectionEntity.Id == 0)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                if (SelectSection.SectionEntity.Sportsman.Count == 0 && SelectSection.SectionEntity.CampaignType.Count == 0)
                {
                    _sectionCrudService.Delete(SelectSection.SectionEntity);
                    SectionList.Remove(SelectSection);
                }
                else
                {
                    MessageBox.Show("Тип кампании содержит Походы и Секции");
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
                SelectSection = new SectionViewModel();
                NotifyOfPropertyChange(() => SelectSection);
                NotifyOfPropertyChange(() => SectionList);
            }
        }

        public void CampaignTypeFromSelectCampaignTypeSection()
        {
            if (SelectSection == null || SelectSection.CampaignType == null)
            {
                MessageBox.Show("Нужно заполнить поле.");
                return;
            }

            CampaignTypeList.Clear();

            foreach (var data in SelectSection.CampaignType)
            {
                CampaignTypeList.Add(data);
            }
            NotifyOfPropertyChange(() => CampaignTypeList);
        }

        public void DeleteConnection()
        {
            if (SelectCampaignType == null || SelectCampaignType.CampaignTypeEntity.Id == 0)
            {
                MessageBox.Show("Нужно выбрать тип похода");
                return;
            }
            if (SelectSection == null || SelectSection.SectionEntity.Id == 0)
            {
                MessageBox.Show("Нужно выбрать секцию");
                return;
            }
            SelectSection.SectionEntity.CampaignType.Remove(SelectCampaignType.CampaignTypeEntity);
            Update();
            RefreshCampaignTypeList();
        }

        public void AddConnection()
        {
            if (SelectCampaignType == null || SelectCampaignType.CampaignTypeEntity.Id == 0)
            {
                MessageBox.Show("Нужно выбрать тип похода");
                return;
            }
            if (SelectSection == null || SelectSection.SectionEntity.Id == 0)
            {
                MessageBox.Show("Нужно выбрать секцию");
                return;
            }
            SelectSection.SectionEntity.CampaignType.Add(SelectCampaignType.CampaignTypeEntity);
            Update();
            RefreshCampaignTypeList();
        }
    }
}