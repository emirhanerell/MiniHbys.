using MiniHbys.Entity;

namespace MiniHbys.DataAccess.Abstraction;

public interface IDepartmentManager
{
    void CreateDepartment(Department department);
    void UpdateDepartment(Department department);
    void DeleteDepartment(int departmentId);
    Department GetDepartmentById(int departmentId);
    List<Department> GetAllDepartments();
}