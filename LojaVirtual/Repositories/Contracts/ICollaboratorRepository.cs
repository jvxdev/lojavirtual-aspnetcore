using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories.Contracts
{
    public interface ICollaboratorRepository
    {
        void Create(Collaborator collaborator);


        Collaborator Read(int Id);


        IEnumerable<Collaborator> ReadAll();


        void Update(Collaborator collaborator);


        void Delete(int Id);


        Collaborator Login(string Email, string Password);
    }
}
