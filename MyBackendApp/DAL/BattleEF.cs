using MyBackendApp.Models;

namespace MyBackendApp.DAL
{
    public class BattleEF : IBattle
    {
        private readonly AppDbContext _dbcontext;
        public BattleEF(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Delete(int id)
        {
            var deleteBattle = GetById(id);
            if (deleteBattle == null)
                throw new Exception($"Data battle dengan id {id} tidak ditemukan");
            try
            {
                _dbcontext.Remove(deleteBattle);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Battle> GetAll()
        {
            var results = _dbcontext.Battles.OrderBy(b => b.Name).ToList();
            return results;
        }

        public IEnumerable<Battle> GetAllWithQuote()
        {
            throw new NotImplementedException();
        }

        public Battle GetById(int id)
        {
            var result = _dbcontext.Battles.FirstOrDefault(b => b.BattleId == id);

            if (result == null)
                throw new Exception($"Data battleid {id} tidak ditemukan");
            return result;
        }

        public IEnumerable<Battle> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Battle Insert(Battle battle)
        {
            try
            {
                _dbcontext.Battles.Add(battle);
                _dbcontext.SaveChanges();
                return battle;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Battle Update(Battle battle)
        {
            var updateBattle = GetById(battle.BattleId);
            if (updateBattle == null)
                throw new Exception($"Data samurai id {battle.BattleId} tidak ditemukan");

            try
            {
                updateBattle.Name = battle.Name;
                _dbcontext.SaveChanges();
                return updateBattle;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}