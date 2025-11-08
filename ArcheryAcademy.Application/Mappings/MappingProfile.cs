using ArcheryAcademy.Application.DTOs.UserPlanDto;
using ArcheryAcademy.Infrastructure.Persistence.Models;
using AutoMapper;

namespace ArcheryAcademy.Application.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        //User_plan
        CreateMap<UserPlan, UserPlanReadDto>().ReverseMap();
        CreateMap<UserPlanCreateDto, UserPlan>().ReverseMap();
        CreateMap<UserPlanUpdateDto, UserPlan>().ReverseMap();
    }
}