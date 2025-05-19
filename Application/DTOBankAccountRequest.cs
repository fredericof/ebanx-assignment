using Domain;

namespace Application;

public class DTOBankAccountRequest
{
    // Event Type
    public EventTypeEnum Type { get; set; }
    
    // Origin Bank Account
    public string? Origin { get; set; }
    
    // Destinatio Bank Account
    public string? Destination { get; set; }
    
    // Money amount
    public decimal Amount { get; set; }
}