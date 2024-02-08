using Microsoft.AspNetCore.Mvc;
using MiniHbys.Business.Abstraction;
using MiniHbys.Entity;

namespace MiniHbys.Web.Controllers;

public class InspectionController : Controller
{
    private readonly IInspectionService _inspectionService;
    private readonly IDoctorService _doctorService;
    private readonly IPatientService _patientService;

    public InspectionController(IInspectionService inspectionService,IDoctorService doctorService,IPatientService 
    patientService)
    {
        _inspectionService = inspectionService;
        _doctorService = doctorService;
        _patientService = patientService;
    }
    // GET
    public IActionResult Index()
    {
        var inspections = _inspectionService.GetAllInspections();
        return View(inspections);
    }
    
    public IActionResult Detail(int id)
    {
        var inspection = _inspectionService.GetInspectionById(id);
        return View(inspection);
    }

    public IActionResult Edit(int id)
    {
        var doctors = _doctorService.GetAllDoctors();
        var patients = _patientService.GetAllPatients();    
        var inspection = _inspectionService.GetInspectionById(id);
        ViewBag.Doctors = doctors;
        ViewBag.Patients = patients;
        return View(inspection);
    }

    [HttpPost]
    public IActionResult Edit(Inspection inspection)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            ViewBag.Message = "Please fill all fields";
            return View();
        }
        _inspectionService.UpdateInspection(inspection);
        return RedirectToAction(actionName: "Index", controllerName: "Inspection");
    }

    public IActionResult Create()
    {
        var doctors = _doctorService.GetAllDoctors();
        var patients = _patientService.GetAllPatients();
        ViewBag.Doctors = doctors;
        ViewBag.Patients = patients;
        return View();
    }

    [HttpPost]
    public IActionResult Create(Inspection inspection)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            var doctors = _doctorService.GetAllDoctors();
            var patients = _patientService.GetAllPatients();
            ViewBag.Doctors = doctors;
            ViewBag.Patients = patients;
            ViewBag.Message = "Please fill all fields";
            return View();
        }
        _inspectionService.CreateInspection(inspection);
        return RedirectToAction(actionName: "Index", controllerName: "Inspection");
    }

    public IActionResult Delete(int id)
    {
        _inspectionService.DeleteInspection(id);
        return RedirectToAction(actionName: "Index", controllerName: "Inspection");
    }
}