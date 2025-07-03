namespace Identity.Application.DTOs.User
{
    public class UserGetDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
