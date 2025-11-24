using ArcheryAcademy.Domain.Entities;
using ArcheryAcademy.Domain.Ports;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.PaymentUseCases.Command;

public record DeletePaymentCommand(Guid Id) : IRequest<bool>;

internal sealed class DeletePaymentCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeletePaymentCommand, bool>
{
    public async Task<bool> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<Payment>();
        var payment = await repository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (payment == null)
            return false;

        await repository.DeleteAsync(payment, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }
}