using LojaVirtual.Models;
using Newtonsoft.Json;

namespace LojaVirtual.Libraries.Login
{
    public class ClientLogin
    {
        private string Key = "Client.Login";
        private Session.Session _session;

        public ClientLogin(Session.Session session)
        {
            _session = session;
        }

        public void Login(Client client)
        {
            //serializar
            string clientJson = JsonConvert.SerializeObject(client);

            _session.Create(Key, clientJson);
        }

        public Client GetClient()
        {
            //deserializar
            if (_session.Exist(Key))
            {
                string clienteJson = _session.Read(Key);

                //deserializar
                return JsonConvert.DeserializeObject<Client>(clienteJson);
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
