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
            // _db.Entry(dbEntity).Property(p => p.Timestamp.Timestamp).OriginalValue = dbEntity.Timestamp.Timestamp;
            _db.Entry(dbEntity).Reference(r => r.Timestamp).TargetEntry.Property(z => z.Timestamp).OriginalValue =
                dbEntity.Timestamp.Timestamp;
        }
        else if (_db.Database.IsNpgsql())
        {
            // _db.Entry(dbEntity).Property(p => p.Timestamp.Xmin).OriginalValue = dbEntity.Timestamp.Xmin;
            _db.Entry(dbEntity).Reference(r => r.Timestamp).TargetEntry.Property(z => z.Xmin).OriginalValue =
                dbEntity.Timestamp.Xmin;
        }
        
        _db.SaveChanges();
        return Redirect("/Awesome/");
    }
}