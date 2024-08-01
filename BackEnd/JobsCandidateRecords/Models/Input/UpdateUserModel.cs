using System.ComponentModel.DataAnnotations;

namespace JobsCandidateRecords.Models.Input
{
    public class UpdateUserModel
    {
        public string Email { get; set; }
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }
    }
}
