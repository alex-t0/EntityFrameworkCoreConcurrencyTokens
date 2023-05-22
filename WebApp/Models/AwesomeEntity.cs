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
        to.Timestamp = new ComplexTimestamp
        {
            Timestamp = Timestamp == null ? null : Convert.FromBase64String(Timestamp),
            Xmin = Xmin
        };

        return to;
    }

    public AwesomeEntity MapFrom(DbAwesomeEntity from)
    {
        Uid = from.Uid;
        Name = from.Name;

        if (from.Timestamp != null)
        {
            Timestamp = from.Timestamp.Timestamp == null ? null : Convert.ToBase64String(from.Timestamp.Timestamp);
            Xmin = from.Timestamp.Xmin;
        }

        return this;
    }
}