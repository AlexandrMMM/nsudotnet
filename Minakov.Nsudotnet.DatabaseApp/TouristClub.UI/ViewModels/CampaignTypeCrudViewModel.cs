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
    class CampaignTypeCrudViewModel : PropertyChangedBase
    {
        private readonly ICampaignTypeCrudService _campaignTypeCrudService;
        private readonly ISectionCrudService _sectionCrudService;

        public CampaignTypeCrudViewModel(ICampaignTypeCrudService campaignTypeCrudService, ISectionCrudService sectionCrudService)
        {
            _campaignTypeCrudService = campaignTypeCrudService;
            _sectionCrudService = sectionCrudService;
            SelectCampaignType = new CampaignTypeViewModel();
            NewCampaignType = new CampaignTypeViewModel();
            CampaignTypeList = new BindableCollection<CampaignTypeViewModel>();
            SectionList = new BindableCollection<SectionViewModel>();
            RefreshSectionList();
            RefreshList();
        }

        public IObservableCollection<CampaignTypeViewModel> CampaignTypeList { get; set; }

        public IObservableCollection<SectionViewModel> SectionList { get; set; }

        private CampaignTypeViewModel _selectCampaignType;

        private SectionViewModel _selectSection;

        private CampaignTypeViewModel _newCampaignType;

        public CampaignTypeViewModel NewCampaignType
        {
            get { return _newCampaignType; }
            set
            {
                if (_newCampaignType != value)
                {
                    _newCampaignType = value;
                    NotifyOfPropertyChange(() => NewCampaignType);
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

        public void Add()
        {
            if (NewCampaignType == null)
            {
                MessageBox.Show("Нужно запольнить поле.");
                return;
            }
            try
            {
                if (NewCampaignType.Name == null || (NewCampaignType.Name.Length > 30 || NewCampaignType.Name.Length < 1))
                {
                    MessageBox.Show("Длина имени не может быть больше 30 символов и меньше 1.");
                    return;
                }
                Mapper.CreateMap<CampaignTypeViewModel, CampaignType>();
                _campaignTypeCrudService.Create(Mapper.Map<CampaignTypeViewModel, CampaignType>(NewCampaignType));
                RefreshList();
                NewCampaignType = new CampaignTypeViewModel();
                NotifyOfPropertyChange(() => NewCampaignType);
                NotifyOfPropertyChange(() => CampaignTypeList);
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
            if (SelectCampaignType == null || SelectCampaignType.CampaignTypeEntity.Id == 0)
            {
                MessageBox.Show("Нужно заполнить поле.");
                return;
            }
            try
            {
                if (SelectCampaignType.Name == null || (SelectCampaignType.Name.Length > 30 || SelectCampaignType.Name.Length < 1))
                {
                    MessageBox.Show("Длина имени не может быть больше 30 символов и меньше 1.");
                    return;
                }
                _campaignTypeCrudService.Update(SelectCampaignType.CampaignTypeEntity);
                SelectCampaignType = new CampaignTypeViewModel();
                RefreshList();
                NotifyOfPropertyChange(() => SelectCampaignType);
                NotifyOfPropertyChange(() => CampaignTypeList);
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

        public void SectionFromSelectCampaignType()
        {
            SectionList.Clear();

            foreach (var section in SelectCampaignType.Section)
            {
                SectionList.Add(section);
            }
            NotifyOfPropertyChange(() => SectionList);
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
            SelectCampaignType.CampaignTypeEntity.Section.Remove(SelectSection.SectionEntity);
            Update();
            RefreshSectionList();
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
            SelectCampaignType.CampaignTypeEntity.Section.Add(SelectSection.SectionEntity);
            Update();
            RefreshSectionList();
        }

        public void RefreshList()
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

        public void Delete()
        {
            if (SelectCampaignType == null || SelectCampaignType.CampaignTypeEntity.Id == 0)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                if (SelectCampaignType.CampaignTypeEntity.Campaign.Count == 0 && SelectCampaignType.CampaignTypeEntity.Section.Count == 0)
                {
                    _campaignTypeCrudService.Delete(SelectCampaignType.CampaignTypeEntity);
                    CampaignTypeList.Remove(SelectCampaignType);
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
                SelectCampaignType = new CampaignTypeViewModel();
                NotifyOfPropertyChange(() => SelectCampaignType);
                NotifyOfPropertyChange(() => CampaignTypeList);
            }
        }
    }
}