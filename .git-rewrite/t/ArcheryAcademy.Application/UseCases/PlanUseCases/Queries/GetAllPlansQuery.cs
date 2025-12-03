using ArcheryAcademy.Application.Dtos.PlanDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.PlanUseCases.Queries;

public record GetAllPlansQuery() : IRequest<List<PlanReadDto>>;

internal sealed class GetAllPlansQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllPlansQuery, List<PlanReadDto>>
{
    public async Task<List<PlanReadDto>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
    {
        var plans = await unitOfWork.Repository<Plan>().GetAllAsync();
        return mapper.Map<List<PlanReadDto>>(plans);
    }
}