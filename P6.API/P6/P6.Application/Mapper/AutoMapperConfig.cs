using AutoMapper;
using P6.Application.DTOs;
using P6.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P6.Application.Mapper
{
    public class AutoMapperConfig :Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, SignupModel>().ReverseMap();
            CreateMap<User, UserSaveDTO>().ReverseMap();
        }
    }
}
