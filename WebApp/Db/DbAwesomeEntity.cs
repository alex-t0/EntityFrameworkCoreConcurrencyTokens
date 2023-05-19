using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Db;

[Table("AwesomeEntity")]
public class DbAwesomeEntity
{
    [Key]
    public Guid Uid { get; set; }
    
    public string Name { get; set; }
    
    [Timestamp]
    public uint Timestamp { get; set; }
}