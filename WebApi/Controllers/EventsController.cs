using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("/event")]
public class EventsController : BaseController
{
    private readonly IEventService _eventService;
    
    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpPost]
    public async Task<IResult> DepositIntoBankAccount([FromBody] DTOBankAccountRequest request)
    {
        try
        {
            if (request.Type == EventTypeEnum.deposit)
            {
                BankAccount bankAccount =
                    await _eventService.DepositIntoBankAccountById(request.Destination, request.Amount);
                var dtoResponse = new DTOBankAccountResponse(bankAccount, null);

                return Results.Created("", dtoResponse);
            }
            else if (request.Type == EventTypeEnum.withdraw)
            {
                BankAccount bankAccount =
                    await _eventService.WithdrawFromBankAccountById(request.Origin, request.Amount);
                
                var dtoResponse = new DTOBankAccountResponse(null, bankAccount);
                return Results.Created("", dtoResponse);
            }
            else if (request.Type == EventTypeEnum.transfer)
            {
                (BankAccount origin, BankAccount destination) =
                    await _eventService.TransferFromOriginToDestinationBankAccount(
                        request.Origin,
                        request.Destination,
                        request.Amount);
                
                var dtoResponse = new DTOBankAccountResponse(destination, origin);
                return Results.Created("", dtoResponse);
            }
        }
        catch (BankAccountNotFoundException e)
        {
            return Results.NotFound(0);
        }

        return Results.BadRequest();
    }
}