using System.Collections.Generic;

namespace TouristClub.Data.Entity
{
    public partial class Section : global::TouristClub.Data.Entity.Entity
    {
        public Section()
        {
            Sportsman = new HashSet<Sportsman>();
            CampaignType = new HashSet<CampaignType>();
            Head = new HashSet<Head>();
            Group = new HashSet<Group>();
        }

        public string Name { get; set; }

        public virtual ICollection<Head> Head { get; set; }
        public virtual ICollection<Sportsman> Sportsman { get; set; }
        public virtual ICollection<CampaignType> CampaignType { get; set; }
        public virtual ICollection<Group> Group { get; set; }
    }
}