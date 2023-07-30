using AutoMapper;
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
        }
    }
}
