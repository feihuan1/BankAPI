using BankAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace BankAPI.Controllers
{
    public class BankController : Controller
    {

        private readonly Account account = new Account { AccountNumber = 1001, AccountHolderName = "Eric", CurrentBalance = 5000 };

        [Route("/")]
        public IActionResult Index()
        {
            return Ok("Welcome to the Best Bank");
        }

        [Route("account-details")]
        public IActionResult GetAccountInfo()
        {
            return Json(account);
        }

        [Route("statement")]
        public IActionResult GetStatemant()
        {
            return File("/sample.pdf", "application/pdf");
        }

        [Route("get-current-banlance/{accountNumber:int}")]
        public IActionResult GetBanlance()
        {
            if (Request.RouteValues["accountNumber"] is string accountNumberValue)
            {
                if(int.TryParse(accountNumberValue, out int accountNumber))
                {
                    if(accountNumber == account.AccountNumber)
                    {
                        return Ok(account.CurrentBalance);
                    }
                    else
                    {
                        return NotFound("account does not exist");
                    }
                }
                else
                {
                    return BadRequest("account number has to be a number");
                }
            }
            else
            {
                return NotFound("Account Number should be supplied");
            }
        }

    }
}
