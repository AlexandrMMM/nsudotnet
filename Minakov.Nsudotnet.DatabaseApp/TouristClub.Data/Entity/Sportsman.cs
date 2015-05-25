using System.Collections.Generic;

namespace TouristClub.Data.Entity
{

    public partial class Sportsman : global::TouristClub.Data.Entity.Entity
    {
        public Sportsman()
        {
            Competition = new HashSet<Competition>();
            Campaign = new HashSet<Campaign>();
            Trainer = new HashSet<Trainer>();
        }

        public int PersonalDataId { get; set; }
        public int SectionId { get; set; }
        public int CategoryId { get; set; }
    
        public virtual Section Section { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Competition> Competition { get; set; }
        public virtual PersonalData PersonalData { get; set; }
        public virtual ICollection<Trainer> Trainer { get; set; }
        public virtual ICollection<Campaign> Campaign { get; set; }
    }
}