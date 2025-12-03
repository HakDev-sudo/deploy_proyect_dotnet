using ArcheryAcademy.Application.DTOs.RolDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.RoleUseCases.Commands;

public record UpdateRoleCommand(Guid RoleGuid, RolUpdateDto RolDto) : IRequest<Role?>;

internal sealed class UpdateRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateRoleCommand, Role?>
{
    public async Task<Role?> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        // Buscar la entidad existente
        var existing = await unitOfWork.Repository<Role>().FirstOrDefaultAsync(x => x.Id == request.RoleGuid, cancellationToken);

        if (existing is null)
            return null;

        // Mapear los nuevos valores sobre la entidad existente
        mapper.Map(request.RolDto, existing);

        // Actualizar en base de datos
        await unitOfWork.Repository<Role>().UpdateAsync(existing);
        await unitOfWork.CompleteAsync(cancellationToken);

        return existing;
    }
}