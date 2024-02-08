namespace MiniHbys.Web.Models;

public class InspectionReportItem
{
    public DateTime InspectionDate { get; set; }
    public string DoctorName { get; set; }
    public string PatientName { get; set; }
    public string InspectionResult { get; set; }
}