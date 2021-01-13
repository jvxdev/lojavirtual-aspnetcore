using LojaVirtual.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Login
{
    public class ColaboratorLogin
    {
        private string Key = "Colaborator.Login";
        private Session.Session _session;

        public ColaboratorLogin(Session.Session session)
        {
            _session = session;
        }

        public void Login(Colaborator colaborator)
        {
            //serializar
            string colaboratorJson = JsonConvert.SerializeObject(colaborator);

            _session.Create(Key, colaboratorJson);
        }

        public Colaborator getColaborator()
        {
            if (_session.Exist(Key))
            {
                string colaboratorJson = _session.Read(Key);

                //deserializar
                return JsonConvert.DeserializeObject<Colaborator>(colaboratorJson);
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
