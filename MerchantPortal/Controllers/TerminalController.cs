using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MerchantPortal.Data.Concrete;
using MerchantPortal.Data.Models;
using MerchantPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace MerchantPortal.Controllers
{
    public class TerminalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public TerminalController(IUnitOfWork unitOfWork, ILogger logger = null)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.MakeAudit(true, 1, 400);
            if (logger != null) { _logger = logger; }
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TerminalCreate(Terminal terminal)
        {
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        terminal.IsActive = false;
                        terminal.IsApprove = false;
                        terminal.IsDeleted = false;
                        terminal.EntryBy = 1;
                        terminal.EntryDate = Convert.ToDateTime(DateTime.Now);
                        terminal.UpdatedBy = 1;
                        terminal.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.TerminalRepo.Add(terminal);
                        _unitOfWork.Save();
                        transaction.Commit();
                        TempData["msg"] = "<script>alert('Saved succesfully');</script>";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }

                    return RedirectToAction("Index", "Merchant");
                }
            }
            return View("Index");
        }

        public IActionResult Edit(Int64? id)
        {
            TerminalViewModel terminalViewModel = new TerminalViewModel();
            terminalViewModel.Merchants = _unitOfWork.MerchantRepo.GetAll();

            Terminal _terminal = null;
            if (id != null)
            {
                _terminal = _unitOfWork.TerminalRepo.GetById(id);
                terminalViewModel.Id = _terminal.Id;
                terminalViewModel.MerchantId = _terminal.MerchantId;
                terminalViewModel.OrgName = _terminal.OrgName;
                terminalViewModel.OwnerName = _terminal.OwnerName;
                terminalViewModel.ContactAddess = _terminal.PhoneNo;
                terminalViewModel.PhoneNo = _terminal.PhoneNo;
                terminalViewModel.FaxNo = _terminal.FaxNo;
                terminalViewModel.EmailId = _terminal.EmailId;
                terminalViewModel.ContactPerson = _terminal.ContactPerson;
                terminalViewModel.ContactPersonPhone = _terminal.ContactPersonPhone;
                terminalViewModel.ContactPersonAddress = _terminal.ContactPersonAddress;
                terminalViewModel.ContactPersonEmailId = _terminal.ContactPersonEmailId;
                terminalViewModel.TradeLicenseNo = _terminal.TradeLicenseNo;
                terminalViewModel.VATRegistrationNo = _terminal.VATRegistrationNo;
                terminalViewModel.EntryBy = _terminal.EntryBy;
                terminalViewModel.EntryDate = _terminal.EntryDate;
                terminalViewModel.IsDeleted = _terminal.IsDeleted;
            }
            return View(terminalViewModel);
        }

        public IActionResult EditTerminal(Int64 Id, Terminal terminal)
        {
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        terminal.Id = Id;
                        terminal.UpdatedBy = 1;
                        terminal.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.TerminalRepo.Edit(terminal);
                        _unitOfWork.Save();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return RedirectToAction(nameof(TerminalList));
        }

        public IActionResult TerminalList(Terminal terminal)
        {
            /*IEnumerable<Terminal> lstData = _unitOfWork.TerminalRepo.GetTerminalData();
            List<TerminalViewModel> lstModel = new List<TerminalViewModel>();
            
            foreach (var item in lstData)
            {
                TerminalViewModel model = new TerminalViewModel();
                model.Id = item.Id;
                model.MerchantId = item.MerchantId;
                model.MerchantName = item.Merchant.MerchantName;
                model.OrgName = item.OrgName;
                model.OwnerName = item.OwnerName;
                model.ContactAddess = item.ContactAddess;
                model.PhoneNo = item.PhoneNo;
                model.EmailId = item.EmailId;
                lstModel.Add(model);
            }

            return View(lstModel.OrderBy(o => o.MerchantName).ThenBy(t => t.OrgName));*/
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

            recordsTotal = (from t in _unitOfWork.TerminalRepo.GetAll()
                            select t).Count();

            var data = (from t in _unitOfWork.TerminalRepo.GetTerminalData()
                        select t).Skip(skip).Take(pageSize);

            ////Sorting   .Skip(skip).Take(pageSize)
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                data = data.AsQueryable().OrderBy(sortColumn + " " + sortColumnDirection);
            }
            //Search
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(t => (t.MerchantName != null && t.MerchantName.ToLower().Contains(searchValue.ToLower())) ||
                                         (t.OrgName != null && t.OrgName.ToLower().Contains(searchValue.ToLower())) ||
                                         (t.OwnerName != null && t.OwnerName.ToLower().Contains(searchValue.ToLower())) ||
                                         (t.ContactAddess != null && t.ContactAddess.ToLower().Contains(searchValue.ToLower())) ||
                                         (t.PhoneNo != null && t.PhoneNo.ToLower().Contains(searchValue.ToLower())) ||
                                         (t.EmailId != null && t.EmailId.ToLower().Contains(searchValue.ToLower()))
                                    );
            }
            //var data = merchantData.Skip(skip).Take(pageSize);
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }

        public IActionResult Details(Int64 Id)
        {
            TerminalViewModel terminalViewModel = new TerminalViewModel();
            Terminal _terminal = _unitOfWork.TerminalRepo.GetById(Id);
            if (_terminal != null)
            {
                terminalViewModel = ObjectToform(_terminal);
                terminalViewModel.MerchantName = _unitOfWork.MerchantRepo.GetById(_terminal.MerchantId).MerchantName;
            }
            return View(terminalViewModel);
        }

        private TerminalViewModel ObjectToform(Terminal _terminal)
        {
            TerminalViewModel terminalViewModel = new TerminalViewModel();
            terminalViewModel.Id = _terminal.Id;
            terminalViewModel.MerchantId = _terminal.MerchantId;
            terminalViewModel.OrgName = _terminal.OrgName;
            terminalViewModel.OwnerName = _terminal.OwnerName;
            terminalViewModel.ContactAddess = _terminal.PhoneNo;
            terminalViewModel.PhoneNo = _terminal.PhoneNo;
            terminalViewModel.FaxNo = _terminal.FaxNo;
            terminalViewModel.EmailId = _terminal.EmailId;
            terminalViewModel.ContactPerson = _terminal.ContactPerson;
            terminalViewModel.ContactPersonPhone = _terminal.ContactPersonPhone;
            terminalViewModel.ContactPersonAddress = _terminal.ContactPersonAddress;
            terminalViewModel.ContactPersonEmailId = _terminal.ContactPersonEmailId;
            terminalViewModel.TradeLicenseNo = _terminal.TradeLicenseNo;
            terminalViewModel.VATRegistrationNo = _terminal.VATRegistrationNo;
            terminalViewModel.IsActive = _terminal.IsActive;
            terminalViewModel.EntryBy = _terminal.EntryBy;
            terminalViewModel.EntryDate = _terminal.EntryDate;
            terminalViewModel.IsDeleted = _terminal.IsDeleted;

            return terminalViewModel;
        }

        public IActionResult Delete(Int64? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TerminalViewModel terminalViewModel = new TerminalViewModel();
            Terminal _terminal = _unitOfWork.TerminalRepo.GetById(id);
            if (_terminal != null)
            {
                terminalViewModel = ObjectToform(_terminal);
                terminalViewModel.MerchantName = _unitOfWork.MerchantRepo.GetById(_terminal.MerchantId).MerchantName;
            }
            return View(terminalViewModel);
        }

        public IActionResult DeleteConfirm(Int64? Id)
        {
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Terminal _Terminal = _unitOfWork.TerminalRepo.GetById(Id);
                        _Terminal.IsDeleted = true;
                        _Terminal.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.TerminalRepo.Edit(_Terminal);
                        _unitOfWork.Save();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return RedirectToAction(nameof(TerminalList));
        }

        public IActionResult Active(Int64 Id)
        {
            TerminalViewModel terminalViewModel = new TerminalViewModel();
            Terminal _terminal = _unitOfWork.TerminalRepo.GetById(Id);
            if (_terminal != null)
            {
                terminalViewModel = ObjectToform(_terminal);
                terminalViewModel.MerchantName = _unitOfWork.MerchantRepo.GetById(_terminal.MerchantId).MerchantName;
            }
            return View(terminalViewModel);
        }

        public IActionResult ActiveDeactiveConfirm(Int64? id)
        {
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Terminal _Terminal = _unitOfWork.TerminalRepo.GetById(id);
                        _Terminal.IsActive = _Terminal.IsActive ? false : true;
                        _Terminal.UpdatedBy = 1;
                        _Terminal.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.TerminalRepo.Edit(_Terminal);
                        _unitOfWork.Save();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return RedirectToAction(nameof(TerminalList));
        }

    }
}