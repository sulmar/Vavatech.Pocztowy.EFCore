using Microsoft.AspNetCore.Mvc;
using Pocztowy.Shop.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pocztowy.Shop.Service.Controllers
{
    [Route("api/[controller]")]
    public class CuctomersController : ControllerBase
    {
        private readonly ICustomersService customersService;

        public IActionResult Get()
        {
            return Ok(customersService.Get());
        }
    }
}
