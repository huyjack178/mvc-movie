namespace MvcMovie.Models
{
    public class UserRole
    {
        public enum RoleType
        {
            admin = 1,
            normal = 2
        }

        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }
}