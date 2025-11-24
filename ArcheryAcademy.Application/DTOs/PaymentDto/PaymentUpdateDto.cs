namespace ArcheryAcademy.Application.Dtos.PaymentDto;

public class PaymentUpdateDto
{
    public decimal? Amount { get; set; }
    public int? StatusId { get; set; }
    public int? MethodId { get; set; }
}