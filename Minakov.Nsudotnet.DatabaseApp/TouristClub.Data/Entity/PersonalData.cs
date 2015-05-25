using System.Collections.Generic;

namespace TouristClub.Data.Entity
{    
    public partial class PersonalData : global::TouristClub.Data.Entity.Entity
    {
        public PersonalData()
        {
            Head = new HashSet<Head>();
            Tourist = new HashSet<Tourist>();
            Sportsman = new HashSet<Sportsman>();
            Campaign = new HashSet<Campaign>();
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Gender { get; set; }
        public System.DateTime BirthDate { get; set; }

        public virtual ICollection<Head> Head { get; set; }
        public virtual ICollection<Tourist> Tourist { get; set; }
        public virtual ICollection<Sportsman> Sportsman { get; set; }
        public virtual ICollection<Campaign> Campaign { get; set; }
    }
}