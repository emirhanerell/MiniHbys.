using Microsoft.AspNetCore.Mvc;
using MiniHbys.Business.Abstraction;
using MiniHbys.Entity;

namespace MiniHbys.Web.Controllers;

public class DepartmentController : Controller
{
    private readonly IDepartmentService _departmentService;
    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }
    public IActionResult Index()
    {
        var departments = _departmentService.GetAllDepartments();
        return View(departments);  
    }

    public IActionResult Detail(int id)  
    {
        var department = _departmentService.GetDepartmentById(id);
        return View(department);
    }

    public IActionResult Edit(int id)
    {
        var department = _departmentService.GetDepartmentById(id);  
        return View(department);
    }

    [HttpPost]
    public IActionResult Edit(Department department)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            ViewBag.Message = "Please fill all fields";
            return View();          
        }
        _departmentService.UpdateDepartment(department);
        return RedirectToAction(actionName: "Index", controllerName: "Department");  
    }

    public IActionResult Create()
    {
        return View();        
    }

    [HttpPost]
    public IActionResult Create(Department department)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            ViewBag.Message = "Please fill all fields";
            return View();
        }
        _departmentService.CreateDepartment(department);
        return RedirectToAction(actionName: "Index", controllerName: "Department");
    }

    public IActionResult Delete(int id)
    {
        _departmentService.DeleteDepartment(id);
        return RedirectToAction(actionName: "Index", controllerName: "Department");
    }
}