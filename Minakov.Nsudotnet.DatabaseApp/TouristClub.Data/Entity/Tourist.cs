namespace TouristClub.Data.Entity
{
    public partial class Tourist : global::TouristClub.Data.Entity.Entity
    {
        public int GroupId { get; set; }
        public int PersonalDataId { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual PersonalData PersonalData { get; set; }
    }
}