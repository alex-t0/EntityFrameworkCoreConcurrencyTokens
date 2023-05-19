using Microsoft.EntityFrameworkCore;

namespace WebApp.Db;

[Owned]
public class DbModificationData
{
    public DateTime CreationDate { get; set; }
    
    public DateTime ModificationDate { get; set; }
}