using ArcheryAcademy.Domain.Enums;

namespace ArcheryAcademy.Application.Dtos.PaymentDto;

public class PaymentReadDto
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } // Mapeado de PaymentStatus (ej. "Completed")
    public string Method { get; set; } // Mapeado de PaymentMethod (ej. "CreditCard")
    public DateTime? CreatedAt { get; set; }
}