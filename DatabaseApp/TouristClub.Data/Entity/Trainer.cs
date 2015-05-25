using System.Collections.Generic;

namespace TouristClub.Data.Entity
{
    public partial class Trainer : global::TouristClub.Data.Entity.Entity
    {
        public Trainer()
        {
            Group = new HashSet<Group>();
            Traning = new HashSet<Traning>();
        }

        public int Salary { get; set; }
        public int SportsmanId { get; set; }
    
        public virtual ICollection<Group> Group { get; set; }
        public virtual Sportsman Sportsman { get; set; }
        public virtual ICollection<Traning> Traning { get; set; } 
    }
}