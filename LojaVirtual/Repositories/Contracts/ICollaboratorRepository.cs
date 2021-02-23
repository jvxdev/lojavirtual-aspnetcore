using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface ICollaboratorRepository
    {
        void Create(Collaborator collaborator);


        Collaborator Read(int Id);


        IPagedList<Collaborator> ReadAll(int? page);


        List<Collaborator> GetCollaboratorEmail(string email);


        void UpdatePassword(Collaborator collaborator);


        void Update(Collaborator collaborator);


        void Delete(int Id);


        Collaborator Login(string Email, string Password);
    }
}
