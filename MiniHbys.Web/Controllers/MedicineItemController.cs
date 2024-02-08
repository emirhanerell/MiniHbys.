using Microsoft.AspNetCore.Mvc;
using MiniHbys.Business.Abstraction;
using MiniHbys.Entity;

namespace MiniHbys.Web.Controllers;

public class MedicineItemController : Controller
{
    private readonly IMedicineItemService _medicineItemService;
    private readonly IInspectionService _inspectionService;
    private readonly IPatientService _patientService;

    public MedicineItemController(IMedicineItemService medicineItemService,IInspectionService inspectionService,
    IPatientService patientService)
    {
        _medicineItemService = medicineItemService;
        _inspectionService = inspectionService;
        _patientService = patientService;
    }
    // GET
    public IActionResult Index()
    {
        var medicineItems = _medicineItemService.GetAllMedicineItems();
        return View(medicineItems);
    }
    public IActionResult Detail(int id)
    {
        var medicineItem = _medicineItemService.GetMedicineItemById(id);
        return View(medicineItem);
    }

    public IActionResult Edit(int id)
    {
        var medicineItem = _medicineItemService.GetMedicineItemById(id);
        var inspections = _inspectionService.GetAllInspections();
        var patients = _patientService.GetAllPatients();
        ViewBag.Inspections = inspections;
        ViewBag.Patients = patients;
        return View(medicineItem);
    }

    [HttpPost]
    public IActionResult Edit(MedicineItem medicineItem)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            ViewBag.Message = "Please fill all fields";
            return View();
        }
        _medicineItemService.UpdateMedicineItem(medicineItem);
        return RedirectToAction(actionName: "Index", controllerName: "MedicineItem");
    }

    public IActionResult Create()
    {
        var inspections = _inspectionService.GetAllInspections();
        var patients = _patientService.GetAllPatients();
        ViewBag.Inspections = inspections;
        ViewBag.Patients = patients;
        return View();
    }

    [HttpPost]
    public IActionResult Create(MedicineItem medicineItem)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            ViewBag.Message = "Please fill all fields";
            return View();
        }
        _medicineItemService.CreateMedicineItem(medicineItem);
        return RedirectToAction(actionName: "Index", controllerName: "MedicineItem");
    }

    public IActionResult Delete(int id)
    {
        _medicineItemService.DeleteMedicineItem(id);
        return RedirectToAction(actionName: "Index", controllerName: "MedicineItem");
    }
}