using Microsoft.EntityFrameworkCore;
using MyBackendApp.Models;

namespace MyBackendApp.DAL
{
    public class QuoteEF : IQuote
    {
        private AppDbContext _dbcontext;
        public QuoteEF(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Delete(int id)
        {
            try
            {
                var deleteQuote = GetById(id);
                _dbcontext.Quotes.Remove(deleteQuote);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Quote> GetAll(int samuraiId)
        {
            var quotes = _dbcontext.Quotes.Include(q => q.Samurai)
                .Where(q => q.SamuraiId == samuraiId);
            return quotes;
        }

        public IEnumerable<Quote> GetAll()
        {
            var quotes = _dbcontext.Quotes.Include(q => q.Samurai);
            return quotes;
        }

        public Quote GetById(int id)
        {
            var quote = _dbcontext.Quotes.Include(q => q.Samurai).FirstOrDefault(q => q.Id == id);
            if (quote == null)
                throw new Exception($"Data quote id {id} tidak ditemukan");
            return quote;
        }

        public IEnumerable<Quote> GetByText(string text)
        {
            var quotes = _dbcontext.Quotes.Where(q => q.Text.Contains(text));
            return quotes;
        }

        public Quote Insert(Quote quote)
        {
            try
            {
                _dbcontext.Quotes.Add(quote);
                _dbcontext.SaveChanges();
                return quote;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Quote Update(Quote quote)
        {
            try
            {
                var quoteUpdate = GetById(quote.Id);
                quoteUpdate.Text = quote.Text;
                quoteUpdate.SamuraiId = quote.SamuraiId;
                _dbcontext.SaveChanges();

                return quoteUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}