using LojaVirtual.Models;
using Newtonsoft.Json;

namespace LojaVirtual.Libraries.Login
{
    public class CollaboratorLogin
    {
        private string Key = "Collaborator.Login";
        private Session.Session _session;


        public CollaboratorLogin(Session.Session session)
        {
            _session = session;
        }

        public void Login(Collaborator collaborator)
        {
            //serializar
            string colaboratorJson = JsonConvert.SerializeObject(collaborator);

            _session.Create(Key, colaboratorJson);
        }

        public Collaborator GetCollaborator()
        {
            if (_session.Exist(Key))
            {
                string collaboratorJson = _session.Read(Key);

                //deserializar
                return JsonConvert.DeserializeObject<Collaborator>(collaboratorJson);
            }
            else
            {
                return null;
            }
        }

        public void Logout()
        {
            _session.DeleteAll();
        }
    }
}
