using ArcheryAcademy.Application.Dtos.PlanDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.PlanUseCases.Queries;

public record GetPlanByIdQuery(Guid Id) : IRequest<PlanReadDto?>;

internal sealed class GetPlanByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetPlanByIdQuery, PlanReadDto?>
{
    public async Task<PlanReadDto?> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<Plan>().FirstOrDefaultAsync(
            x => x.Id == request.Id, cancellationToken
        );

        if (entity == null)
            return null;

        return mapper.Map<PlanReadDto>(entity);
    }
}