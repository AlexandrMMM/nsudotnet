using System.Collections.Generic;

namespace TouristClub.Data.Entity
{
    public partial class CampaignType : global::TouristClub.Data.Entity.Entity
    {
        public CampaignType()
        {
            Section = new HashSet<Section>();
            Campaign = new HashSet<Campaign>();
        }

        public string Name { get; set; }
    
        public virtual ICollection<Section> Section { get; set; }
        public virtual ICollection<Campaign> Campaign { get; set; }
    }
}
