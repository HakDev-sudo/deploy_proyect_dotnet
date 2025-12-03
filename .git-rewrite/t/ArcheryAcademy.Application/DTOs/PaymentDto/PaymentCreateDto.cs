using System.ComponentModel.DataAnnotations;

namespace ArcheryAcademy.Application.Dtos.PaymentDto;

public class PaymentCreateDto
{
    public Guid BookingId { get; set; }
    public decimal Amount { get; set; }
    public int StatusId { get; set; }
    public int MethodId { get; set; }
}