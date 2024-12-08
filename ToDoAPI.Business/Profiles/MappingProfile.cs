using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Entities.Auth;
using ToDoAPI.Entities.DTOs;
using ToDoAPI.Entities.DTOs.ToDo;
using ToDoAPI.Entities.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoAPI.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           CreateMap<Todo, TodoDto>();

            // DTO -> Entity
            CreateMap<TodoCreateDTO, Todo>();
            // User Mapping
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Username))
                .ReverseMap();

            // RegisterDto -> User mapping
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Category Mapping
         
        }
    }
}
