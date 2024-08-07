using JobsCandidateRecords.Models.DTO;
using JobsCandidateRecords.Models;

namespace JobsCandidateRecords.Services
{
    /// <summary>
    /// Defines the contract for employee-related operations within the system.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>A list of EmployeeDTO objects.</returns>
        Task<List<EmployeeDTO>> GetAllEmployeesAsync();

        /// <summary>
        /// Retrieves an employee by id.
        /// </summary>
        /// <param name="id">The employee id.</param>
        /// <returns>An EmployeeDTO object, or null if the employee is not found.</returns>
        Task<EmployeeDTO?> GetEmployeeByIdAsync(int id);

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="employee">The employee object to create.</param>
        /// <returns>The created EmployeeDTO object.</returns>
        Task<EmployeeDTO> CreateEmployeeAsync(Employee employee);

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="employee">The employee object to update.</param>
        /// <returns>True if the update is successful, otherwise false.</returns>
        Task<bool> UpdateEmployeeAsync(Employee employee);

        /// <summary>
        /// Deletes an employee by id.
        /// </summary>
        /// <param name="id">The employee id.</param>
        /// <returns>True if the deletion is successful, otherwise false.</returns>
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
