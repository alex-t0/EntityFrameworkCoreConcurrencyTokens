using WebApp.Db;

namespace WebApp.Models;

public class AwesomeEntity
{
    public Guid Uid { get; set; }
    
    public string Name { get; set; }
    
    public uint Xmin { get; set; }
    
    public string Timestamp { get; set; }

    public DbAwesomeEntity MapTo(DbAwesomeEntity to)
    {
        to.Uid = Uid;
        to.Name = Name;
        to.Timestamp = Timestamp == null ? null : Convert.FromBase64String(Timestamp);
        to.Xmin = Xmin;
        
        return to;
    }

    public AwesomeEntity MapFrom(DbAwesomeEntity from)
    {
        Uid = from.Uid;
        Name = from.Name;
        Timestamp = from.Timestamp == null ? null : Convert.ToBase64String(from.Timestamp);
        Xmin = from.Xmin;

        return this;
    }
}