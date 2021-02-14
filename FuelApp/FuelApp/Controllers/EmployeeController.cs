using FuelApp.Models;
using FuelApp.QueryHandler;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelApp.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        [Route("api/employees")]
        public IActionResult employeeView()
        {
            EmployeeData obj = new EmployeeData();

            return Ok(obj.employeeView());
        }

        [HttpPost]
        [Route("api/employees")]
        public IActionResult employeeAdd([FromBody] Employee emp)
        {
            EmployeeData obj = new EmployeeData();

            try
            {
                return Ok(obj.employeeAdd(emp));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
