using System.Collections.Generic;

namespace TouristClub.Data.Entity
{
    public partial class Group : global::TouristClub.Data.Entity.Entity
    {
        public Group()
        {
            Tourist = new HashSet<Tourist>();
        }

        public string Name { get; set; }
        public int TrainerId { get; set; }
        public int SectionId { get; set; }
    
        public virtual Trainer Trainer { get; set; }
        public virtual Section Section { get; set; }
        public virtual ICollection<Tourist> Tourist { get; set; }
    }
}