using FuelApp.Models;
using FuelApp.QueryHandler;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelApp.Controllers
{
    public class FuelTypeController : ControllerBase
    {
        [HttpGet]
        [Route("api/fuel")]
        public IActionResult fuelView()
        {
            FuelTypeData obj = new FuelTypeData();

            return Ok(obj.fuelView());
        }

        [HttpPost]
        [Route("api/fuel")]
        public IActionResult fuelAdd([FromBody] FuelType fuel)
        {
            FuelTypeData obj = new FuelTypeData();

            try
            {
                return Ok(obj.fuelAdd(fuel));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
