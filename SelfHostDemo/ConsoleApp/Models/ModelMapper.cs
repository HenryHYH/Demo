using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ConsoleApp.Core.Domain.Users;
using ConsoleApp.Models.Users;

namespace ConsoleApp.Models
{
    public class ModelMapper
    {
        public static void MapModels()
        {
            Mapper.CreateMap<User, UserModel>()
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.Age, m => m.MapFrom(s => s.Age));
            Mapper.CreateMap<UserModel, User>()
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.Age, m => m.MapFrom(s => s.Age));
        }
    }
}
