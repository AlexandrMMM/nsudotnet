using System.Collections.Generic;

namespace TouristClub.Data.Entity
{
    public partial class Campaign : Entity
    {
        public Campaign()
        {
            RoutePoint = new HashSet<RoutePoint>();
            PersonalData = new HashSet<PersonalData>();
        }

        public int CategoryId { get; set; }
        public System.DateTime StartDateTime { get; set; }
        public int DiaryId { get; set; }
        public int SportsmanId { get; set; }
        public int CampaignTimeinHour { get; set; }
        public int CampaignTypeId { get; set; }
    
        public virtual ICollection<RoutePoint> RoutePoint { get; set; }
        public virtual Category Category { get; set; }
        public virtual Diary Diary { get; set; }
        public virtual CampaignType CampaignType { get; set; }
        public virtual Sportsman Sportsman { get; set; }
        public virtual ICollection<PersonalData> PersonalData { get; set; }
    }
}
