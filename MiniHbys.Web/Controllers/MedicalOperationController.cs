using Microsoft.AspNetCore.Mvc;
using MiniHbys.Business.Abstraction;
using MiniHbys.Entity;

namespace MiniHbys.Web.Controllers;

public class MedicalOperationController : Controller
{
    private readonly IMedicalOperationService _medicalOperationService;
    private readonly IInspectionService _inspectionService;

    public MedicalOperationController(IMedicalOperationService medicalOperationService,IInspectionService inspectionService)
    {
        _medicalOperationService = medicalOperationService;
        _inspectionService = inspectionService;
    }
    // GET
    public IActionResult Index()
    {
        var medicalOperations = _medicalOperationService.GetAllMedicalOperations();
        return View(medicalOperations);
    }
    
    public IActionResult Detail(int id)
    {
        var medicalOperation = _medicalOperationService.GetMedicalOperationById(id);
        return View(medicalOperation);
    }

    public IActionResult Edit(int id)
    {
        var medicalOperation = _medicalOperationService.GetMedicalOperationById(id);
        var inspections = _inspectionService.GetAllInspections();
        ViewBag.Inspections = inspections;
        return View(medicalOperation);
    }

    [HttpPost]
    public IActionResult Edit(MedicalOperation medicalOperation)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            ViewBag.Message = "Please fill all fields";
            return View();
        }
        _medicalOperationService.UpdateMedicalOperation(medicalOperation);
        return RedirectToAction(actionName: "Index", controllerName: "MedicalOperation");
    }

    public IActionResult Create()
    {
        var inspections = _inspectionService.GetAllInspections();
        ViewBag.Inspections = inspections;
        return View();
    }

    [HttpPost]
    public IActionResult Create(MedicalOperation medicalOperation)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            ViewBag.Message = "Please fill all fields";
            return View();
        }
        _medicalOperationService.CreateMedicalOperation(medicalOperation);
        return RedirectToAction(actionName: "Index", controllerName: "MedicalOperation");
    }

    public IActionResult Delete(int id)
    {
        _medicalOperationService.DeleteMedicalOperation(id);
        return RedirectToAction(actionName: "Index", controllerName: "MedicalOperation");
    }
}