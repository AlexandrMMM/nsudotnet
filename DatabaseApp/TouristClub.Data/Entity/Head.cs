using System.Collections.Generic;

namespace TouristClub.Data.Entity
{
    
    public partial class Head : global::TouristClub.Data.Entity.Entity
    {
        public System.DateTime EmployDate { get; set; }
        public int PersonalDataId { get; set; }
        public int SectionId { get; set; }

        public virtual PersonalData PersonalData { get; set; }
        public virtual Section Section { get; set; }
    }
}