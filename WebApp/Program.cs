using Microsoft.EntityFrameworkCore;
using WebApp.Configuration;
using WebApp.Constants;
using WebApp.Db;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AwesomeDbContext>(options =>
{
    var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .Build();
    var dbConf = new DbConfiguration();
    config.GetSection("DbConfiguration").Bind(dbConf);

    var connectionConfiguration = dbConf.ConnectionStrings.Single(x => x.Name == dbConf.ActiveConnectionString);
    if (connectionConfiguration.Name == ConnectionStringConstants.MsSql)
    {
        options.UseSqlServer(connectionConfiguration.Url);
    }
    else if (dbConf.ActiveConnectionString == ConnectionStringConstants.Postgres)
    {
        options.UseNpgsql(connectionConfiguration.Url);
    }
    else
    {
        throw new InvalidOperationException("MS SQL Server and Postgres are supported only");
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
