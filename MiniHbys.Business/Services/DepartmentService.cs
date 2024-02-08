using MiniHbys.Business.Abstraction;
using MiniHbys.Common.WebViewModels;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;

namespace MiniHbys.Business.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentManager _departmentManager;
    public DepartmentService(IDepartmentManager departmentManager)
    {
        _departmentManager = departmentManager;
    }
    
    public List<Department> GetAllDepartments()
    {
        return _departmentManager.GetAllDepartments();
    }

    public void CreateDepartment(Department department)
    {
        _departmentManager.CreateDepartment(department);
    }
    public void UpdateDepartment(Department department)
    {
        _departmentManager.UpdateDepartment(department);
    }

    public void DeleteDepartment(int departmentId)
    {
        var department = _departmentManager.GetDepartmentById(departmentId);

        if (department == null)
            throw new Exception("Couldnt find department with department id :"+departmentId);
        
        _departmentManager.DeleteDepartment(departmentId);
    }
    public Department GetDepartmentById(int departmentId)
    {
        return _departmentManager.GetDepartmentById(departmentId);
    }
}