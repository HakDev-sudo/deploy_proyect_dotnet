using ArcheryAcademy.Application.Dtos.PaymentDto;
using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.PaymentUseCases.Command;

public record UpdatePaymentCommand(Guid PaymentId, PaymentUpdateDto PaymentDto) : IRequest<Payment?>;

internal sealed class UpdatePaymentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdatePaymentCommand, Payment?>
{
    public async Task<Payment?> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var existing = await unitOfWork.Repository<Payment>().FirstOrDefaultAsync(x => x.Id == request.PaymentId, cancellationToken);

        if (existing is null)
            return null;

        mapper.Map(request.PaymentDto, existing);

        await unitOfWork.Repository<Payment>().UpdateAsync(existing);
        await unitOfWork.CompleteAsync(cancellationToken);

        return existing; 
    }
}