using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.UserPlanUseCases.Commands;

public record DeleteUserPlanCommand(Guid Id) : IRequest<bool>;

internal sealed class DeleteUserPlanCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteUserPlanCommand, bool>
{
    public async Task<bool> Handle(DeleteUserPlanCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<UserPlan>();
        var userPlan = await repository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (userPlan == null)
            return false;

        await repository.DeleteAsync(userPlan, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }
}