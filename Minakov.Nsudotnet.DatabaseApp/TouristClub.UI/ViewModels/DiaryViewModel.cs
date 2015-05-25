using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TouristClub.Data.Entity;

namespace TouristClub.UI.ViewModels
{
    class DiaryViewModel : PropertyChangedBase
    {
        private Diary _dairy;

        public DiaryViewModel()
        {
            DiaryEntity = new Diary();
        }

        public DiaryViewModel(Diary diaryEntity)
        {
            _dairy = diaryEntity;
            _dairy = new Diary(){ Id = diaryEntity.Id, Campaign = diaryEntity.Campaign, Stop = diaryEntity.Stop, Name = diaryEntity.Name};
        }

        public Diary DiaryEntity { get { return _dairy; }
            set
            {
                _dairy = new Diary(){ Id = value.Id, Campaign = value.Campaign, Stop = value.Stop, Name = value.Name};
            } }

        public string Name 
        {
            get { return DiaryEntity.Name; } 
            set
            {
                if (value == DiaryEntity.Name)
                    return;
                DiaryEntity.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
    }
}