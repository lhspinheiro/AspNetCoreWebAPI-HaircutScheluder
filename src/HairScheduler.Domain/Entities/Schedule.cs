using HairScheduler.Communication.Enums;

namespace HairScheduler.Domain.Entities;

public class Schedule
{   
    public long Id { get; set; }    
    public string Name { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public HaircutCategory haircutCategory { get; set; }
    public string? HaircutDescription { get; set; } = String.Empty;
    public DateTime Date { get; set; }
    public PaymentType paymentType { get; set; }

}
