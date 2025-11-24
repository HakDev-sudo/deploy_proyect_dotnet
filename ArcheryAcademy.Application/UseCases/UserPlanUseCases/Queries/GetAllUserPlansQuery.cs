using ArcheryAcademy.Application.DTOs.UserPlanDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.UserPlanUseCases.Queries;

public record GetAllUserPlansQuery() : IRequest<List<UserPlanReadDto>>;

internal sealed class GetAllUserPlansQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllUserPlansQuery, List<UserPlanReadDto>>
{
    public async Task<List<UserPlanReadDto>> Handle(GetAllUserPlansQuery request, CancellationToken cancellationToken)
    {
        // Obtener todos los registros desde el repositorio genérico
        var userPlans = await unitOfWork.Repository<UserPlan>().GetAllAsync();

        // Mapear entidades → DTOs de lectura
        return mapper.Map<List<UserPlanReadDto>>(userPlans);
    }
}