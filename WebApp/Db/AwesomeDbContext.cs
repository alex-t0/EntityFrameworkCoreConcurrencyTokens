using Microsoft.EntityFrameworkCore;

namespace WebApp.Db;

public class AwesomeDbContext : DbContext
{
    public AwesomeDbContext(DbContextOptions<AwesomeDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<DbAwesomeEntity> AwesomeEntities { get; set; }

    private byte[] ConvertUintToByteArray(uint src)
    {
        var len = sizeof(uint);
        byte[] result = new byte[len];

        for (int i = 0; i < len; i++)
        {
            result[i] = Convert.ToByte(src & 255);
            src >>= 8;
        }
        
        return result;
    }

    private uint ConvertByteArrayToUint(byte[] bytes)
    {
        uint result = 0;

        for (int i = bytes.Length - 1; i >=0; i--)
        {
            result <<= 8;
            result |= bytes[i];
        }

        return result;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        if (Database.IsSqlServer())
        {
            // modelBuilder.Entity<DbAwesomeEntity>().Ignore(b => b.Xmin);
            modelBuilder.Entity<DbAwesomeEntity>().Property(x => x.Timestamp).HasColumnName("Timestamp").HasColumnType("Timestamp");
        }
        else if (Database.IsNpgsql())
        {
            // modelBuilder.Entity<DbAwesomeEntity>().Ignore(b => b.Timestamp);
            modelBuilder.Entity<DbAwesomeEntity>().Property(x => x.Timestamp)
                .HasColumnName("xmin").HasColumnType("xid")
                .HasConversion(to => ConvertByteArrayToUint(to), from => ConvertUintToByteArray(from));
        }
    }
}