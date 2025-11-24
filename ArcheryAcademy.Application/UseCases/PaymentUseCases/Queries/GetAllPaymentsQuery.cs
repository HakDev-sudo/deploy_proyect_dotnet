using ArcheryAcademy.Application.Dtos.PaymentDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.PaymentUseCase.Queries;

public record GetAllPaymentsQuery() : IRequest<List<PaymentReadDto>>;

internal sealed class GetAllPaymentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllPaymentsQuery, List<PaymentReadDto>>
{
    public async Task<List<PaymentReadDto>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
    {
        var payments = await unitOfWork.Repository<Payment>().GetAllAsync();
        return mapper.Map<List<PaymentReadDto>>(payments);
    }
}