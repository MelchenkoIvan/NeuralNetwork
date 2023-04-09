using System;
using AutoMapper;
using NeuralNetowrkCore.Models;
using NeuralNetworkDatabase.Entities;

namespace NeuralNetowrkCore.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}

