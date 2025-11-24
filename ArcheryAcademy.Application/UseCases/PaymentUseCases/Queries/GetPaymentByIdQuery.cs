using ArcheryAcademy.Application.Dtos.PaymentDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.PaymentUseCase.Queries;

public record GetPaymentByIdQuery(Guid Id) : IRequest<PaymentReadDto?>;

internal sealed class GetPaymentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetPaymentByIdQuery, PaymentReadDto?>
{
    public async Task<PaymentReadDto?> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<Payment>().FirstOrDefaultAsync(
            x => x.Id == request.Id, cancellationToken
        );

        if (entity == null)
            return null;

        return mapper.Map<PaymentReadDto>(entity);
    }
}