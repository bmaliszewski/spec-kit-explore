namespace eFitnessKiosk.Models;

public class Payment
{
    public string Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; }
    public string PaymentMethod { get; set; }
    public string PassId { get; set; }
}
