using ArcheryAcademy.Application.Dtos.PaymentDto;
using ArcheryAcademy.Application.Dtos.PlanDto;
using ArcheryAcademy.Application.DTOs.RolDto;
using ArcheryAcademy.Application.DTOs.UserPlanDto;
using ArcheryAcademy.Application.DTOs.ScheduleDto;
using ArcheryAcademy.Infrastructure.Persistence.Entities;
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

        //Schedule
        CreateMap<Schedule, ScheduleReadDto>().ReverseMap();
        CreateMap<ScheduleCreateDto, Schedule>().ReverseMap();
        CreateMap<ScheduleUpdateDto, Schedule>().ReverseMap();
        
        //Payment
        CreateMap<Payment, PaymentReadDto>().ReverseMap();
        CreateMap<PaymentCreateDto, Payment>().ReverseMap();
        CreateMap<PaymentUpdateDto, Payment>().ReverseMap();
        
        //Plan
        CreateMap<Plan, PlanReadDto>().ReverseMap();
        CreateMap<PlanCreateDto, Plan>().ReverseMap();
        CreateMap<PlanUpdateDto, Plan>().ReverseMap();
        
        //Role
        CreateMap<Role, RolCreateDto>().ReverseMap();
        CreateMap<Role, RolUpdateDto>().ReverseMap();
        CreateMap<Role, RolUpdateDto>().ReverseMap();
    }
}