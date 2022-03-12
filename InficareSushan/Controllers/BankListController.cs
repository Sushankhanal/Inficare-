using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InficareSushan.Models;
using InficareSushan.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InficareSushan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankListController : ControllerBase
    {
        private readonly IBankList bankListRepository;

        public BankListController(IBankList bankListRepository)
        {
            this.bankListRepository = bankListRepository;
        }

        [HttpGet]
        public async Task< ActionResult<IEnumerable<BankList>>> GetBankList()
        {
            try
            {
                return (await bankListRepository.GetBankList()).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}