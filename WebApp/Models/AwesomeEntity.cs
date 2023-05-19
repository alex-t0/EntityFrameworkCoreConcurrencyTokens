using WebApp.Db;

namespace WebApp.Models;

public class AwesomeEntity
{
    public Guid Uid { get; set; }
    
    public string Name { get; set; }
    
    public uint Timestamp { get; set; }

    public void MapTo(DbAwesomeEntity to)
    {
        to.Uid = Uid;
        to.Name = Name;
        to.Timestamp = Timestamp;
    }

    public void MapFrom(DbAwesomeEntity from)
    {
        Uid = from.Uid;
        Name = from.Name;
        Timestamp = from.Timestamp;
    }
}