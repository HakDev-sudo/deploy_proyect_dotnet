using ArcheryAcademy.Application.Dtos.PlanDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.PlanUseCases.Commands;

public record UpdatePlanCommand(Guid PlanId, PlanUpdateDto PlanDto) : IRequest<Plan?>;

internal sealed class UpdatePlanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdatePlanCommand, Plan?>
{
    public async Task<Plan?> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
    {
        var existing = await unitOfWork.Repository<Plan>().FirstOrDefaultAsync(x => x.Id == request.PlanId, cancellationToken);

        if (existing is null)
            return null;

        mapper.Map(request.PlanDto, existing);

        await unitOfWork.Repository<Plan>().UpdateAsync(existing);
        await unitOfWork.CompleteAsync(cancellationToken);

        return existing; // Devuelve la entidad actualizada
    }
}