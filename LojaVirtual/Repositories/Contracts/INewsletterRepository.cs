using LojaVirtual.Models;
using System.Collections.Generic;

namespace LojaVirtual.Repositories.Contracts
{
    public interface INewsletterRepository
    {
        void Create(NewsletterEmail newsletter);


        IEnumerable<NewsletterEmail> ReadAll();
    }
}
