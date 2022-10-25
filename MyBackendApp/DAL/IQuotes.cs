using MyBackendApp.Models;

namespace MyBackendApp.DAL
{
    public interface IQuote
    {
        public IEnumerable<Quote> GetAll();
        public IEnumerable<Quote> GetAll(int samuraiId);
        public Quote GetById(int id);
        public IEnumerable<Quote> GetByText(string text);
        public Quote Insert(Quote quote);
        public Quote Update(Quote quote);
        public void Delete(int id);
    }
}