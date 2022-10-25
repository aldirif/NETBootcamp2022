using MyBackendApp.Models;

namespace MyBackendApp.DAL
{
    public interface ISamurai
    {
        public IEnumerable<Samurai> GetAll();
        public IEnumerable<Samurai> GetAllWithQuote();
        public Samurai GetById(int id);
        public IEnumerable<Samurai> GetByName(string name);
        public Samurai Samurai(Samurai samurai);
        public Samurai Update(Samurai samurai);
        public void Delete(int id);

        //mendaftarkan samurai yang sudah ada ke battle yang sudah ada
        public void AddSamuraiToBattle(int samuraiId, int battleId);
        public void AddHorse(Horse horse);
        public IEnumerable<Samurai> GetAllSamuraiWithHorse();

        public Samurai GetSamuraiWithBattle(int samuraiId);
        public IEnumerable<Samurai> GetAllSamuraisWithBattles();

        public void RemoveBattleFromSamurai(int samuraiId, int battleId);

        public IEnumerable<Samurai> GetAllWithQuery();
        public IEnumerable<Samurai> GetSamuraiWhoSaidWord(string text);
        public void RemoveQuotesFromSamurai(int samuraiId);
        object Insert(Samurai samurai);
    }
}