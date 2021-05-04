using AutoMapper;
using LojaVirtual.Libraries.Text;
using LojaVirtual.Models;
using LojaVirtual.Models.ProductAggregator;
using Newtonsoft.Json;
using PagarMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductItem>();
            CreateMap<Client, DeliveryAddress>()
                .ForMember(
                dest => dest.Id, 
                opt => opt.MapFrom(
                orig => 0
                ))
                .ForMember(
                dest => dest.AddressName, 
                opt => opt.MapFrom(
                orig => string.Format("Endereço do cliente ({0})", orig.Name)));
            CreateMap<Transaction, Order>()
                .ForMember(
                dest => dest.ClientId,
                opt => opt.MapFrom(
                orig => int.Parse(orig.Customer.Id)
                ))
                .ForMember(
                dest => dest.TransactionId,
                opt => opt.MapFrom(
                orig => orig.Id
                ))
                .ForMember(
                dest => dest.FreteCompany,
                opt => opt.MapFrom(
                orig => "ECT - Correios"
                ))
                .ForMember(
                dest => dest.PaymentForm,
                opt => opt.MapFrom(
                orig => (orig.PaymentMethod == 0) ? "Cartão de crédito" : "Boleto bancário"
                ))
                .ForMember(
                dest => dest.TransactionData,
                opt => opt.MapFrom(
                orig => JsonConvert.SerializeObject(orig)
                ))
                .ForMember(
                dest => dest.RegistryDate,
                opt => opt.MapFrom(
                orig => DateTime.Now
                ))
                .ForMember(
                dest => dest.RegistryDate,
                opt => opt.MapFrom(
                orig => DateTime.Now
                ))
                .ForMember(
                dest => dest.TotalValue,
                opt => opt.MapFrom(
                orig => orig.Amount
                ))
                .ForMember(
                dest => dest.RegistryDate,
                opt => opt.MapFrom(
                orig => Mask.ConvertPagarMeIntToDecimal(orig.Amount)
                ));
        }
    }
}
