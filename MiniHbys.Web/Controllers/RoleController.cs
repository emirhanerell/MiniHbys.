using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MiniHbys.Business.Abstraction;
using MiniHbys.Entity;

namespace MiniHbys.Web.Controllers;

public class RoleController : Controller
{
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;

    public RoleController(IRoleService roleService,IUserService userService)
    {
        _roleService = roleService;
        _userService = userService;
    }
    // GET
    public IActionResult Index()
    {
        var roles = _roleService.GetAllRoles();
        return View(roles);
    }
    
    public IActionResult Detail(int id)
    {
        var role = _roleService.GetRoleById(id);
        return View(role);
    }

    public IActionResult Edit(int id)
    {
        var role = _roleService.GetRoleById(id);
        return View(role);
    }

    [HttpPost]
    public IActionResult Edit(Role role)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            ViewBag.Message = "Please fill all fields";
            return View();
        }
        _roleService.UpdateRole(role);
        
        #region Refresh Session With New Role Informations

        var currentUserId = HttpContext.Session.GetInt32("UserId");
        var currentUser = _userService.GetUserById(currentUserId ?? 0);

        if (currentUser != null)
        {
            HttpContext.Session.SetInt32("DepartmentReadAccess",currentUser.Role.DepartmentReadAccess == true ? 1:0);
            HttpContext.Session.SetInt32("DepartmentWriteAccess",currentUser.Role.DepartmentWriteAccess == true ? 1:0);
            HttpContext.Session.SetInt32("DoctorReadAccess",currentUser.Role.DoctorReadAccess == true ? 1:0);
            HttpContext.Session.SetInt32("DoctorWriteAccess",currentUser.Role.DoctorWriteAccess == true ? 1:0);
            HttpContext.Session.SetInt32("InspectionReadAccess",currentUser.Role.InspectionReadAccess == true ? 1:0);
            HttpContext.Session.SetInt32("InspectionWriteAccess",currentUser.Role.InspectionWriteAccess == true ? 1:0);
            HttpContext.Session.SetInt32("IsSuperAdmin",currentUser.Role.IsSuperAdmin == true ? 1:0);
            HttpContext.Session.SetInt32("PatientReadAccess",currentUser.Role.PatientReadAccess == true ? 1:0);
            HttpContext.Session.SetInt32("PatientWriteAccess",currentUser.Role.PatientWriteAccess == true ? 1:0);
            HttpContext.Session.SetInt32("MedicalOperationReadAccess",currentUser.Role.MedicalOperationReadAccess == true ? 1:0);
            HttpContext.Session.SetInt32("MedicalOperationWriteAccess",currentUser.Role.MedicalOperationWriteAccess == true ? 1:0);
            HttpContext.Session.SetInt32("MedicineItemReadAccess",currentUser.Role.MedicineItemReadAccess == true ? 1:0);
            HttpContext.Session.SetInt32("MedicineItemWriteAccess",currentUser.Role.MedicineItemWriteAccess == true ? 1:0);
        }

        #endregion
        
        return RedirectToAction(actionName: "Index", controllerName: "Role");
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Role role)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            ViewBag.Message = "Please fill all fields";
            return View();
        }
        _roleService.CreateRole(role);
        return RedirectToAction(actionName: "Index", controllerName: "Role");
    }

    public IActionResult Delete(int id)
    {
        _roleService.DeleteRole(id);
        return RedirectToAction(actionName: "Index", controllerName: "Role");
    }
}