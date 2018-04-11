using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MerchantPortal.Data.Concrete;
using MerchantPortal.Data.Models;
using MerchantPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace MerchantPortal.Controllers
{
    public class MctVATSetupController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public MctVATSetupController(IUnitOfWork unitOfWork, ILogger logger = null)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.MakeAudit(true, 1, 400);
            if (logger != null) { _logger = logger; }
        }

        public IActionResult Index()
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

            recordsTotal = (from m in _unitOfWork.MctVATSetupRepo.GetAll()
                            select m).Count();

            var data = (from m in _unitOfWork.MctVATSetupRepo.GetAll()
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
        public IActionResult Create([Bind("VATID,MerchantId,TerminalId,Descriptions,CountryId,VATRegNo,IsContinuousType,Percentage,IsActive")] MctVATSetup mctVATSetup)
        {
            mctVATSetup.IsApprove = false;
            mctVATSetup.IsDeleted = false;
            mctVATSetup.EntryBy = 1;
            mctVATSetup.EntryDate = Convert.ToDateTime(DateTime.Now);
            mctVATSetup.UpdatedBy = 1;
            mctVATSetup.UpdatedDate = Convert.ToDateTime(DateTime.Now);

            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _unitOfWork.MctVATSetupRepo.Add(mctVATSetup);
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
            MctVATSetupViewModel mctVATSetupViewModel = new MctVATSetupViewModel();
            mctVATSetupViewModel.Merchants = _unitOfWork.MerchantRepo.GetAll();
            mctVATSetupViewModel.Terminals = _unitOfWork.TerminalRepo.GetAll();
            mctVATSetupViewModel.Countries = _unitOfWork.CountryRepo.GetAll();
            mctVATSetupViewModel.Currencies = _unitOfWork.CurrencyRepo.GetAll();

            if (id != null)
            {
                mctVATSetupViewModel.MctVATSetup = _unitOfWork.MctVATSetupRepo.GetById(id);
            }
            return View(mctVATSetupViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,VATID,MerchantID,TerminalId,Descriptions,CountryId,VATRegNo,IsContinuousType,Percentage,IsActive")] MctVATSetup mctVATSetup)
        {
            if (id != mctVATSetup.ID)
            {
                return NotFound();
            }

            mctVATSetup.IsApprove = false;
            mctVATSetup.IsDeleted = false;
            mctVATSetup.EntryBy = 1;
            mctVATSetup.EntryDate = Convert.ToDateTime(DateTime.Now);
            mctVATSetup.UpdatedBy = 1;
            mctVATSetup.UpdatedDate = Convert.ToDateTime(DateTime.Now);

            if (ModelState.IsValid)
            {
                using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
                {
                    try
                    {
                        _unitOfWork.MctVATSetupRepo.Edit(mctVATSetup);
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

            return View(mctVATSetup);
        }

        public IActionResult Details(int Id)
        {
            MctVATSetup mctVATSetup = new MctVATSetup();
            mctVATSetup = _unitOfWork.MctVATSetupRepo.GetById(Id);
            return View(mctVATSetup);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MctVATSetup mctVATSetup = _unitOfWork.MctVATSetupRepo.GetById(id);
            if (mctVATSetup == null)
            {
                return NotFound();
            }
            return View(mctVATSetup);
        }

        public IActionResult DeleteConfirmed(int? id)
        {
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        MctVATSetup mctVATSetup = _unitOfWork.MctVATSetupRepo.GetById(id);
                        mctVATSetup.IsDeleted = true;
                        mctVATSetup.UpdatedBy = 1;
                        mctVATSetup.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.MctVATSetupRepo.Edit(mctVATSetup);
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
            MctVATSetup mctVATSetup = new MctVATSetup();
            mctVATSetup = _unitOfWork.MctVATSetupRepo.GetById(Id);
            return View(mctVATSetup);
        }

        public IActionResult ActiveDeactiveConfirmed(int? id)
        {
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        MctVATSetup mctVATSetup = _unitOfWork.MctVATSetupRepo.GetById(id);
                        mctVATSetup.IsActive = mctVATSetup.IsActive ? false : true;
                        mctVATSetup.UpdatedBy = 1;
                        mctVATSetup.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.MctVATSetupRepo.Edit(mctVATSetup);
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