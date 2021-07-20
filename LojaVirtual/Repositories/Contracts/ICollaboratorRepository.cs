using LojaVirtual.Models;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface ICollaboratorRepository
    {
        void Create(Collaborator collaborator);


        Collaborator Read(int id);


        IPagedList<Collaborator> ReadAll(int? page);


        List<Collaborator> GetCollaboratorByEmail(string email);


        void UpdatePassword(Collaborator collaborator);


        void Update(Collaborator collaborator);


        void Delete(int id);


        Collaborator Login(string email, string password);
    }
}
