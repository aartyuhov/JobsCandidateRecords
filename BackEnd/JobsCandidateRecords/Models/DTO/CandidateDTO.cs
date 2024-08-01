namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// DTO for candidate information.
    /// </summary>
    public record CandidateDTO
    {
        /// <summary>
        /// Candidate identifier.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Candidate's first name.
        /// </summary>
        public string FirstName { get; init; }

        /// <summary>
        /// Candidate's last name.
        /// </summary>
        public string LastName { get; init; }

        /// <summary>
        /// Candidate's date of birth.
        /// </summary>
        public DateOnly? DateOfBirth { get; init; }

        /// <summary>
        /// Candidate's gender.
        /// </summary>
        public string Gender { get; init; }

        /// <summary>
        /// Candidate's email address.
        /// </summary>
        public string Email { get; init; }

        /// <summary>
        /// Candidate's phone number.
        /// </summary>
        public string Phone { get; init; }

        /// <summary>
        /// Candidate's address.
        /// </summary>
        public string Address { get; init; }

        /// <summary>
        /// Additional information about the candidate.
        /// </summary>
        public string AboutInfo { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateDTO"/> record.
        /// </summary>
        /// <param name="id">Candidate identifier.</param>
        /// <param name="firstName">Candidate's first name.</param>
        /// <param name="lastName">Candidate's last name.</param>
        /// <param name="dateOfBirth">Candidate's date of birth.</param>
        /// <param name="gender">Candidate's gender.</param>
        /// <param name="email">Candidate's email address.</param>
        /// <param name="phone">Candidate's phone number.</param>
        /// <param name="address">Candidate's address.</param>
        /// <param name="aboutInfo">Additional information about the candidate.</param>
        public CandidateDTO(
            int id,
            string firstName,
            string lastName,
            DateOnly? dateOfBirth,
            string gender,
            string email,
            string phone,
            string address,
            string aboutInfo)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            Phone = phone;
            Address = address;
            AboutInfo = aboutInfo;
        }
    }
}
