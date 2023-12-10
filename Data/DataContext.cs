using Microsoft.EntityFrameworkCore;

namespace ASP_SPD_222.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Entities.User> Users { get; set; }
        public DbSet<Entities.LoginJournalItem> LoginJournal { get; set; }
        public DbSet<Entities.MyFormDataBase> MyFormDataBases { get; set; }
        public DataContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("ASP_SPD_222");
        }
    }
}
