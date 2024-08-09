using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using JobsCandidateRecords.Models.DTO;
using JobsCandidateRecords.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers.DTO
{
    /// <summary>
    /// Controller for managing EmployeeDTO operations.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EmployeeDTOController"/> class.
    /// </remarks>
    /// <param name="employeeService">The employee service.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDTOController(IEmployeeService employeeService) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeService;

        /// <summary>
        /// Gets the list of employees.
        /// </summary>
        /// <returns>A list of EmployeeDTO objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var employeeDTOs = await _employeeService.GetAllEmployeesAsync();
            return Ok(employeeDTOs);
        }

        /// <summary>
        /// Gets an employee by id.
        /// </summary>
        /// <param name="id">The employee id.</param>
        /// <returns>An EmployeeDTO object.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            var employeeDTO = await _employeeService.GetEmployeeByIdAsync(id);
            if (employeeDTO == null)
            {
                return NotFound();
            }
            return Ok(employeeDTO);
        }

        /// <summary>
        /// Updates an employee.
        /// </summary>
        /// <param name="employee">The employee object to update.</param>
        /// <returns>No content if update is successful.</returns>
        [HttpPut]
        public async Task<IActionResult> PutEmployee(Employee employee)
        {
            if (!await _employeeService.UpdateEmployeeAsync(employee))
            {
                return NotFound();
            }
            return Ok();
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="employee">The employee object to create.</param>
        /// <returns>The created EmployeeDTO object.</returns>
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeDTO = await _employeeService.CreateEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = employeeDTO.Id }, employeeDTO);
        }

        /// <summary>
        /// Deletes an employee by id.
        /// </summary>
        /// <param name="id">The employee id.</param>
        /// <returns>No content if delete is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (!await _employeeService.DeleteEmployeeAsync(id))
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
