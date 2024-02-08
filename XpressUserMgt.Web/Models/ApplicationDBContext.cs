using Microsoft.EntityFrameworkCore;

namespace XpressUserMgt.Web.Models
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public ApplicationDBContext()
        {

        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<GroupItem> GroupItems { get; set; }
        public DbSet<GroupItemAllRecord> GroupItemAllRecords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //OnModelCreating
            modelBuilder.Entity<GroupItem>().HasNoKey();
            modelBuilder.Entity<GroupItemAllRecord>().HasNoKey();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: false);
                var configuration = builder.Build();
                var connectionString = configuration.GetConnectionString("Conn");
                //string FinalMainConnStrSQL = configuration.getSQLCONN(connectionString);
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
