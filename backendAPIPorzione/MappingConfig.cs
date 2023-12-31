﻿using AutoMapper;
using backendAPIPorzione.Models;
using backendAPIPorzione.Models.Dto;

namespace backendAPIPorzione
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Producto, UpdateProductoDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, UpdateUsuarioDto>().ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<Detalle, DetalleDto>().ReverseMap();

        }
    }
}
