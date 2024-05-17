using System.ComponentModel;

namespace Lara.Domain.Entities;

public enum BorrowedBookStatus
{
    [Description("Aguardando devolução")]
    AWAITING_RETURN,
    
    [Description("Atraso na devolução")]
    DELAY_IN_RETURN,
    
    [Description("Devolução realizada")]
    RETURN_MADE
}

public class BorrowedBook : BaseTransaction
{
    public const int RETURN_DATE_DAYS = 7;
    public BorrowedBookStatus Status { get; set; } = BorrowedBookStatus.AWAITING_RETURN;
    
    public DateTime ReturnDate { get; set; } = DateTime.UtcNow.AddDays(RETURN_DATE_DAYS);
}