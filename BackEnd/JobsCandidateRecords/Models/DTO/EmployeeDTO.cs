namespace JobsCandidateRecords.Models.DTO
{
    public record EmployeeDTO(
        int Id,
        string FirstName,
        string LastName,
        DateOnly? DateOfBirth,
        string Gender,
        string Email,
        string PhoneNumber,
        string Address,
        DateTime HireDate,
        string? PositionName,
        string? AvatarUrl
    );
}
