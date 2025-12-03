using ArcheryAcademy.Application.DTOs.ScheduleDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.ScheduleUseCase.Commands;

public record CreateScheduleCommand(ScheduleCreateDto ScheduleDto) : IRequest<Schedule>;

internal sealed class CreateScheduleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateScheduleCommand, Schedule>
{
    public async Task<Schedule> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        // Validar que el instructor existe
        var instructorExists = await unitOfWork.Repository<User>()
            .GetByIdAsync(request.ScheduleDto.InstructorId);
        
        if (instructorExists == null)
        {
            throw new KeyNotFoundException(
                $"Instructor with ID {request.ScheduleDto.InstructorId} not found.");
        }
        var schedule = mapper.Map<Schedule>(request.ScheduleDto);
        await unitOfWork.Repository<Schedule>().Insert(schedule);
        await unitOfWork.CompleteAsync(cancellationToken);
        return schedule;
    }
}