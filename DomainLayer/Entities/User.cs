namespace DomainLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; } 
        public string PasswordHash { get; set; } 
    }
}
