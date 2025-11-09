using ArcheryAcademy.Application.DTOs.ScheduleDto;
using ArcheryAcademy.Domain.Ports;
using ArcheryAcademy.Infrastructure.Persistence.Entities;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.ScheduleUseCase.Commands;

public record CreateScheduleCommand(ScheduleCreateDto ScheduleDto) : IRequest<Schedule>;

internal sealed class CreateScheduleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateScheduleCommand, Schedule>
{
    public async Task<Schedule> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = mapper.Map<Schedule>(request.ScheduleDto);
        await unitOfWork.Repository<Schedule>().Insert(schedule);
        await unitOfWork.CompleteAsync(cancellationToken);
        return schedule;
    }
}