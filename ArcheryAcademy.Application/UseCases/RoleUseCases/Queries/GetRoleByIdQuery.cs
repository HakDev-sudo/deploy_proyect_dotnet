using ArcheryAcademy.Application.DTOs.RolDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.RoleUseCases.Queries;

public record GetRoleByIdQuery(Guid Id) : IRequest<RolReadDto?>;

internal sealed class GetRoleByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetRoleByIdQuery, RolReadDto?>
{
    public async Task<RolReadDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<Role>().FirstOrDefaultAsync(
            x => x.Id == request.Id, cancellationToken
        );

        if (entity == null)
            return null;

        return mapper.Map<RolReadDto>(entity);
    }
}