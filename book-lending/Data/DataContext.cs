namespace book_lending.Data
{
    public class DataContext : DbContext
    {
        
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookOwnership> BookOwnerships { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleOperation> RoleOperations { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                @"Host=localhost;Port=5432;Database=BookLending;Username=postgres;Password=postgres"); 
        }
    }
}