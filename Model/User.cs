namespace Model
{
    public class User
    {
        public int UserId { get; set; }
      public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Role { get; set; } = "User"; 
        public bool IsActive { get; set; } = true;
      public DateTime? CreatedDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
    }
}
