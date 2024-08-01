namespace JobsCandidateRecords.Models.DTO
{

    public record DepartmentDTO
    (
    int Id,
    string Name,
    string? Description,
    int CompanyId,
    string? CompanyName
    );
}