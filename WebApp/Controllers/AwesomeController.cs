using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        var model = _db.AwesomeEntities.ToList().Select(x => new AwesomeEntity().MapFrom(x)).ToArray();
        return View(model);
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
    
    public IActionResult Update(Guid uid)
    {
        var dbEntity = _db.Find<DbAwesomeEntity>(uid);
        var entity = new AwesomeEntity();
        entity.MapFrom(dbEntity);
        return View("Update", entity);
    }
    
    [HttpPost]
    public IActionResult Save(AwesomeEntity entity)
    {
        var dbEntity = _db.Find<DbAwesomeEntity>(entity.Uid);
        entity.MapTo(dbEntity);

        if (_db.Database.IsSqlServer())
        {
            _db.Entry(dbEntity).Property(p => p.Timestamp).OriginalValue = dbEntity.Timestamp;            
        }
        else if (_db.Database.IsNpgsql())
        {
            _db.Entry(dbEntity).Property(p => p.Xmin).OriginalValue = dbEntity.Xmin;    
        }
        
        _db.SaveChanges();
        return Redirect("/Awesome/");
    }
}