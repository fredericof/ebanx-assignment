using System.Text.Json.Serialization;
using Domain;

namespace Application;

public class DTOBankAccountResponse
{
    // Origin Bank Account
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BankAccount? Origin { get; set; }
    
    // Destinatio Bank Account
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BankAccount? Destination { get; set; }

    public DTOBankAccountResponse(BankAccount? destination, BankAccount? origin)
    {
        Destination = destination;
        Origin = origin;
    }
}