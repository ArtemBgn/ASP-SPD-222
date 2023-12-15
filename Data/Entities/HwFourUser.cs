namespace ASP_SPD_222.Data.Entities
{
    public class HwFourUser
    {
        public Guid Id { get; set; }
        public String Login { get; set; } = null;
        public String? LastName { get; set; }
        public String? FirstName { get; set; }
        public String? FatherName { get; set; }
        public String Email { get; set; } = null;
        public String PasswordSalt { get; set; } = null;
        public String PasswordDk { get; set; } = null;   // Derived key (RFC 2898)
        public String? Avatar { get; set; }              // filename/URL
        public DateTime RegisterDt { get; set; }
        public DateTime? DeleteDt { get; set; }
    }
}
