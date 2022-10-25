using Microsoft.EntityFrameworkCore;
using MyBackendApp.Models;

namespace MyBackendApp.DAL
{
    public class SamuraiEF : ISamurai
    {
        private AppDbContext _dbcontext;
        public SamuraiEF(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void AddHorse(Horse horse)
        {
            try
            {
                _dbcontext.Add(horse);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public void AddSamuraiToBattle(int samuraiId, int battleId)
        {
            try
            {
                var samurai = _dbcontext.Samurais.FirstOrDefault(s => s.Id == samuraiId);
                var battle = _dbcontext.Battles.FirstOrDefault(b => b.BattleId == battleId);
                if (samurai != null && battle != null)
                {
                    //battle.Samurais = new List<Samurai>();
                    battle.Samurais.Add(samurai);
                    _dbcontext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            var deleteSamurai = GetById(id);
            if (deleteSamurai == null)
                throw new Exception($"Data samurai dengan id {id} tidak ditemukan");
            try
            {
                _dbcontext.Remove(deleteSamurai);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Samurai> GetAll()
        {
            var results = _dbcontext.Samurais.OrderBy(s => s.Name).ToList();
            /*var results = (from s in _dbcontext.Samurais
                          orderby s.Name ascending
                          select s).ToList();*/

            return results;
        }

        public IEnumerable<Samurai> GetAllSamuraisWithBattles()
        {
            var samurais = _dbcontext.Samurais.Include(s => s.Battles).ToList();
            return samurais;
        }

        public IEnumerable<Samurai> GetAllSamuraiWithHorse()
        {
            var samurais = _dbcontext.Samurais.Include(s => s.Horse).ToList();
            return samurais;
        }

        public IEnumerable<Samurai> GetAllWithQuery()
        {
            var samurais = _dbcontext.Samurais.FromSqlRaw("select * from Samurais order by Name asc").ToList();
            return samurais;
        }

        public IEnumerable<Samurai> GetAllWithQuote()
        {
            var results = _dbcontext.Samurais.Include(s => s.Quotes);
            return results;
        }

        public Samurai GetById(int id)
        {
            var result = _dbcontext.Samurais.FirstOrDefault(s => s.Id == id);
            /*var result = (from s in _dbcontext.Samurais
                         where s.Id == id
                         select s).FirstOrDefault();*/

            if (result == null)
                throw new Exception($"Data id {id} tidak ditemukan");
            return result;
        }

        public IEnumerable<Samurai> GetByName(string name)
        {
            var results = _dbcontext.Samurais.Where(s => s.Name.Contains(name)).OrderBy(s => s.Name);
            return results;
        }

        public IEnumerable<Samurai> GetSamuraiWhoSaidWord(string text)
        {
            var samurais = _dbcontext.Samurais
                .FromSqlInterpolated($"exec dbo.SamuraisWhoSaidAWord {text}").ToList();
            return samurais;
        }

        public Samurai GetSamuraiWithBattle(int samuraiId)
        {
            var result = _dbcontext.Samurais.Include(s => s.Battles)
                .FirstOrDefault(s => s.Id == samuraiId);
            if (result == null)
                throw new Exception($"samurai id {samuraiId} tidak ditemukan");

            return result;
        }

        public Samurai Insert(Samurai samurai)
        {
            try
            {
                _dbcontext.Samurais.Add(samurai);
                _dbcontext.SaveChanges();
                return samurai;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveBattleFromSamurai(int samuraiId, int battleId)
        {
            try
            {
                var battleWithSamurai = _dbcontext.Battles.Include(b => b.Samurais.Where(s => s.Id == samuraiId))
                .FirstOrDefault(s => s.BattleId == battleId);
                var samurai = battleWithSamurai.Samurais[0];
                battleWithSamurai.Samurais.Remove(samurai);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveQuotesFromSamurai(int samuraiId)
        {
            try
            {
                _dbcontext.Database.ExecuteSqlInterpolated($"exec dbo.DeleteQuotesForSamurai {samuraiId}");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Samurai Samurai(Samurai samurai)
        {
            throw new NotImplementedException();
        }

        public Samurai Update(Samurai samurai)
        {
            var updateSamurai = GetById(samurai.Id);
            if (updateSamurai == null)
                throw new Exception($"Data samurai id {samurai.Id} tidak ditemukan");

            try
            {
                updateSamurai.Name = samurai.Name;
                _dbcontext.SaveChanges();
                return samurai;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        object ISamurai.Insert(Samurai samurai)
        {
            throw new NotImplementedException();
        }
    }
}