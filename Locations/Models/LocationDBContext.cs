using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

public partial class LocationDBContext:DbContext
{
    public LocationDBContext(DbContextOptions<LocationDBContext> options):base(options)
    {
     
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            var UserId = user.Property(p => p.UserId);
            UserId.ValueGeneratedOnAdd();
            // only for in-memory
            if (Database.IsInMemory())
                UserId.HasValueGenerator<InMemoryIntegerValueGenerator<int>>();
        });


        modelBuilder.Entity<LocationHistory>(locationHistory =>
        {
            var RecordID = locationHistory.Property(p => p.RecordID);
            RecordID.ValueGeneratedOnAdd();
            // only for in-memory
            if (Database.IsInMemory())
                RecordID.HasValueGenerator<IDGenerator>();
        });


    }

    public virtual DbSet<User> Users {get;set;}
    public virtual DbSet<LocationHistory> LocationHistory {get;set;}
}