using CoreCrudApi.Db.Models;
using CoreCrudApi.Exceptions;
using CoreCrudApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreCrudApi.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "GeneralUser")]
    [ApiController]    
    public class EmployeeController : ControllerBase
    {
        public readonly IEmployeeService _employeeService1;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService1 = employeeService;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult( await _employeeService1.Get());
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employees =await _employeeService1.Get(id);
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult Post([FromBody] Employee value)
        {
            if (value == null)
            {
                return BadRequest("Invalid data");
            }

           _employeeService1.Post(value);

            return CreatedAtAction(nameof(Get), new { id = value.EmployeeId }, value);

        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee value)
        {
            try
            {

                return Ok(_employeeService1.Put(id, value));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _employeeService1.Delete(id);

                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }            
        }
    }
}
