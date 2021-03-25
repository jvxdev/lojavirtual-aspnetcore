using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSCorreios;

namespace LojaVirtual.Libraries.Manager.Shipping
{
    public class WSCorreiosCalcularFrete
    {
        private IConfiguration _configutarion;
        private CalcPrecoPrazoWSSoap _service;


        public WSCorreiosCalcularFrete(IConfiguration configuration, CalcPrecoPrazoWSSoap service)
        {
            _configutarion = configuration;
            _service = service;
        }


        public void CalcularValorPrazoFrete(String cepDestino, String tipoFrete, Package package)
        {
            var cepOrigem = _configutarion.GetValue<String>("Frete:cepOrigem");
            var maoPropria = _configutarion.GetValue<String>("Frete:maoPropria");
            var avisoDeRecebimento = _configutarion.GetValue<String>("Frete:avisoDeRecebimento");
            _service.CalcPrecoPrazoAsync("", "", tipoFrete,);
        }
    }
}
