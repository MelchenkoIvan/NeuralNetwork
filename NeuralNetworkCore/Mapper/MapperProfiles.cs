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
    public class NNProfile : Profile
    {
        public NNProfile()
        {
            CreateMap<Symptoms, SymptomsDTO>();
            CreateMap<Symptoms, ResultDTO>();
        }
    }
}

