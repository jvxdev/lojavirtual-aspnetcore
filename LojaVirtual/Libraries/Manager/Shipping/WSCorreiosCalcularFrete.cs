using LojaVirtual.Models;
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


        public async Task<ValorPrazoFrete> CalcularFrete(String cepDestino, String tipoFrete, List<Package> packages)
        {
            List<ValorPrazoFrete> valorDosPacotesPorFrete = new List<ValorPrazoFrete>();

            foreach (var package in packages)
            {
                valorDosPacotesPorFrete.Add(await CalcularValorPrazoFrete(cepDestino, tipoFrete, package));
            }

            ValorPrazoFrete valorDosFretes = valorDosPacotesPorFrete
                .GroupBy(a => a.TipoFrete)
                .Select(list => new ValorPrazoFrete
                {
                    TipoFrete = list.First().TipoFrete,
                    Prazo = list.Max(c => c.Prazo),
                    Valor = list.Sum(c => c.Valor)
                }).ToList().First();

            return valorDosFretes;
        }


        private async Task<ValorPrazoFrete> CalcularValorPrazoFrete(String cepDestino, String tipoFrete, Package package)
        {
            var cepOrigem = _configutarion.GetValue<String>("Frete:cepOrigem");
            var maoPropria = _configutarion.GetValue<String>("Frete:maoPropria");
            var avisoDeRecebimento = _configutarion.GetValue<String>("Frete:avisoDeRecebimento");
            var Diameter = Math.Max(Math.Max(package.Lenght, package.Width), package.Height);

            cResultado result = await _service.CalcPrecoPrazoAsync("", "", tipoFrete, cepOrigem, cepDestino, package.Weight.ToString(), 1, package.Lenght, package.Height, package.Width, Diameter, maoPropria, 0, avisoDeRecebimento);
        
            if (result.Servicos[0].Erro == "0")
            {
                return new ValorPrazoFrete()
                {
                    TipoFrete = tipoFrete,
                    Prazo = int.Parse(result.Servicos[0].PrazoEntrega),
                    Valor = double.Parse(result.Servicos[0].Valor.Replace(".", "").Replace(",", "."))
                };
            }
            else
            {
                throw new Exception("Erro: " + result.Servicos[0].MsgErro);
            }
        }
    }
}
