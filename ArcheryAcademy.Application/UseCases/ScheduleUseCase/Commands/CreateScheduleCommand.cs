using ArcheryAcademy.Application.DTOs.ScheduleDto;
using ArcheryAcademy.Domain.Ports;
using ArcheryAcademy.Domain.Entities;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.ScheduleUseCase.Commands;

public record CreateScheduleCommand(ScheduleCreateDto Schedule) : IRequest<ScheduleReadDto>;

internal sealed class CreateScheduleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateScheduleCommand, ScheduleReadDto>
{
    public async Task<ScheduleReadDto> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        // Validaciones de negocio:
        // - Validar que StartTime < EndTime
        // - Validar que MaxStudents > 0
        // - NOTA: Verificar que instructor existe y tiene rol adecuado
        // - NOTA: Verificar que no hay conflicto de horarios para el mismo instructor

        var schedule = mapper.Map<Schedule>(request.Schedule);
        schedule.Id = Guid.NewGuid();
        schedule.IsActive = true;
        schedule.CreatedAt = DateTime.UtcNow;

        await unitOfWork.Repository<Schedule>().AddAsync(schedule);
        await unitOfWork.SaveChangesAsync();

        // Obtener el schedule creado con sus relaciones (si es necesario)
        var createdSchedule = await unitOfWork.Repository<Schedule>().GetByIdAsync(schedule.Id);

        return mapper.Map<ScheduleReadDto>(createdSchedule);
    }
}