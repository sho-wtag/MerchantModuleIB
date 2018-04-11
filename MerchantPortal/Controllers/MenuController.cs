using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MerchantPortal.Data.Concrete;
using MerchantPortal.Data.Models;
using MerchantPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MerchantPortal.Controllers
{
    public class MenuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public MenuController(IUnitOfWork unitOfWork, ILogger<AccountController> logger)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.MakeAudit(true, 1, 400);
            if (logger != null) { _logger = logger; }
        }
        protected IEnumerable<ControllerActionMapping> ControllerActionMappingList()
        {
            return _unitOfWork.ControllerActionMappingRepo.GetAll();
        }

        protected IEnumerable<ApplicationRole> LoadRole()
        {
            try
            {
                MenuViewModel model = new MenuViewModel();
                IEnumerable<ApplicationRole> lstRole = _unitOfWork.ApplicationRoleRepo.GetAll().Where(a => a.IsDeleted == false).ToList();
                return lstRole;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                MenuViewModel model = new MenuViewModel();
                model.ApplicationRoleList = LoadRole();
                model.ControllerActionMappingList = _unitOfWork.ControllerActionMappingRepo.GetAll().ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public IActionResult Create(MenuViewModel model)
        {
            foreach (var item in model.ControllerActionMappingList)
            {
                if (item.IsActive)
                {
                    RolePermission rolePermission = new RolePermission
                    {
                        RoleId = model.ApplicationRole.Id,
                        ControllerActionId = item.Id,
                        Permission = true
                    };
                    int parentId = _unitOfWork.RolePermissionRepo.HasParentPermission(item.Id, model.ApplicationRole.Id);
                    if (parentId == 0)
                    {
                        RolePermission parent = new RolePermission
                        {
                            RoleId = model.ApplicationRole.Id,
                            ControllerActionId = parentId,
                            Permission = true
                        };
                        _unitOfWork.RolePermissionRepo.Add(parent);
                        _unitOfWork.Save();
                    }
                    _unitOfWork.RolePermissionRepo.Add(rolePermission);
                    _unitOfWork.Save();

                }
            }
            return RedirectToAction(nameof(Create));
        }
    }
}