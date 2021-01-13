using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IColaboratorRepository
    {
        void Create(Colaborator colaborator);

        Colaborator Read(int Id);

        IEnumerable<Colaborator> ReadAll();

        void Update(Colaborator colaborator);

        void Delete(int Id);

        Colaborator Login(string Email, string Password);
    }
}
