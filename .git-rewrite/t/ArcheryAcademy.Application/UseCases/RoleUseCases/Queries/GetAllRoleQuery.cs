using ArcheryAcademy.Application.DTOs.RolDto;
using ArcheryAcademy.Application.DTOs.UserPlanDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.RoleUseCases.Queries;

public record GetAllRoleQuery() : IRequest<List<RolReadDto>>;

internal sealed class GetAllRoleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllRoleQuery, List<RolReadDto>>
{
    public async Task<List<RolReadDto>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
    {
        // Obtener todos los registros desde el repositorio genérico
        var role = await unitOfWork.Repository<Role>().GetAllAsync();

        // Mapear entidades → DTOs de lectura
        return mapper.Map<List<RolReadDto>>(role);
    }
}