using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories.Contracts
{
    public interface INewsletterRepository
    {
        void Create(NewsletterEmail newsletter);

        IEnumerable<NewsletterEmail> ReadAll();
    }
}
