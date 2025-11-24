using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.DTOs.UserPlanDto;

public record UpdateUserPlanCommand(Guid UserPlanId, UserPlanUpdateDto UserPlanDto) : IRequest<UserPlan?>;

internal sealed class UpdateUserPlanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateUserPlanCommand, UserPlan?>
{
    public async Task<UserPlan?> Handle(UpdateUserPlanCommand request, CancellationToken cancellationToken)
    {
        // Buscar la entidad existente
        var existing = await unitOfWork.Repository<UserPlan>().FirstOrDefaultAsync(x => x.Id == request.UserPlanId, cancellationToken);

        if (existing is null)
            return null;

        // Mapear los nuevos valores sobre la entidad existente
        mapper.Map(request.UserPlanDto, existing);

        // Actualizar en base de datos
        await unitOfWork.Repository<UserPlan>().UpdateAsync(existing);
        await unitOfWork.CompleteAsync(cancellationToken);

        return existing;
    }
}