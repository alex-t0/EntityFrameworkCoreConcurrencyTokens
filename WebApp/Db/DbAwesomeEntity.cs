using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Models;

namespace WebApp.Db;

[Table("AwesomeEntity", Schema = "dbo")]
public class DbAwesomeEntity
{
    [Key]
    public Guid Uid { get; set; }
    
    public string Name { get; set; }
    
    public ComplexTimestamp Timestamp { get; set; }
}