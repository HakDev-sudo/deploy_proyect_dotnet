using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.RoleUseCases.Commands;


public record DeleteRoleCommand(Guid Id) : IRequest<bool>;

internal sealed class DeleteRoleCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteRoleCommand, bool>
{
    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<Role>();
        var role = await repository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (role == null)
            return false;

        await repository.DeleteAsync(role, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }
}