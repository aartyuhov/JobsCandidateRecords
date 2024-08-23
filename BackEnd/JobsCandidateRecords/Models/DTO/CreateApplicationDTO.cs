namespace JobsCandidateRecords.Models.DTO
{
    public record CreateApplicationDTO
    {
        public int CandidateId { get; init; }
        public int? EmployeeWhoCreatedId { get; init; }
        public DateTime? CreationDate { get; init; }
        public string Details { get; init; }
    }
}
