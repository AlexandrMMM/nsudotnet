using System.Collections.Generic;

namespace TouristClub.Data.Entity
{
    public partial class Competition : global::TouristClub.Data.Entity.Entity
    {
        public Competition()
        {
            Sportsman = new HashSet<Sportsman>();
        }

        public string Name { get; set; }

        public virtual ICollection<Sportsman> Sportsman { get; set; }
    }
}