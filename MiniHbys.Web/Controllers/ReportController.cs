using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using MiniHbys.Business.Abstraction;
using MiniHbys.DataAccess.Managers;
using MiniHbys.Entity;
using MiniHbys.Web.Models;

namespace MiniHbys.Web.Controllers;

public class ReportController : Controller
{
    private readonly IReportService _reportService;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;

    public ReportController(IReportService reportService, IPatientService patientService, IDoctorService doctorService)
    {
        _reportService = reportService;
        _patientService = patientService;
        _doctorService = doctorService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetInspectionsByDate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult GetInspectionsByDateReport(GetInspectionByDateViewModel model)
    {
        if (model.EndDate <= model.StartDate)
        {
            ViewBag.Message = "End date have to be greater than start date";
            return View(viewName: "GetInspectionsByDate");
        }

        var inspections = _reportService.GetInspectionsByDate(model.StartDate, model.EndDate);
        var reportJson = System.Text.Json.JsonSerializer.Serialize(inspections);
        HttpContext.Session.SetString("InspectionsByDateReport", reportJson);
        return View(inspections);
    }

    public IActionResult GetInspectionsByDoctor()
    {
        var doctors = _doctorService.GetAllDoctors();
        ViewBag.Doctors = doctors;
        return View();
    }

    [HttpPost]
    public IActionResult GetInspectionsByDoctorReport(GetInspectionByDoctorViewModel model)
    {
        if (model.DoctorID == 0)
        {
            ViewBag.Message = "Please choose doctor";
            return View(viewName: "GetInspectionsByDoctor");
        }

        var inspections = _reportService.GetInspectionsByDoctor(model.DoctorID);
        var reportJson = System.Text.Json.JsonSerializer.Serialize(inspections);
        HttpContext.Session.SetString("InspectionsByDoctorReport", reportJson);
        return View(inspections);
    }

    public IActionResult GetInspectionsByPatient()
    {
        var patients = _patientService.GetAllPatients();
        ViewBag.Patients = patients;
        return View();
    }

    [HttpPost]
    public IActionResult GetInspectionsByPatientReport(GetInspectionByPatientViewModel model)
    {
        if (model.PatientID == 0)
        {
            ViewBag.Message = "Please choose patient";
            return View(viewName: "GetInspectionsByPatient");
        }

        var inspections = _reportService.GetInspectionsByPatient(model.PatientID);
        var reportJson = System.Text.Json.JsonSerializer.Serialize(inspections);
        HttpContext.Session.SetString("InspectionsByPatientReport", reportJson);
        return View(inspections);
    }

    public IActionResult GetMedicineItemsByPatient()
    {
        var patients = _patientService.GetAllPatients();
        ViewBag.Patients = patients;
        return View();
    }

    [HttpPost]
    public IActionResult GetMedicineItemsByPatientReport(GetMedicineItemsByPatientViewModel model)
    {
        if (model.PatientID == 0)
        {
            ViewBag.Message = "Please choose patient";
            return View(viewName: "GetMedicineItemsByPatient");
        }

        var medicineItems = _reportService.GetMedicineItemsByPatient(model.PatientID);
        var reportJson = System.Text.Json.JsonSerializer.Serialize(medicineItems);
        HttpContext.Session.SetString("MedicineItemsByPatientReport", reportJson);
        return View(medicineItems);
    }

    public IActionResult GetPatientsByBirthDate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult GetPatientsByBirthDateReport(GetPatientsByBirthDateViewModel model)
    {
        if (model.EndDate <= model.StartDate)
        {
            ViewBag.Message = "End date have to be greater than start date";
            return View(viewName: "GetPatientsByBirthDate");
        }

        var patients = _reportService.GetPatientsByBirthDate(model.StartDate, model.EndDate);
        var reportJson = System.Text.Json.JsonSerializer.Serialize(patients);
        HttpContext.Session.SetString("PatientsByBirthDateReport", reportJson);
        return View(patients);
    }

    public IActionResult ExportReport(string reportName)
    {
        var reportJson = HttpContext.Session.GetString(reportName);
        string fileName = string.Empty;
        switch (reportName)
        {
            case "PatientsByBirthDateReport":
                var patientsByBirtDate = System.Text.Json.JsonSerializer.Deserialize<List<Patient>>(reportJson);
                fileName =
                    $"PatientsByBirthDateReport_{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-{DateTime.Now.Millisecond}";
                Utilities.ExportData.ExportCsv(patientsByBirtDate, fileName);
                break;
            case "MedicineItemsByPatientReport":
                var medicineItemByPatients = System.Text.Json.JsonSerializer.Deserialize<List<MedicineItem>>(reportJson);
                var medicineItemReportItems = GetReportItemListForMedicineItem(medicineItemByPatients);
                fileName =
                    $"MedicineItemsByPatientReport_{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-{DateTime.Now.Millisecond}";
                Utilities.ExportData.ExportCsv(medicineItemReportItems, fileName);
                break;
            case "InspectionsByPatientReport":
                var inspectionsByPatient = System.Text.Json.JsonSerializer.Deserialize<List<Inspection>>(reportJson);
                var inspectionsByPatientReportItems = GetReportItemListForInspection(inspectionsByPatient);
                fileName =
                    $"InspectionsByPatientReport_{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-{DateTime.Now.Millisecond}";
                Utilities.ExportData.ExportCsv(inspectionsByPatientReportItems, fileName);
                break;
            case "InspectionsByDoctorReport":
                var inspectionsByDoctor = System.Text.Json.JsonSerializer.Deserialize<List<Inspection>>(reportJson);
                var inspectionsByDoctorReportItems = GetReportItemListForInspection(inspectionsByDoctor);
                fileName =
                    $"InspectionsByDoctorReport_{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-{DateTime.Now.Millisecond}";
                Utilities.ExportData.ExportCsv(inspectionsByDoctorReportItems, fileName);
                break;
            case "InspectionsByDateReport":
                var inspectionsByDate = System.Text.Json.JsonSerializer.Deserialize<List<Inspection>>(reportJson);
                var inspectionsByDateReportItems = GetReportItemListForInspection(inspectionsByDate);
                fileName =
                    $"InspectionsByDateReport_{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-{DateTime.Now.Millisecond}";
                Utilities.ExportData.ExportCsv(inspectionsByDateReportItems, fileName);
                break;
        }

        return RedirectToAction("Index");
    }

    [NonAction]
    private List<InspectionReportItem> GetReportItemListForInspection(List<Inspection> inspections)
    {
        var inspectionReportItems = new List<InspectionReportItem>();

        foreach (var inspection in inspections)
        {
            var reportItem = new InspectionReportItem();
            var doctorFullName = string.Format("{0} {1}",inspection.Doctor?.DoctorName ?? string.Empty ,
                inspection.Doctor?.DoctorSurname ?? string.Empty);
            var patientFullName = string.Format("{0} {1}",inspection.Patient?.PatientName ?? string.Empty ,
                inspection.Patient?.PatientSurname ?? string.Empty);
            reportItem.DoctorName = doctorFullName;
            reportItem.InspectionDate = inspection.InspectionDate ?? DateTime.MinValue;
            reportItem.InspectionResult = inspection.InspectionResult;
            reportItem.PatientName = patientFullName;
            
            inspectionReportItems.Add(reportItem);
        }

        return inspectionReportItems;
    }

    [NonAction]
    private List<MedicineItemReportItem> GetReportItemListForMedicineItem(List<MedicineItem> medicineItems)
    {
        var medicineItemReportItems = new List<MedicineItemReportItem>();
        foreach (var item in medicineItems)
        {
            var reportItem = new MedicineItemReportItem();
            var doctorFullName = string.Format("{0} {1}",item.Inspection?.Doctor?.DoctorName ?? string.Empty ,
                item.Inspection?.Doctor?.DoctorSurname ?? string.Empty);
            var patientFullName = string.Format("{0} {1}",item.Inspection?.Patient?.PatientName ?? string.Empty ,
                item.Inspection?.Patient?.PatientSurname ?? string.Empty);

            reportItem.DoctorName = doctorFullName;
            reportItem.InspectionDate = item.Inspection?.InspectionDate ?? DateTime.MinValue;
            reportItem.MedicineName = item.MedicineName;
            reportItem.PatientName = patientFullName;
            medicineItemReportItems.Add(reportItem);
        }

        return medicineItemReportItems;
    }
}