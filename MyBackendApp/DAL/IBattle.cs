using MyBackendApp.Models;

namespace MyBackendApp.DAL
{
    public interface IBattle
    {
        public IEnumerable<Battle> GetAll();
        public IEnumerable<Battle> GetAllWithQuote();
        public Battle GetById(int id);
        public IEnumerable<Battle> GetByName(string name);
        public Battle Insert(Battle battle);
        public Battle Update(Battle battle);
        public void Delete(int id);
    }
}