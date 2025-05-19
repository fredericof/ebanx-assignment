namespace Domain;

public class Event
{
    // Event Type
    public EventTypeEnum Type { get; set; }
    
    // Origin AccountId
    public string Origin { get; set;  }
    
    // Destination AccountId
    public string Destination { get; set; }
    
    // How Much Money
    public decimal Amount { get; set; }
}