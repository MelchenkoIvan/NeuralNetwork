using System;
using AutoMapper;
using NeuralNetworkCore.Models;
using NeuralNetworkDatabase.Entities;

namespace NeuralNetworkCore.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}

