using MerchantPortal.Data.Concrete;
using MerchantPortal.Data.Models;
using MerchantPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace MerchantPortal.Controllers
{
    public class MerchantController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public MerchantController(IUnitOfWork unitOfWork, ILogger logger = null)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.MakeAudit(true, 1, 400);
            if (logger != null) { _logger = logger; }
        }
        public IActionResult Index()
        {
            var ProductList = new SelectList(_unitOfWork.CountryRepo.GetAll().Where(c => (c.IsDeleted == false)).ToList(), "Id", "StrCountryName");
            ViewBag.ProductList = ProductList;

            MerchantViewModel merchantViewModel = new MerchantViewModel();
            merchantViewModel.Merchants = _unitOfWork.MerchantRepo.GetAll();
            merchantViewModel.Terminals = _unitOfWork.TerminalRepo.GetAll();
            merchantViewModel.Countries = _unitOfWork.CountryRepo.GetAll();
            merchantViewModel.Currencies = _unitOfWork.CurrencyRepo.GetAll();

            return View(merchantViewModel);
        }
        public IActionResult MerchantCreate(Merchant merchant)
        {
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        merchant.IsActive = false;
                        merchant.IsApprove = false;
                        merchant.IsDeleted = false;
                        merchant.EntryBy = 1;
                        merchant.EntryDate = Convert.ToDateTime(DateTime.Now);
                        merchant.UpdatedBy = 1;
                        merchant.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.MerchantRepo.Add(merchant);
                        _unitOfWork.Save();
                        transaction.Commit();
                        TempData["msg"] = "<script>alert('Saved succesfully');</script>";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            //return View("Index");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditMerchant(Int64 Id, Merchant merchant)
        {
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        merchant.Id = Id;
                        merchant.UpdatedBy = 1;
                        merchant.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.MerchantRepo.Edit(merchant);
                        _unitOfWork.Save();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return RedirectToAction(nameof(MerchantList));
        }

        public IActionResult MerchantList(Merchant merchant)
        {
            //List<Merchant> mList = new List<Merchant>();
            //mList = _unitOfWork.MerchantRepo.GetAll().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult GetData(Terminal terminal)
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

            recordsTotal = (from m in _unitOfWork.MerchantRepo.GetAll()
                            select m).Count();

            var data = (from m in _unitOfWork.MerchantRepo.GetAll()
                        select m).Skip(skip).Take(pageSize);

            ////Sorting   .Skip(skip).Take(pageSize)
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                data = data.AsQueryable().OrderBy(sortColumn + " " + sortColumnDirection);
            }
            ////Search
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(m => m.MerchantName.ToLower().Contains(searchValue.ToLower()) ||
                                         (m.BizOrgName != null && m.BizOrgName.ToLower().Contains(searchValue.ToLower())) ||
                                         (m.BizOwnerName != null && m.BizOwnerName.ToLower().Contains(searchValue.ToLower())) ||
                                         (m.BizContactAddess != null && m.BizContactAddess.ToLower().Contains(searchValue.ToLower())) ||
                                         (m.BizPhone != null && m.BizPhone.ToLower().Contains(searchValue.ToLower())) ||
                                         (m.BizEmail != null && m.BizEmail.ToLower().Contains(searchValue.ToLower()))
                                    );
            }
            //var data = merchantData.Skip(skip).Take(pageSize);
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }

        public IActionResult Details(Int64 Id)
        {
            Merchant merchant = new Merchant();
            merchant = _unitOfWork.MerchantRepo.GetById(Id);
            return View(merchant);
        }

        public IActionResult Active(Int64 Id)
        {
            Merchant merchant = new Merchant();
            merchant = _unitOfWork.MerchantRepo.GetById(Id);
            return View(merchant);
        }

        public IActionResult Edit(Int64? id)
        {
            Merchant _merchant = null;
            if (id != null)
            {
                _merchant = _unitOfWork.MerchantRepo.GetById(id);
            }
            return View(_merchant);
        }

        public IActionResult Delete(Int64? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var _merchant = _unitOfWork.MerchantRepo.GetById(id);
            if (_merchant == null)
            {
                return NotFound();
            }
            return View(_merchant);
        }

        public IActionResult ActiveDeactiveConfirm(Int64? id)
        {
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Merchant _Merchant = _unitOfWork.MerchantRepo.GetById(id);
                        _Merchant.IsActive = _Merchant.IsActive ? false : true;
                        _Merchant.UpdatedBy = 1;
                        _Merchant.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.MerchantRepo.Edit(_Merchant);
                        _unitOfWork.Save();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return RedirectToAction(nameof(MerchantList));
        }

        public IActionResult DeleteConfirm(Int64? id)
        {
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Merchant _Merchant = _unitOfWork.MerchantRepo.GetById(id);
                        _Merchant.IsDeleted = true;
                        _Merchant.UpdatedBy = 1;
                        _Merchant.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.MerchantRepo.Edit(_Merchant);
                        _unitOfWork.Save();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return RedirectToAction(nameof(MerchantList));
        }
    }
}