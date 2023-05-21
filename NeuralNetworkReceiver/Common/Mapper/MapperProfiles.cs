using AutoMapper;
using NeuralNetworkCore.Models;
using NeuralNetworkDatabase.Entities;

namespace NeuralNetworkReceiver.Common.Mapper;

internal class NnProfile : Profile
{
    public NnProfile()
    {
        CreateMap<Symptoms, SymptomsDTO>();
    }
}