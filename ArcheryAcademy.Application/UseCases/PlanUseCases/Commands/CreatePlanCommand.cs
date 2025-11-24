using ArcheryAcademy.Application.Dtos.PlanDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.PlanUseCases.Commands;

public record CreatePlanCommand(PlanCreateDto PlanDto) : IRequest<Plan>;

internal sealed class CreatePlanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreatePlanCommand, Plan>
{
    public async Task<Plan> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
    {
        var plan = mapper.Map<Plan>(request.PlanDto);
        await unitOfWork.Repository<Plan>().Insert(plan);
        await unitOfWork.CompleteAsync(cancellationToken);
        return plan; // Devuelve la entidad con el Guid generado
    }
}