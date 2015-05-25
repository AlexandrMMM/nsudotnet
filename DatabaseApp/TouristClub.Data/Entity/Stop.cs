namespace TouristClub.Data.Entity
{
    public partial class Stop : Entity
    {
        public System.DateTime DateTime { get; set; }
        public int StopTimeInMinutes { get; set; }
        public int RoutePointId { get; set; }
        public int DiaryId { get; set; }
    
        public virtual RoutePoint RoutePoint { get; set; }
        public virtual Diary Diary { get; set; }
    }
}