using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MerchantPortal.Data.Concrete;
using MerchantPortal.Data.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using MerchantPortal.Models;
using System.Linq.Dynamic.Core;


namespace MerchantPortal.Controllers
{
    public class ApprovalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public ApprovalController(IUnitOfWork unitOfWork, ILogger logger = null)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.MakeAudit(true, 1, 400);
            if (logger != null) { _logger = logger; }
        }


        //public IActionResult Index(int? id)
        //{
        //    Bank _bank = null;
        //    ViewData["CountryId"] = new SelectList(_unitOfWork.CountryRepo.GetAll().Where(c => (c.IsDeleted == false)), "Id", "StrCountryName");
        //    ViewData["Operation"] = id;
        //    ViewData["Bank"] = _unitOfWork.BankRepo.GetAll().ToList();
        //    if (id != null)
        //    {
        //        _bank = _unitOfWork.BankRepo.GetById(id);
        //    }
        //    return View(_bank);
        //}

        public IActionResult Index(int? id)
        {


            var str = new SelectList(_unitOfWork.MerchantRepo.GetAll().Where(c => (c.IsActive == true)).ToList(), "Id", "MerchantName");
            ViewBag.Locations = str;


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

            recordsTotal = (from m in getDataApproval()
                            select m).Count();

            var data = (from m in getDataApproval()
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
                                         m.MerchantName.ToLower().Contains(searchValue.ToLower()) ||
                                         m.OrgName.ToLower().Contains(searchValue.ToLower()) ||
                                         m.GLNumber.ToLower().Contains(searchValue.ToLower())
                                    );
            }
            //var data = merchantData.Skip(skip).Take(pageSize);
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }



        private IEnumerable<ApprovalViewModel> getDataApproval()
        {
            //List<Merchant> mList = new List<Merchant>();
            var mList = _unitOfWork.MerchantRepo.GetAll().ToList();
            var tList = _unitOfWork.TerminalRepo.GetAll().ToList();
            var cList = _unitOfWork.MctCommissionSetupRepo.GetAll().ToList();
            var vatList = _unitOfWork.MctVATSetupRepo.GetAll().ToList();
            var glList = _unitOfWork.MctGLSetupRepo.GetAll().ToList();

            var data = (from m in mList
                        join t in tList
                        on m.Id equals t.MerchantId
                        join c in cList on t.Id equals c.TerminalId into commissionlist
                        join vat in vatList on t.Id equals vat.TerminalId into vatlist
                        join gl in glList on t.Id equals gl.TerminalId into gllist
                        from com in commissionlist.DefaultIfEmpty()
                        from vat in vatlist.DefaultIfEmpty()
                        from gl in gllist.DefaultIfEmpty()
                        select new Data.Models.ApprovalViewModel
                        {
                            Id = m.Id,
                            MerchantName = m.MerchantName,
                            Id2 = t.Id,
                            OrgName = t.OrgName,
                            CommissionId = com == null ? 0 : com.Id,
                            CommissionAmount = (com == null ? 0 : com.CommissionAmount),
                            BankPercentage = (com == null ? 0 : com.BankPercentage),
                            GLNumber = gl == null ? "0" : gl.GLNumber,
                            AccountNo = gl == null ? "0" : gl.AccountNo,
                            VATRegNo = vat == null ? "0" : vat.VATRegNo,
                            IsApprove = m.IsApprove
                            //IsActive=m.IsActive,

                            //CommissionId=com.Id
                            //VATId=vat.ID,
                            //GLId=gl.ID,
                            //GLNumber=gl.GLNumber,
                            //AccountNo=gl.AccountNo
                        }).ToList();
            return (IEnumerable<ApprovalViewModel>)data;
        }

        public IActionResult ApproverList()
        {
            return View();
        }

        public IActionResult Approve(Int64 Id)
        {
            Data.Models.ApprovalViewModel objapp = new Data.Models.ApprovalViewModel();
            var mList = _unitOfWork.MerchantRepo.GetById(Id);
            var tList = _unitOfWork.TerminalRepo.GetById(Id);
            var cList = _unitOfWork.MctCommissionSetupRepo.GetById(Id);
            var vatList = _unitOfWork.MctVATSetupRepo.GetById(Id);
            var glList = _unitOfWork.MctGLSetupRepo.GetById(Id);

            objapp.Id2 = tList.Id;
            objapp.MerchantName = mList.MerchantName == null ? "0" : mList.MerchantName;
            objapp.OrgName = tList.OrgName == null ? "0" : tList.OrgName;
            objapp.CommissionAmount = (cList == null ? 0 : cList.CommissionAmount); //cList.CommissionAmount;
            objapp.BankPercentage = (cList == null ? 0 : cList.BankPercentage);//cList.BankPercentage;
            objapp.GLNumber = (glList == null ? "0" : glList.GLNumber);//glList.GLNumber;
            objapp.AccountNo = (glList == null ? "0" : glList.AccountNo);//glList.AccountNo;
            objapp.VATRegNo = (vatList == null ? "0" : vatList.VATRegNo); //vatList.VATRegNo;

            return View(objapp);
        }

        public IActionResult ApproveConfirm(Int64? Id2)
        {
            if (ModelState.IsValid)
            {
                Data.Models.ApprovalViewModel objapp = new Data.Models.ApprovalViewModel();
                var mList = _unitOfWork.ApprovalViewrepo.ApproveMerchants(Id2);
            }
            return RedirectToAction(nameof(ApproverList));
        }

        public IActionResult Details(Int64 Id)
        {
            //Id is the terminal id
            Data.Models.ApprovalViewModel objapp = new Data.Models.ApprovalViewModel();

            if (Id > 0) {
                objapp.Terminal = _unitOfWork.TerminalRepo.GetById(Id);
                if (objapp.Terminal != null)
                {
                    objapp.Merchant = _unitOfWork.MerchantRepo.GetById(objapp.Terminal.MerchantId);
                    objapp.MctCommissionSetup = _unitOfWork.MctCommissionSetupRepo.GetById(objapp.Terminal.Id);
                    objapp.MctVATSetup = _unitOfWork.MctVATSetupRepo.GetById(objapp.Terminal.Id);
                    objapp.MctGLSetup = _unitOfWork.MctGLSetupRepo.GetById(objapp.Terminal.Id);
                }
            }
            
            return View(objapp);
        }
    }
}