namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// DTO for company request.
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    public record CompanyDTO(
        int Id,
        string Name,
        string? Description
    );
}