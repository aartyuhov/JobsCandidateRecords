using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models.DTO;
using JobsCandidateRecords.Models;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Services
{
    /// <summary>
    /// Service for managing Employee operations.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EmployeeService"/> class.
    /// </remarks>
    /// <param name="context">The application database context.</param>
    public class EmployeeService(ApplicationDbContext context) : IEmployeeService
    {
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>A list of EmployeeDTO objects.</returns>
        public async Task<List<EmployeeDTO>> GetAllEmployeesAsync()
        {
            var employees = await _context.Employees
                .Include(e => e.Position)
                .ToListAsync();

            return employees.Select(e => EmployeeToDTO(e)).ToList();
        }

        /// <summary>
        /// Retrieves an employee by id.
        /// </summary>
        /// <param name="id">The employee id.</param>
        /// <returns>An EmployeeDTO object, or null if the employee is not found.</returns>
        public async Task<EmployeeDTO?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.Id == id);

            return employee == null ? null : EmployeeToDTO(employee);
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="employee">The employee object to create.</param>
        /// <returns>The created EmployeeDTO object.</returns>
        public async Task<EmployeeDTO> CreateEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return EmployeeToDTO(employee);
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="employee">The employee object to update.</param>
        /// <returns>True if the update is successful, otherwise false.</returns>
        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            if (!_context.Employees.Any(e => e.Id == employee.Id))
            {
                return false;
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.Id == employee.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Deletes an employee by id.
        /// </summary>
        /// <param name="id">The employee id.</param>
        /// <returns>True if the deletion is successful, otherwise false.</returns>
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Converts an Employee object to an EmployeeDTO object.
        /// </summary>
        /// <param name="employee">The employee object.</param>
        /// <returns>An EmployeeDTO object.</returns>
        private static EmployeeDTO EmployeeToDTO(Employee employee)
        {
            return new EmployeeDTO(
                employee.Id,
                employee.FirstName,
                employee.LastName,
                employee.DateOfBirth,
                employee.Gender,
                employee.Email,
                employee.PhoneNumber,
                employee.Address,
                employee.HireDate,
                employee.Position?.Title,
                employee.AvatarUrl
            );
        }
    }
}
