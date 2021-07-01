using AutoMapper;
using LojaVirtual.Libraries.Json.Resolver;
using LojaVirtual.Libraries.Text;
using LojaVirtual.Models;
using LojaVirtual.Models.Const;
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

            CreateMap<Transaction, TransactionPagarMe>();

            CreateMap<TransactionPagarMe, Order>()
                .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(
                orig => 0
                ))
                .ForMember(
                dest => dest.ClientId,
                opt => opt.MapFrom(
                orig => int.Parse(orig.Customer.ExternalId)
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
                orig => (orig.PaymentMethod == 0) ? PaymentMethodConst.CreditCard : PaymentMethodConst.Boleto
                ))
                .ForMember(
                dest => dest.TransactionData,
                opt => opt.MapFrom(
                orig => JsonConvert.SerializeObject(orig, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
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
                orig => Mask.ConvertPagarMeIntToDecimal(orig.Amount)));

            CreateMap<List<ProductItem>, Order>()
                .ForMember(
                dest => dest.ProductsData,
                opt => opt.MapFrom(
                orig => JsonConvert.SerializeObject(orig, new JsonSerializerSettings() { ContractResolver = new ProductItemResolver<List<ProductItem>>(), ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
                ));

            CreateMap<Order, OrderSituation>()
                .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(
                orig => 0
                ))
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
                orig => JsonConvert.SerializeObject(orig, new JsonSerializerSettings() { ContractResolver = new ProductItemResolver<List<ProductItem>>(), ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
                ));

            CreateMap<CancelData, BankAccount>()
                .ForMember(
                dest => dest.BankCode,
                opt => opt.MapFrom(
                orig => orig.Bank_code
                ))
                .ForMember(
                dest => dest.Agencia,
                opt => opt.MapFrom(
                orig => orig.Agency
                ))
                .ForMember(
                dest => dest.AgenciaDv,
                opt => opt.MapFrom(
                orig => orig.Agency_dv
                ))
                .ForMember(
                dest => dest.Conta,
                opt => opt.MapFrom(
                orig => orig.Account
                ))
                .ForMember(
                dest => dest.ContaDv,
                opt => opt.MapFrom(
                orig => orig.Account_dv
                ))
                .ForMember(
                dest => dest.LegalName,
                opt => opt.MapFrom(
                orig => orig.Legal_name
                ))
                .ForMember(
                dest => dest.DocumentNumber,
                opt => opt.MapFrom(
                orig => orig.CPF
                ));
        }
    }
}