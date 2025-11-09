using System.ComponentModel.DataAnnotations;
using ArcheryAcademy.Domain.Enums;

namespace ArcheryAcademy.Application.Dtos.PaymentDto;

public class PaymentCreateDto
{
    public int BookingId { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public PaymentMethod Method { get; set; }
}