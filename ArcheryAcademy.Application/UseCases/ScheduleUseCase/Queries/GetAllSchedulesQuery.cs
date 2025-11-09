using ArcheryAcademy.Application.DTOs.ScheduleDto;
using ArcheryAcademy.Domain.Ports;
using ArcheryAcademy.Infrastructure.Persistence.Models;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.ScheduleUseCase.Queries;

public record GetAllSchedulesQuery() : IRequest<List<ScheduleReadDto>>;

internal sealed class GetAllSchedulesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllSchedulesQuery, List<ScheduleReadDto>>
{
    public async Task<List<ScheduleReadDto>> Handle(GetAllSchedulesQuery request, CancellationToken cancellationToken)
    {
        var schedules = await unitOfWork.Repository<Schedule>().GetAllAsync();
        return mapper.Map<List<ScheduleReadDto>>(schedules);
    }
}