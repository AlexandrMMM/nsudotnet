using System.Linq;
using TouristClub.Data;
using TouristClub.Data.Entity;
using TouristClub.Logic.Interface;

namespace TouristClub.Logic.Realisation
{
    public class DiaryCrudService : CrudService<Diary>, IDiaryCrudService
    {
        private DataContext _context;

        public DiaryCrudService(DataContext context) : base(context)
        {
            _context = context;
        }

        public new void Update(Diary Tdiary)
        {
            var diary = _context.DiarySet.First(e => e.Id == Tdiary.Id);
            diary.Name = Tdiary.Name;
            _context.SaveChanges();
        }
    }
}