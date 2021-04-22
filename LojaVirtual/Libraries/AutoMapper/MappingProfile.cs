﻿using AutoMapper;
using LojaVirtual.Models;
using LojaVirtual.Models.ProductAggregator;
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
                .ForMember(dest => dest.Id, 
                opt => opt.MapFrom(
                orig => 0))
                .ForMember(
                dest => dest.AddressName, 
                opt => opt.MapFrom(
                orig => string.Format("Endereço do cliente ({0})", orig.Name)));
        }
    }
}
