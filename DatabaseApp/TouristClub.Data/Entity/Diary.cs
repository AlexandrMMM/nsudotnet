using System.Collections.Generic;

namespace TouristClub.Data.Entity
{
    public partial class Diary : global::TouristClub.Data.Entity.Entity
    {
        public Diary()
        {
            Stop = new HashSet<Stop>();
            Campaign = new HashSet<Campaign>();
        }

        public string Name { get; set; }

        public virtual ICollection<Stop> Stop { get; set; }
        public virtual ICollection<Campaign> Campaign { get; set; }
    }
}