using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MerchantPortal.Data.Concrete;
using MerchantPortal.Data.Models;
using MerchantPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace MerchantPortal.Controllers
{
    /// <summary>
    /// Modified by : Sarwar 
    /// Date         : 29-Jan-2018.
    /// Description  : Repository for Branch.
    /// </summary>
    /// 
    public class MctCommissionSetupController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public MctCommissionSetupController(IUnitOfWork unitOfWork, ILogger logger = null)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.MakeAudit(true, 1, 400);
            if (logger != null) { _logger = logger; }
        }

        // GET: Merchant commission           
        public ActionResult Index()
        {           
            return View();
        }

        [HttpPost]
        public IActionResult GetData()
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

            recordsTotal = (from m in _unitOfWork.MctCommissionSetupRepo.GetAll()
                            select m).Count();

            var data = (from m in _unitOfWork.MctCommissionSetupRepo.GetAll()
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
                                         m.TerminalName.ToLower().Contains(searchValue.ToLower())
                                    );
            }
            //var data = merchantData.Skip(skip).Take(pageSize);
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });           
        }

        // GET: Merchant commission
        public IActionResult ListPage(int? id)
        {
            List<MctCommissionSetup> mList = new List<MctCommissionSetup>();
            mList = _unitOfWork.MctCommissionSetupRepo.GetAll().ToList();
            return View(mList);
        }

        [HttpPost]    
        public IActionResult Create([Bind("MerchantId,TerminalId,Description,CountryId,ProductId,TrnxnType,CurrencyId,IsContinuousType,IsPercentage,CommissionAmount,BankPercentage,MinAmount,IsRoundUpAmount,IsRoundDownAmount,IsActive")] MctCommissionSetup mctCommissionSetup)
        {
            mctCommissionSetup.IsApprove = false;
            mctCommissionSetup.IsDeleted = false;
            mctCommissionSetup.EntryBy = 1;
            mctCommissionSetup.EntryDate = Convert.ToDateTime(DateTime.Now);
            mctCommissionSetup.UpdatedBy = 1;
            mctCommissionSetup.UpdatedDate = Convert.ToDateTime(DateTime.Now);

            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _unitOfWork.MctCommissionSetupRepo.Add(mctCommissionSetup);
                        _unitOfWork.Save();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        //todo
                        transaction.Rollback();
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            var ProductList = new SelectList(_unitOfWork.CountryRepo.GetAll().Where(c => (c.IsDeleted == false)).ToList(), "Id", "StrCountryName");
            ViewBag.ProductList = ProductList;

            MctCommissionSetupViewModel mctCommissionSetupViewModel = new MctCommissionSetupViewModel();
            mctCommissionSetupViewModel.Merchants = _unitOfWork.MerchantRepo.GetAll();
            mctCommissionSetupViewModel.Terminals = _unitOfWork.TerminalRepo.GetAll();
            mctCommissionSetupViewModel.Countries = _unitOfWork.CountryRepo.GetAll();
            mctCommissionSetupViewModel.Currencies = _unitOfWork.CurrencyRepo.GetAll();

            if (id != null)
            {
                mctCommissionSetupViewModel.MctCommissionSetup = _unitOfWork.MctCommissionSetupRepo.GetById(id);
            }
            return View(mctCommissionSetupViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int Id, [Bind("Id,MerchantId,TerminalId,Description,CountryId,ProductId,TrnxnType,CurrencyId,IsContinuousType,IsPercentage,CommissionAmount,BankPercentage,MinAmount,IsRoundUpAmount,IsRoundDownAmount,IsActive")] MctCommissionSetup mctCommissionSetup)
        {
            if (Id != mctCommissionSetup.Id)
            {
                return NotFound();
            }

            mctCommissionSetup.IsApprove = false;
            mctCommissionSetup.IsDeleted = false;
            mctCommissionSetup.EntryBy = 1;
            mctCommissionSetup.EntryDate = Convert.ToDateTime(DateTime.Now);
            mctCommissionSetup.UpdatedBy = 1;
            mctCommissionSetup.UpdatedDate = Convert.ToDateTime(DateTime.Now);

            if (ModelState.IsValid)
            {
                using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
                {
                    try
                    {
                        _unitOfWork.MctCommissionSetupRepo.Edit(mctCommissionSetup);
                        _unitOfWork.Save();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(mctCommissionSetup);
        }

        public IActionResult Details(int Id)
        {
            MctCommissionSetup mctCommissionSetup = new MctCommissionSetup();
            mctCommissionSetup = _unitOfWork.MctCommissionSetupRepo.GetById(Id);
            return View(mctCommissionSetup);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MctCommissionSetup mctCommissionSetup = _unitOfWork.MctCommissionSetupRepo.GetById(id);
            if (mctCommissionSetup == null)
            {
                return NotFound();
            }
            return View(mctCommissionSetup);
        }

        public IActionResult DeleteConfirmed(int? id)
        {        
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        MctCommissionSetup mctCommissionSetup = _unitOfWork.MctCommissionSetupRepo.GetById(id);
                        mctCommissionSetup.IsDeleted = true;
                        mctCommissionSetup.UpdatedBy = 1;
                        mctCommissionSetup.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.MctCommissionSetupRepo.Edit(mctCommissionSetup);
                        _unitOfWork.Save();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Active(int Id)
        {
            MctCommissionSetup mctCommissionSetup = new MctCommissionSetup();
            mctCommissionSetup = _unitOfWork.MctCommissionSetupRepo.GetById(Id);
            return View(mctCommissionSetup);
        }

        public IActionResult ActiveDeactiveConfirmed(int? id)
        {
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        MctCommissionSetup mctCommissionSetup = _unitOfWork.MctCommissionSetupRepo.GetById(id);
                        mctCommissionSetup.IsActive = mctCommissionSetup.IsActive ? false : true;
                        mctCommissionSetup.UpdatedBy = 1;
                        mctCommissionSetup.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.MctCommissionSetupRepo.Edit(mctCommissionSetup);
                        _unitOfWork.Save();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}