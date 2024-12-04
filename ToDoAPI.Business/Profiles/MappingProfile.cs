using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Entities.Auth;
using ToDoAPI.Entities.DTOs;
using ToDoAPI.Entities.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoAPI.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
             CreateMap<Todo, TodoDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username)).ReverseMap(); ;
         

            // User Mapping
            CreateMap<User, UserDto>()
                 .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Username)).
                 ReverseMap();
            CreateMap<RegisterDto, User>() // RegisterDto -> User
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            // Category Mapping
            CreateMap<Category, CategoryDto>().ReverseMap(); ;
        }
    }
}
