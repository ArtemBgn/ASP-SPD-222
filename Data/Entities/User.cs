namespace ASP_SPD_222.Data.Entities
{
    public class User
    {
        public Guid     Id              { get; set; }
        public String   Login           { get; set; } = null;
        public String?  Name            { get; set; }
        public String   Email           { get; set; } = null;
        public String   PasswordSalt    { get; set; }
        public String   PasswordDk      { get; set; }           // Derived key (RFC 2898)
        public String?  Avatar          { get; set; } = null;   // filename/URL
        public DateTime RegisterDt      { get; set; }
        public DateTime? DeletDt        { get; set; } = null;
    }
}
