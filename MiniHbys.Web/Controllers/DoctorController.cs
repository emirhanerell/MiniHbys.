using Microsoft.AspNetCore.Mvc;
using MiniHbys.Business.Abstraction;
using MiniHbys.Entity;

namespace MiniHbys.Web.Controllers;

public class DoctorController : Controller  
{
    private readonly IDoctorService _doctorService;    
    private readonly IDepartmentService _departmentService;

    public DoctorController(IDoctorService doctorService, IDepartmentService departmentService)
    {
        _doctorService = doctorService; 
        _departmentService = departmentService;  
    }
    // GET
    public IActionResult Index()
    {
        var doctors = _doctorService.GetAllDoctors();
        return View(doctors);  
    }

    public IActionResult Detail(int id)
    {
        var doctor = _doctorService.GetDoctorById(id);
        return View(doctor); 
    }

    public IActionResult Edit(int id)
    {
        var departmentList = _departmentService.GetAllDepartments();
        var doctor = _doctorService.GetDoctorById(id);
        ViewBag.Departments = departmentList;   
        return View(doctor);
    }

    [HttpPost]
    public IActionResult Edit(Doctor doctor)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            ViewBag.Message = "Please fill all fields";
            return View();
        }
        _doctorService.UpdateDoctor(doctor);
        return RedirectToAction(actionName: "Index", controllerName: "Doctor");
    }

    public IActionResult Create()
    {
        var departmentList = _departmentService.GetAllDepartments();
        ViewBag.Departments = departmentList;
        return View();
    }

    [HttpPost]
    public IActionResult Create(Doctor doctor)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            ViewBag.Message = "Please fill all fields";
            return View();
        }
        _doctorService.CreateDoctor(doctor);  
        return RedirectToAction(actionName: "Index", controllerName: "Doctor");
    }

    public IActionResult Delete(int id)
    {
        _doctorService.DeleteDoctor(id);
        return RedirectToAction(actionName: "Index", controllerName: "Doctor");
    }
}