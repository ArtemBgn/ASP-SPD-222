namespace ASP_SPD_222.Data.Entities
{
    public class LoginJournalItem
    {
        /// <summary>
        /// Інформація про входи у систему
        /// </summary>
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Moment { get; set; }
    }
}
