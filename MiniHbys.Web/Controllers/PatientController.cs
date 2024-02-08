using Microsoft.AspNetCore.Mvc;
using MiniHbys.Business.Abstraction;
using MiniHbys.Entity;

namespace MiniHbys.Web.Controllers;

public class PatientController : Controller
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }
    // GET
    public IActionResult Index()
    {
        var patients = _patientService.GetAllPatients();
        return View(patients);
    }
    
    public IActionResult Detail(int id)
    {
        var patient = _patientService.GetPatientById(id);
        return View(patient);
    }

    public IActionResult Edit(int id)
    {
        var patient = _patientService.GetPatientById(id);
        return View(patient);
    }

    [HttpPost]
    public IActionResult Edit(Patient patient)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            ViewBag.Message = "Please fill all fields";
            return View();
        }
        _patientService.UpdatePatient(patient);
        return RedirectToAction(actionName: "Index", controllerName: "Patient");
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Patient patient)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            ViewBag.Message = "Please fill all fields";
            return View();
        }
        _patientService.CreatePatient(patient);
        return RedirectToAction(actionName: "Index", controllerName: "Patient");
    }

    public IActionResult Delete(int id)
    {
        _patientService.DeletePatient(id);
        return RedirectToAction(actionName: "Index", controllerName: "Patient");
    }
}