using ArcheryAcademy.Application.DTOs.UserPlanDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.UserPlanUseCases.Commands;

public record CreateUserPlanCommand(UserPlanCreateDto UserPlanDto) : IRequest<UserPlan>;

internal sealed class CreateUserPlanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateUserPlanCommand, UserPlan>
{
    public async Task<UserPlan> Handle(CreateUserPlanCommand request, CancellationToken cancellationToken)
    {
        var userPlan = mapper.Map<UserPlan>(request.UserPlanDto);
        await unitOfWork.Repository<UserPlan>().Insert(userPlan);
        await unitOfWork.CompleteAsync(cancellationToken);
        return userPlan;
    }
}