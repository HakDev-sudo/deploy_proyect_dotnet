using ArcheryAcademy.Application.Dtos.PaymentDto;
using ArcheryAcademy.Domain.Ports;
using ArcheryAcademy.Domain.Entities;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.PaymentUseCases.Command;

public record CreatePaymentCommand(PaymentCreateDto PaymentDto) : IRequest<Payment>;

internal sealed class CreatePaymentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreatePaymentCommand, Payment>
{
    public async Task<Payment> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = mapper.Map<Payment>(request.PaymentDto);
        await unitOfWork.Repository<Payment>().Insert(payment);
        await unitOfWork.CompleteAsync(cancellationToken);
        return payment; 
    }
}