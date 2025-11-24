using ArcheryAcademy.Application.DTOs.UserPlanDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.UserPlanUseCases.Queries;

public record GetUserPlanByIdQuery(Guid Id) : IRequest<UserPlanReadDto?>;

internal sealed class GetUserPlanByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetUserPlanByIdQuery, UserPlanReadDto?>
{
    public async Task<UserPlanReadDto?> Handle(GetUserPlanByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<UserPlan>().FirstOrDefaultAsync(
            x => x.Id == request.Id, cancellationToken
        );

        if (entity == null)
            return null;

        return mapper.Map<UserPlanReadDto>(entity);
    }
}