using LojaVirtual.Database;
using LojaVirtual.Models;
using System.Collections.Generic;
using System.Linq;

namespace LojaVirtual.Repositories.Contracts
{
    public class NewsletterRepository : INewsletterRepository
    {
        private LojaVirtualContext _database;

        public NewsletterRepository(LojaVirtualContext database)
        {
            _database = database;
        }

        public void Create(NewsletterEmail newsletter)
        {
            _database.Newsletter.Add(newsletter);
            _database.SaveChanges();
        }

        public IEnumerable<NewsletterEmail> ReadAll()
        {
            return _database.Newsletter.ToList();
        }
    }
}
