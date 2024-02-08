using Microsoft.AspNetCore.Mvc;
using MiniHbys.Business.Abstraction;
using MiniHbys.Common.WebViewModels;
using MiniHbys.Entity;


namespace MiniHbys.Web.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    public UserController(IUserService userService,IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
    }
    
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        //Login Actions
        var user = _userService.Login(model.Username, model.Password);
        if (user == null)
        {
            ViewBag.Message = "Couldnt find user";
            return View();
        }
        
        HttpContext.Session.SetInt32("UserId",user.UserID);
        HttpContext.Session.SetString("UserName",user.UserName);
        HttpContext.Session.SetString("UserSurname",user.UserSurname);
        HttpContext.Session.SetString("UserFullname",string.Format("{0} {1}",user.UserName,user.UserSurname));
        HttpContext.Session.SetString("UserEmail",user.UserEmail);
        HttpContext.Session.SetInt32("DepartmentReadAccess",user.Role.DepartmentReadAccess == true ? 1:0);
        HttpContext.Session.SetInt32("DepartmentWriteAccess",user.Role.DepartmentWriteAccess == true ? 1:0);
        HttpContext.Session.SetInt32("DoctorReadAccess",user.Role.DoctorReadAccess == true ? 1:0);
        HttpContext.Session.SetInt32("DoctorWriteAccess",user.Role.DoctorWriteAccess == true ? 1:0);
        HttpContext.Session.SetInt32("InspectionReadAccess",user.Role.InspectionReadAccess == true ? 1:0);
        HttpContext.Session.SetInt32("InspectionWriteAccess",user.Role.InspectionWriteAccess == true ? 1:0);
        HttpContext.Session.SetInt32("IsSuperAdmin",user.Role.IsSuperAdmin == true ? 1:0);
        HttpContext.Session.SetInt32("PatientReadAccess",user.Role.PatientReadAccess == true ? 1:0);
        HttpContext.Session.SetInt32("PatientWriteAccess",user.Role.PatientWriteAccess == true ? 1:0);
        HttpContext.Session.SetInt32("MedicalOperationReadAccess",user.Role.MedicalOperationReadAccess == true ? 1:0);
        HttpContext.Session.SetInt32("MedicalOperationWriteAccess",user.Role.MedicalOperationWriteAccess == true ? 1:0);
        HttpContext.Session.SetInt32("MedicineItemReadAccess",user.Role.MedicineItemReadAccess == true ? 1:0);
        HttpContext.Session.SetInt32("MedicineItemWriteAccess",user.Role.MedicineItemWriteAccess == true ? 1:0);
        
        return RedirectToAction(actionName: "Index", controllerName: "Home");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        //register actions
        return RedirectToAction(actionName: "Login", controllerName: "User");
    }
    
    public IActionResult Index()
    {
        var users = _userService.GetAllUsers();
        return View(users);
    }
    
    public IActionResult Detail(int id)
    {
        var user = _userService.GetUserById(id);
        return View(user);
    }

    public IActionResult Edit(int id)
    {
        var roles = _roleService.GetAllRoles();
        var user = _userService.GetUserById(id);
        ViewBag.Roles = roles;
        return View(user);
    }

    [HttpPost]
    public IActionResult Edit(User user)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            ViewBag.Message = "Please fill all fields";
            return View();
        }
        _userService.UpdateUser(user);

        #region Refresh Session With New Role Informations

        var currentUser = _userService.GetUserById(user.UserID);

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
        
        return RedirectToAction(actionName: "Index", controllerName: "User");
    }

    public IActionResult Create()
    { 
        var roles = _roleService.GetAllRoles();
        ViewBag.Roles = roles;
        return View();
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        var modelState = ModelState.IsValid;
        if (!modelState)
        {
            ViewBag.Message = "Please fill all fields";
            return View();
        }
        _userService.CreateUser(user);
        return RedirectToAction(actionName: "Index", controllerName: "User");
    }

    public IActionResult Delete(int id)
    {
        _userService.DeleteUser(id);
        return RedirectToAction(actionName: "Index", controllerName: "User");
    }
}