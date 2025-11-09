using ArcheryAcademy.Domain.Enums;

namespace ArcheryAcademy.Application.Dtos.PaymentDto;

public class PaymentUpdateDto
{
    public decimal? Amount { get; set; }
    public PaymentStatus? Status { get; set; }
    public PaymentMethod? Method { get; set; }
}