namespace TouristClub.Data.Entity
{
    public partial class Traning : global::TouristClub.Data.Entity.Entity
    {
        public string Place { get; set; }
        public System.DateTime DateTime { get; set; }
        public int TreningTimeInMinutes { get; set; }
        public int TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; }
    }
}