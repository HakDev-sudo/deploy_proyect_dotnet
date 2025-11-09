using ArcheryAcademy.Application.Dtos.PaymentDto;
using ArcheryAcademy.Domain.Ports;
using ArcheryAcademy.Infrastructure.Persistence.Models;
using AutoMapper;
using MediatR;

namespace ArcheryAcademy.Application.UseCases.PaymentUseCases.Queries;

public record GetAllPaymentsQuery() : IRequest<List<PaymentReadDto>>;

internal sealed class GetAllPaymentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllPaymentsQuery, List<PaymentReadDto>>
{
    public async Task<List<PaymentReadDto>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
    {
        var payments = await unitOfWork.Repository<Payment>().GetAllAsync();
        
        // Mapea la lista de Entidades a la lista de DTOs para la capa externa
        return mapper.Map<List<PaymentReadDto>>(payments);
    }
}