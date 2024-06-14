public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Points { get; set; }
}

public class LoyaltyContext : DbContext
{
    public LoyaltyContext(DbContextOptions<LoyaltyContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}

