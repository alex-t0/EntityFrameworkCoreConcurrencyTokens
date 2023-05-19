namespace WebApp.Configuration;

public class DbConfiguration
{
    public string ActiveConnectionString { get; set; }
    
    public IEnumerable<ConnectionString> ConnectionStrings { get; set; }
}