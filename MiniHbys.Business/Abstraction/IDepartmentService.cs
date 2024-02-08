using MiniHbys.Common.WebViewModels;
using MiniHbys.Entity;

namespace MiniHbys.Business.Abstraction;

public interface IDepartmentService
{
    List<Department> GetAllDepartments();
    void CreateDepartment(Department department);
    void UpdateDepartment(Department department);
    void DeleteDepartment(int departmentId);
    Department GetDepartmentById(int departmentId);
}