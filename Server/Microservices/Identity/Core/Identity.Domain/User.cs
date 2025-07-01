using Microsoft.AspNetCore.Identity;

namespace Identity.Domain
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
