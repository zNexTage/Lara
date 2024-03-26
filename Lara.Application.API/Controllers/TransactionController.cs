using Lara.Domain.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Lara.Application.API.Controllers;

public class TransactionController : LaraControllerBase
{
    [HttpPost]
    public IActionResult Purchase(CreateTransactionDto transactionDto)
    {
        
        return Ok();
    }
    
}