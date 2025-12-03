using ArcheryAcademy.Application.DTOs.RolDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.RoleUseCases.Commands;

public record CreateRoleCommand(RolCreateDto RolDto) : IRequest<Role>;

internal sealed class CreateRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateRoleCommand, Role>
{
    public async Task<Role> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = mapper.Map<Role>(request.RolDto);
        await unitOfWork.Repository<Role>().Insert(role);
        await unitOfWork.CompleteAsync(cancellationToken);
        return role;
    }
}