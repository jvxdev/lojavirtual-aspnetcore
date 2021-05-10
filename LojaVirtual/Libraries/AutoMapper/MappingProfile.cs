using AutoMapper;
using LojaVirtual.Libraries.Text;
using LojaVirtual.Models;
using LojaVirtual.Models.ProductAggregator;
using Newtonsoft.Json;
using PagarMe;
using System;
using System.Collections.Generic;

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
                orig => (orig.PaymentMethod == 0) ? PaymentMethod.CreditCard : PaymentMethod.Boleto
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

            CreateMap<List<ProductItem>, Order>()
                .ForMember(
                dest => dest.ProductsData,
                opt => opt.MapFrom(
                orig => JsonConvert.SerializeObject(orig)
                ));

            CreateMap<Order, OrderSituation>()
                .ForMember(
                dest => dest.OrderId,
                opt => opt.MapFrom(
                orig => orig.Id
                ))
                .ForMember(
                dest => dest.Date,
                opt => opt.MapFrom(
                orig => DateTime.Now
                ));

            CreateMap<ProductTransaction, OrderSituation>()
                .ForMember(
                dest => dest.Data,
                opt => opt.MapFrom(
                orig => JsonConvert.SerializeObject(orig)
                ));
        }
    }


    public static class Extension
    {
        public static TDestination Map<TSource, TDestination>(this TDestination destination, TSource source)
        {
            return Mapper.Map(source, destination);
        }
    }
}