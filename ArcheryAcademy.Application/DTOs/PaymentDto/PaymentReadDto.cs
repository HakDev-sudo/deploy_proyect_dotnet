namespace ArcheryAcademy.Application.Dtos.PaymentDto;

public class PaymentReadDto
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public decimal Amount { get; set; }
    public int StatusId { get; set; }
    public int MethodId { get; set; }
    public string? Status { get; set; }
    public string? Method { get; set; }
    public DateTime? CreatedAt { get; set; }
}