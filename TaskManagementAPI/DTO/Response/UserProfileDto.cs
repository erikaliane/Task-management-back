namespace TaskManagementAPI.DTO.Response
{
    public class UserProfileDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}