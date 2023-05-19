using Microsoft.AspNetCore.Mvc;
using WebApp.Db;
using WebApp.Models;

namespace WebApp.Controllers;

public class AwesomeController : Controller
{
    private readonly AwesomeDbContext _db;

    public AwesomeController(AwesomeDbContext db)
    {
        _db = db;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Insert(AwesomeEntity entity)
    {
        if (entity.Uid == Guid.Empty) entity.Uid = Guid.NewGuid();

        var dbEntity = new DbAwesomeEntity();
        entity.MapTo(dbEntity);

        _db.AwesomeEntities.Add(dbEntity);
        _db.SaveChanges();
        
        return Redirect("/Awesome/");
    }
    
    public IActionResult Update()
    {
        return View("Create");
    }
    
    [HttpPost]
    public IActionResult Save(AwesomeEntity entity)
    {
        return Redirect("/Awesome/");
    }
}