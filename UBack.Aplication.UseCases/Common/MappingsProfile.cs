using AutoMapper;
using UBack.Aplication.Dtos;
using UBack.Domain.Dominio.Entities;

namespace UBack.Aplication.UseCases.Common
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Inscripcion, InscripcionDTO>().ReverseMap();
            CreateMap<Materia, MateriaDTO>().ReverseMap();
            CreateMap<Rol, RolDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();

        }
    }
}
