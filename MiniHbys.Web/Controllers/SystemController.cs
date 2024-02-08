using Microsoft.AspNetCore.Mvc;
using MiniHbys.Business.Abstraction;
using MiniHbys.Web.Models;

namespace MiniHbys.Web.Controllers;

public class SystemController : Controller
{
    private readonly ISystemService _systemService;  

    public SystemController(ISystemService systemService)
    {
        _systemService = systemService;   
    }
    
    // GET
    public IActionResult CreateBackup()
    {
        return View();
    }

    public IActionResult RestoreDatabase()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CreateBackup(BackupViewModel model)
    {
        _systemService.BackupDatabase(model.Path,model.FileName);
        return RedirectToAction(controllerName:"Home",actionName:"Index");
    }

    [HttpPost]
    public IActionResult RestoreDatabase(RestoreViewModel model)
    {
        _systemService.RestoreDatabase(model.FullPath);
        return RedirectToAction(controllerName:"Home",actionName:"Index");
    }
}