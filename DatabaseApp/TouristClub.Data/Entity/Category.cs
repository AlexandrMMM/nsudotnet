using System.Collections.Generic;

namespace TouristClub.Data.Entity
{
    public partial class Category : global::TouristClub.Data.Entity.Entity
    {
        public Category()
        {
            Sportsman = new HashSet<Sportsman>();
            Campaign = new HashSet<Campaign>();
        }

        public int CategoryLevel { get; set; }
    
        public virtual ICollection<Sportsman> Sportsman { get; set; }
        public virtual ICollection<Campaign> Campaign { get; set; }
    }
}