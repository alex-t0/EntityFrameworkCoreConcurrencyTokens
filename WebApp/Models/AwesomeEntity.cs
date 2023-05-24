using WebApp.Db;

namespace WebApp.Models;

public class AwesomeEntity
{
    public Guid Uid { get; set; }
    
    public string Name { get; set; }
    
    public string Timestamp { get; set; }

    public DbAwesomeEntity MapTo(DbAwesomeEntity to)
    {
        to.Uid = Uid;
        to.Name = Name;
        // to.Timestamp = Timestamp == null ? null : Convert.FromBase64String(Timestamp);
        to.Timestamp = uint.Parse(Timestamp);
        // to.Xmin = Xmin;
        
        return to;
    }

    public AwesomeEntity MapFrom(DbAwesomeEntity from)
    {
        Uid = from.Uid;
        Name = from.Name;
        // Timestamp = from.Timestamp == null ? null : Convert.ToBase64String((byte[])from.Timestamp);
        Timestamp = from.Timestamp.ToString();
        // Xmin = from.Xmin;

        return this;
    }
}