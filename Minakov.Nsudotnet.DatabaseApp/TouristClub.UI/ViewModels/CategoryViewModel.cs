using Caliburn.Micro;
using TouristClub.Data.Entity;

namespace TouristClub.UI.ViewModels
{
    class CategoryViewModel : PropertyChangedBase
    {
        public CategoryViewModel()
        {
            CategoryEntity = new Category();
        }

        public CategoryViewModel(Category categoryEntity)
        {
            CategoryEntity = categoryEntity;
        }

        public Category CategoryEntity { get; set; }

        public int CategoryLevel 
        {
            get { return CategoryEntity.CategoryLevel; } 
            set
            {
                if(value == CategoryEntity.CategoryLevel)
                    return;
                CategoryEntity.CategoryLevel = value;
                NotifyOfPropertyChange(() => CategoryLevel);
            } 
        }

    }
}