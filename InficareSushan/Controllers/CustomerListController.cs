using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InficareSushan.Models;
using InficareSushan.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InficareSushan.Controllers
{
 [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerListController : ControllerBase
    {
        private readonly ICustomerListRepository customerListRepository;

        public CustomerListController(ICustomerListRepository customerListRepository)
        {
            this.customerListRepository = customerListRepository;
        }

        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<CustomerList>>> GetCustomerList()
        {
            try
            {
                return (await customerListRepository.GetCustomerList()).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
