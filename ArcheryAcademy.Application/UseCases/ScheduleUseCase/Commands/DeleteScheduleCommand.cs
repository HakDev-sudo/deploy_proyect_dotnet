using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.ScheduleUseCase.Commands;

public record DeleteScheduleCommand(Guid Id) : IRequest<bool>;

internal sealed class DeleteScheduleCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteScheduleCommand, bool>
{
    public async Task<bool> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<Schedule>();
        var schedule = await repository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (schedule == null)
            return false;

        await repository.DeleteAsync(schedule, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }
}