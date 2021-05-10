using LojaVirtual.Models;
using LojaVirtual.Models.Const;
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
                var result = await CalcularValorPrazoFrete(cepDestino, tipoFrete, package);

                if (result != null)
                {
                    valorDosPacotesPorFrete.Add(result);
                }
            }

            if (valorDosPacotesPorFrete.Count > 0)
            {
                ValorPrazoFrete valorDosFretes = valorDosPacotesPorFrete
                    .GroupBy(a => a.TipoFrete)
                    .Select(list => new ValorPrazoFrete
                    {
                        TipoFrete = list.First().TipoFrete,
                        CodTipoFrete = list.First().CodTipoFrete,
                        Prazo = list.Max(c => c.Prazo),
                        Valor = list.Sum(c => c.Valor)
                    }).ToList().First();

                return valorDosFretes;
            }

            return null;
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
                var cleanValue = result.Servicos[0].Valor.Replace(".", "");
                var finalValue = double.Parse(cleanValue);

                return new ValorPrazoFrete()
                {
                    TipoFrete = CorreiosConst.GetName(tipoFrete),
                    CodTipoFrete = tipoFrete,
                    Prazo = int.Parse(result.Servicos[0].PrazoEntrega),
                    Valor = finalValue
                };
            }
            else if (result.Servicos[0].Erro == "008" || result.Servicos[0].Erro == "-888")
            {
                //SEDEX10 - Não entrega nessa região em específico
                return null;
            }
            else
            {
                throw new Exception("Erro: " + result.Servicos[0].MsgErro);
            }
        }
    }
}
