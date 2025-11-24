using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.PlanUseCases.Commands;

public record DeletePlanCommand(Guid Id) : IRequest<bool>;

internal sealed class DeletePlanCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeletePlanCommand, bool>
{
    public async Task<bool> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<Plan>();
        var plan = await repository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (plan == null)
            return false;

        await repository.DeleteAsync(plan, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }
}