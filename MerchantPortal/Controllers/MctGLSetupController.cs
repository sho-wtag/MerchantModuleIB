using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MerchantPortal.Data.Concrete;
using MerchantPortal.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace MerchantPortal.Controllers
{
    public class MctGLSetupController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public MctGLSetupController(IUnitOfWork unitOfWork, ILogger logger = null)
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

            recordsTotal = (from m in _unitOfWork.MctGLSetupRepo.GetAll()
                            select m).Count();

            var data = (from m in _unitOfWork.MctGLSetupRepo.GetAll()
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

        [HttpPost]
        public IActionResult Create([Bind("MerchantId,TerminalId,GLNumber,AccountNo,AccountName,Descriptions,IsActive")] MctGLSetup mctGLSetup)
        {
            mctGLSetup.IsApprove = false;
            mctGLSetup.IsDeleted = false;
            mctGLSetup.EntryBy = 1;
            mctGLSetup.EntryDate = Convert.ToDateTime(DateTime.Now);
            mctGLSetup.UpdatedBy = 1;
            mctGLSetup.UpdatedDate = Convert.ToDateTime(DateTime.Now);

            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _unitOfWork.MctGLSetupRepo.Add(mctGLSetup);
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
            MctGLSetup mctGLSetup = null;
            if (id != null)
            {
                mctGLSetup = _unitOfWork.MctGLSetupRepo.GetById(id);
            }
            return View(mctGLSetup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,MerchantId,TerminalId,GLNumber,AccountNo,AccountName,Descriptions,IsActive")] MctGLSetup mctGLSetup)
        {
            if (id != mctGLSetup.ID)
            {
                return NotFound();
            }

            mctGLSetup.IsApprove = false;
            mctGLSetup.IsDeleted = false;
            mctGLSetup.EntryBy = 1;
            mctGLSetup.EntryDate = Convert.ToDateTime(DateTime.Now);
            mctGLSetup.UpdatedBy = 1;
            mctGLSetup.UpdatedDate = Convert.ToDateTime(DateTime.Now);

            if (ModelState.IsValid)
            {
                using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
                {
                    try
                    {
                        _unitOfWork.MctGLSetupRepo.Edit(mctGLSetup);
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

            return View(mctGLSetup);
        }

        public IActionResult Details(int Id)
        {
            MctGLSetup mctGLSetup = new MctGLSetup();
            mctGLSetup = _unitOfWork.MctGLSetupRepo.GetById(Id);
            return View(mctGLSetup);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MctGLSetup mctGLSetup = _unitOfWork.MctGLSetupRepo.GetById(id);
            if (mctGLSetup == null)
            {
                return NotFound();
            }
            return View(mctGLSetup);
        }

        public IActionResult DeleteConfirmed(int? id)
        {
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        MctGLSetup mctGLSetup = _unitOfWork.MctGLSetupRepo.GetById(id);
                        mctGLSetup.IsDeleted = true;
                        mctGLSetup.UpdatedBy = 1;
                        mctGLSetup.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.MctGLSetupRepo.Edit(mctGLSetup);
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
            MctGLSetup mctGLSetup = new MctGLSetup();
            mctGLSetup = _unitOfWork.MctGLSetupRepo.GetById(Id);
            return View(mctGLSetup);
        }

        public IActionResult ActiveDeactiveConfirmed(int? id)
        {
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        MctGLSetup mctGLSetup = _unitOfWork.MctGLSetupRepo.GetById(id);
                        mctGLSetup.IsActive = mctGLSetup.IsActive ? false : true;
                        mctGLSetup.UpdatedBy = 1;
                        mctGLSetup.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.MctGLSetupRepo.Edit(mctGLSetup);
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