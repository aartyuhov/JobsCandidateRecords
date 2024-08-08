namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// Data Transfer Object for an Employee.
    /// </summary>
    public record EmployeeDTO
    {
        /// <summary>
        /// Gets the employee ID.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Gets the first name of the employee.
        /// </summary>
        public string FirstName { get; init; }

        /// <summary>
        /// Gets the last name of the employee.
        /// </summary>
        public string LastName { get; init; }

        /// <summary>
        /// Gets the date of birth of the employee.
        /// </summary>
        public DateOnly? DateOfBirth { get; init; }

        /// <summary>
        /// Gets the gender of the employee.
        /// </summary>
        public string Gender { get; init; }

        /// <summary>
        /// Gets the email address of the employee.
        /// </summary>
        public string Email { get; init; }

        /// <summary>
        /// Gets the phone number of the employee.
        /// </summary>
        public string PhoneNumber { get; init; }

        /// <summary>
        /// Gets the address of the employee.
        /// </summary>
        public string Address { get; init; }

        /// <summary>
        /// Gets the hire date of the employee.
        /// </summary>
        public DateTime HireDate { get; init; }

        /// <summary>
        /// Gets the id of the position held by the employee.
        /// </summary>
        public int PositionId { get; init; }
        /// <summary>
        /// Gets the name of the position held by the employee.
        /// </summary>
        public string? PositionName { get; init; }

        /// <summary>
        /// Gets the URL of the employee's avatar.
        /// </summary>
        public string? AvatarUrl { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeDTO"/> record.
        /// </summary>
        public EmployeeDTO(
            int id,
            string firstName,
            string lastName,
            DateOnly? dateOfBirth,
            string gender,
            string email,
            string phoneNumber,
            string address,
            DateTime hireDate,
            int positionId,
            string? positionName,
            string? avatarUrl)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            HireDate = hireDate;
            PositionId = positionId;
            PositionName = positionName;
            AvatarUrl = avatarUrl;
        }
    }
}
