namespace Practical17.Models
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public User Users { get; set; }
        public Role Roles { get; set; }
    }
}
