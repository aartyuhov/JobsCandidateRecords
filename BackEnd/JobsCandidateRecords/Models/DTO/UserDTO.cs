namespace JobsCandidateRecords.Models.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
