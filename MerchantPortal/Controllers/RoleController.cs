using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MerchantPortal.Data.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;
using MerchantPortal.Models;
using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MerchantPortal.Helper;

namespace MerchantPortal.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public RoleController(RoleManager<ApplicationRole> roleManager, IUnitOfWork unitOfWork, ILogger<AccountController> logger)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.MakeAudit(true, 1, 400);
            _roleManager = roleManager;
            if (logger != null) { _logger = logger; }
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetRole()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault() == "asc" ? "ascending" : "descending";
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                recordsTotal = (from m in _unitOfWork.ApplicationRoleRepo.GetAll()
                                select m).Count();

                var data = (from m in
                                _unitOfWork.ApplicationRoleRepo.GetAll().Where(c => c.IsDeleted == false)
                            select m).Skip(skip).Take(pageSize);

                ////Sorting   .Skip(skip).Take(pageSize)
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    data = data.AsQueryable().OrderBy(sortColumn + " " + sortColumnDirection);
                }
                ////Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    data = data.Where(m => m.Name.ToLower().Contains(searchValue.ToLower())
                                        );
                }
                //var data = merchantData.Skip(skip).Take(pageSize);
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            ApplicationRoleViewModel model = new ApplicationRoleViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationRoleViewModel ApplicationRole)
        {
            if (ModelState.IsValid)
            {
                int exists = (from m in
                                  _unitOfWork.ApplicationRoleRepo.GetAll().Where(
                                      a => a.Name.ToLower() == ApplicationRole.Name.ToLower() &&
                                      a.IsDeleted == false)
                              select m).Count();
                if (exists > 0)
                {
                    ApplicationRole.MessageText = Notification.Show("Already Exixts", "Role", ToastType.Warning);
                }
                else
                {
                    ApplicationRole application = new ApplicationRole();
                    application.Name = ApplicationRole.Name;
                    application.NormalizedName = ApplicationRole.Name.ToUpper();
                    application.EntryBy = 1;
                    application.EntryDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
                    application.UpdatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));

                    _unitOfWork.ApplicationRoleRepo.Add(application);
                    _unitOfWork.Save();
                    ApplicationRole.MessageText = Notification.Show("Save successfully", "Role", ToastType.Warning);

                    //////////////////////////Using Identity Role Management/////////////////////////////////////
                    //var result = await _roleManager.CreateAsync(ApplicationRole);
                    //if (result.Succeeded)
                    //{
                    //    _logger.LogInformation("Role created.");
                    //    return RedirectToAction(nameof(Index));
                    //}
                    //AddErrors(result);
                    //////////////////////////////////////////////////////////////////////////////////////////////
                }
            }
            return View(ApplicationRole);
        }

        [HttpGet]
        public IActionResult Edit(Int64? id)
        {
            ApplicationRoleViewModel viewModel = new ApplicationRoleViewModel();
            try
            {
                ApplicationRole model = _unitOfWork.ApplicationRoleRepo.GetById(id);
                ModelAdapter.ModelMap(viewModel, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(viewModel);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Int64 id, ApplicationRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id.ToString());
                if (applicationRole != null && applicationRole.Name != model.Name)
                {
                    applicationRole.Name = model.Name;
                    applicationRole.NormalizedName = model.Name.ToUpper();
                    applicationRole.UpdatedBy = 1; //HttpContext.Session.GetString(SessionVariable.UserName)!=null?Convert.ToInt64(HttpContext.Session.GetString(SessionVariable.UserName)):1;
                    applicationRole.UpdatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
                    _unitOfWork.ApplicationRoleRepo.Edit(id, applicationRole);
                    _unitOfWork.Save();
                    model.MessageText = Notification.Show("Update Successfully", "Role", ToastType.Warning);

                    //////////////////////////Using Identity Role Management/////////////////////////////////////
                    //var result = await _roleManager.UpdateAsync(applicationRole);
                    //if (result.Succeeded)
                    //{
                    //    _logger.LogInformation("User created a new account with password.");
                    //    return RedirectToAction(nameof(Index));
                    //}
                    //AddErrors(result);
                    //////////////////////////////////////////////////////////////////////////////////////////////
                }

            }
            return View(model);
        }

        public IActionResult Delete(Int64? id)
        {

            ApplicationRole model = _unitOfWork.ApplicationRoleRepo.GetById(id);
            ApplicationRoleViewModel viewModel = new ApplicationRoleViewModel();
            ModelAdapter.ModelMap(viewModel, model);
            return View(viewModel);
            //ApplicationRole applicationRole = _unitOfWork.ApplicationRoleRepo.GetById(id);
            //ApplicationRoleViewModel viewModel = new ApplicationRoleViewModel
            //{
            //    Id = applicationRole.Id,
            //    Name = applicationRole.Name
            //};
            //return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Int64 id, ApplicationRoleViewModel model)
        {
            try
            {
                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id.ToString());
                if (applicationRole != null)
                {
                    applicationRole.IsDeleted = true;
                    applicationRole.UpdatedBy = 1; //HttpContext.Session.GetString(SessionVariable.UserName)!=null?Convert.ToInt64(HttpContext.Session.GetString(SessionVariable.UserName)):1;
                    applicationRole.UpdatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
                    _unitOfWork.ApplicationRoleRepo.Edit(id, applicationRole);
                    _unitOfWork.Save();
                    Notification.Show("Delete Successfully", "Role", ToastType.Warning);

                    //////////////////////////Using Identity Role Management/////////////////////////////////////
                    //var result = await _roleManager.UpdateAsync(applicationRole);
                    //if (result.Succeeded)
                    //{
                    //    _logger.LogInformation("User created a new account with password.");
                    //    return RedirectToAction(nameof(Index));
                    //}
                    //AddErrors(result);
                    //////////////////////////////////////////////////////////////////////////////////////////////                
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Active(Int64? id)
        {
            ApplicationRoleViewModel viewModel = new ApplicationRoleViewModel();
            try
            {
                ApplicationRole model = _unitOfWork.ApplicationRoleRepo.GetById(id);
                ModelAdapter.ModelMap(viewModel, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Active(Int64 id, ApplicationRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id.ToString());
                if (applicationRole != null && applicationRole.Name != model.Name)
                {
                    applicationRole.IsActive = applicationRole.IsActive ? false : true;
                    applicationRole.UpdatedBy = 1; //HttpContext.Session.GetString(SessionVariable.UserName)!=null?Convert.ToInt64(HttpContext.Session.GetString(SessionVariable.UserName)):1;
                    applicationRole.UpdatedDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
                    _unitOfWork.ApplicationRoleRepo.Edit(id, applicationRole);
                    _unitOfWork.Save();
                }

            }
            return View(model);
        }

        [ActionName("IsBankExist")]
        private bool RoleExists(Int64 id)
        {
            return _unitOfWork.ApplicationRoleRepo.GetAll().Any(e => e.Id == id);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}