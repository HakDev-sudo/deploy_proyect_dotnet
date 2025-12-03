using ArcheryAcademy.Application.DTOs.ScheduleDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.ScheduleUseCase.Commands;

public record UpdateScheduleCommand(Guid ScheduleId, ScheduleUpdateDto ScheduleDto) : IRequest<Schedule?>;

internal sealed class UpdateScheduleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateScheduleCommand, Schedule?>
{
    public async Task<Schedule?> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
    {
        var existing = await unitOfWork.Repository<Schedule>().FirstOrDefaultAsync(x => x.Id == request.ScheduleId, cancellationToken);

        if (existing is null)
            return null;
        mapper.Map(request.ScheduleDto, existing);

        await unitOfWork.Repository<Schedule>().UpdateAsync(existing);
        await unitOfWork.CompleteAsync(cancellationToken);

        return existing;
    }
}