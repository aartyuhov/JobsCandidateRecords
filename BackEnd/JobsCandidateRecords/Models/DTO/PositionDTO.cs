using System;

namespace JobsCandidateRecords.Models.DTO
{
    public record PositionDTO
    (
    int Id,
    string Title,
    string? Responsibilities,
    int DepartmentId,
    string? DepartmentName
    );
}