using MerchantPortal.Data.Concrete;
using MerchantPortal.Data.Models;
using MerchantPortal.Helper;
using MerchantPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace MerchantPortal.Controllers
{
    public class SettlementRuleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public SettlementRuleController(IUnitOfWork unitOfWork, ILogger logger = null)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.MakeAudit(true, 1, 400);
            if (logger != null) { _logger = logger; }
        }
        public IActionResult Index()
        {
            SettlementRuleViewModel settlementRuleViewModel = new SettlementRuleViewModel();
            settlementRuleViewModel = InitializeData(settlementRuleViewModel);

            return View(settlementRuleViewModel);
        }

        private SettlementRuleViewModel InitializeData(SettlementRuleViewModel settlementRuleViewModel)
        {
            settlementRuleViewModel.Merchants = _unitOfWork.MerchantRepo.GetAll();
            settlementRuleViewModel.Terminals = _unitOfWork.TerminalRepo.GetAll();
            return settlementRuleViewModel;
        }

        [HttpPost]
        public IActionResult GetTerminalCommissionInfo(SettlementRule rule)
        {
            SettlementRuleViewModel viewModel = new SettlementRuleViewModel();
            MctCommissionSetup commission = _unitOfWork.MctCommissionSetupRepo.GetTerminalCommissionInfo(rule.TerminalId);
            if (commission != null)
                viewModel.Commission = commission.CommissionAmount;
            else
                viewModel.Commission = 0;

            return Json(new { data = viewModel });
        }

        [HttpPost]
        public IActionResult GetTerminalVATInfo(SettlementRule rule)
        {
            SettlementRuleViewModel viewModel = new SettlementRuleViewModel();
            MctVATSetup vat = _unitOfWork.MctVATSetupRepo.GetTerminalVATInfo(rule.TerminalId);
            if (vat != null)
                viewModel.VATPercentage = vat.Percentage;
            else
                viewModel.VATPercentage = 0;

            return Json(new { data = viewModel });
        }

        [HttpGet]
        public IActionResult SettlementRuleCreate()
        {
            SettlementRuleViewModel settlementRuleViewModel = new SettlementRuleViewModel();
            settlementRuleViewModel = InitializeData(settlementRuleViewModel);

            return View(settlementRuleViewModel);
        }

        public IActionResult SettlementRuleCreate(SettlementRule rule)
        {
            SettlementRuleViewModel viewModel = new SettlementRuleViewModel();
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (rule.Frequency <= 0)
                        {
                            viewModel.MessageText = Notification.Show("Frequency must be grater than 0", "Info", type: ToastType.Info);
                            viewModel = InitializeData(viewModel);
                            return View(viewModel);
                        }
                        int ruleCount = 0;
                        ruleCount = _unitOfWork.SettlementRuleRepo.GetRuleCount();
                        ruleCount = ruleCount == 0 ? 1 : ruleCount + 1;
                        rule.SettlementRuleId = "SET-" + ruleCount.ToString().PadLeft(5, '0');
                        rule.Commission = rule.CommissionEnable ? rule.Commission : null;
                        rule.VATPercentage = rule.VATEnable ? rule.VATPercentage : null;
                        rule.IsActive = false;
                        rule.IsApprove = false;
                        rule.IsDeleted = false;
                        rule.EntryBy = 1;
                        rule.EntryDate = Convert.ToDateTime(DateTime.Now);
                        rule.UpdatedBy = 1;
                        rule.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.SettlementRuleRepo.Add(rule);
                        _unitOfWork.Save();
                        transaction.Commit();
                        viewModel.MessageText = Notification.Show(CustomMessage.SaveMessage("Settlement Rule"), "Success", type: ToastType.Success);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        viewModel.MessageText = Notification.Show(CustomMessage.SaveErrorMessage("Settlement Rule"), "Error", type: ToastType.Error);
                    }
                }
                if (!ModelState.IsValid)
                    viewModel.MessageText = Notification.Show("Form data is not valid", "Error", type: ToastType.Error);

                viewModel = InitializeData(viewModel);
                return View(viewModel);
            }
        }

        public IActionResult RuleList(SettlementRule rule)
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetData(SettlementRule rule)
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

            recordsTotal = (from t in _unitOfWork.SettlementRuleRepo.GetAll()
                            select t).Count();

            var data = (from t in _unitOfWork.SettlementRuleRepo.GetSettlementRuleData()
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
                                         (t.TerminalName != null && t.TerminalName.ToLower().Contains(searchValue.ToLower())) ||
                                         (t.SettlementRuleId != null && t.SettlementRuleId.ToLower().Contains(searchValue.ToLower())) ||
                                         (t.SettlementType != null && t.SettlementType.ToLower().Contains(searchValue.ToLower())) ||
                                         (t.Frequency.Equals(searchValue.ToLower())) ||
                                         (t.Commission != null && t.Commission.Equals(searchValue.ToLower())) ||
                                         (t.VATPercentage != null && t.VATPercentage.Equals(searchValue.ToLower()))
                                    );
            }
            //var data = merchantData.Skip(skip).Take(pageSize);
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }

        [HttpGet]
        public IActionResult Edit(Int64? id)
        {
            SettlementRuleViewModel viewModel = new SettlementRuleViewModel();
            if (id != null)
            {
                SettlementRule _rule = _unitOfWork.SettlementRuleRepo.GetById(id);
                if (_rule != null)
                {
                    viewModel = ModelAdapter.ModelMap(viewModel, _rule);
                }
                else
                {
                    viewModel.MessageText = Notification.Show("Rule not found", "Error", type: ToastType.Error);
                    return RedirectToAction(nameof(RuleList));
                }
            }
            viewModel = InitializeData(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(Int64 Id, SettlementRuleViewModel rule)
        {
            SettlementRuleViewModel viewModel = new SettlementRuleViewModel();
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (rule.Frequency <= 0)
                        {
                            viewModel.MessageText = Notification.Show("Frequency must be grater than 0", "Info", type: ToastType.Info);
                            viewModel = InitializeData(viewModel);
                            return View(viewModel);
                        }
                        SettlementRule oldRule = _unitOfWork.SettlementRuleRepo.GetById(Id);
                        oldRule.MerchantId = rule.MerchantId;
                        oldRule.TerminalId = rule.TerminalId;
                        oldRule.Description = rule.Description;
                        oldRule.SettlementType = rule.SettlementType;
                        oldRule.Frequency = rule.Frequency;
                        oldRule.CommissionEnable = rule.CommissionEnable;
                        oldRule.Commission = rule.CommissionEnable ? rule.Commission : null;
                        oldRule.VATEnable = rule.VATEnable;
                        oldRule.VATPercentage = rule.VATEnable ? rule.VATPercentage : null;
                        oldRule.UpdatedBy = 1;
                        oldRule.UpdatedDate = Convert.ToDateTime(DateTime.Now);
                        _unitOfWork.SettlementRuleRepo.Edit(oldRule);
                        _unitOfWork.Save();
                        transaction.Commit();
                        viewModel.MessageText = Notification.Show(CustomMessage.UpdateMessage("Settlement Rule"), "Success", type: ToastType.Success);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        viewModel.MessageText = Notification.Show(CustomMessage.UpdateErrorMessage("Settlement Rule"), "Failed", type: ToastType.Error);
                    }
                }
            }
            viewModel = InitializeData(viewModel);
            return View(viewModel);
        }

        public IActionResult Details(Int64 Id)
        {
            SettlementRuleViewModel viewModel = new SettlementRuleViewModel();
            SettlementRule _SettlementRule = _unitOfWork.SettlementRuleRepo.GetById(Id);
            if (_SettlementRule != null)
            {
                viewModel = ModelAdapter.ModelMap(viewModel, _SettlementRule);
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(Int64? id)
        {
            SettlementRuleViewModel viewModel = new SettlementRuleViewModel();
            SettlementRule _SettlementRule = _unitOfWork.SettlementRuleRepo.GetById(id);
            if (_SettlementRule != null)
            {
                viewModel = ModelAdapter.ModelMap(viewModel, _SettlementRule);
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(Int64? Id)
        {
            SettlementRuleViewModel viewModel = new SettlementRuleViewModel();
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        SettlementRule _SettlementRule = _unitOfWork.SettlementRuleRepo.GetById(Id);
                        if (_SettlementRule != null)
                        {
                            _SettlementRule.IsDeleted = true;
                            _SettlementRule.UpdatedBy = 1;
                            _SettlementRule.UpdatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                            _unitOfWork.SettlementRuleRepo.Edit(_SettlementRule);
                            _unitOfWork.Save();
                            transaction.Commit();
                            viewModel.MessageText = Notification.Show(CustomMessage.DeleteMessage("Settlement Rule"), "Success", type: ToastType.Success);
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        viewModel.MessageText = Notification.Show(CustomMessage.UpdateErrorMessage("Settlement Rule"), "Failed", type: ToastType.Error);
                    }
                }
            }
            return View("RuleList", viewModel);
        }

        [HttpGet]
        public IActionResult Active(Int64 Id)
        {
            SettlementRuleViewModel viewModel = new SettlementRuleViewModel();
            SettlementRule _SettlementRule = _unitOfWork.SettlementRuleRepo.GetById(Id);
            if (_SettlementRule != null)
            {
                viewModel = ModelAdapter.ModelMap(viewModel, _SettlementRule);
            }
            return View(viewModel);
        }

        public IActionResult ActiveDeactiveConfirm(Int64? id)
        {
            SettlementRuleViewModel viewModel = new SettlementRuleViewModel();
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        SettlementRule _SettlementRule = _unitOfWork.SettlementRuleRepo.GetById(id);
                        if (_SettlementRule != null)
                        {
                            _SettlementRule.IsActive = _SettlementRule.IsActive ? false : true;
                            _SettlementRule.UpdatedBy = 1;
                            _SettlementRule.UpdatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                            _unitOfWork.SettlementRuleRepo.Edit(_SettlementRule);
                            _unitOfWork.Save();
                            transaction.Commit();
                            viewModel.MessageText = Notification.Show("Record updated successfully", "Success", type: ToastType.Success);
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        viewModel.MessageText = Notification.Show("Record failed to update", "Failed", type: ToastType.Error);
                    }
                }
            }
            return View("RuleList", viewModel);
        }

        public IActionResult RuleListApprove(SettlementRule rule)
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDataApproved(SettlementRule rule)
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

            recordsTotal = (from t in _unitOfWork.SettlementRuleRepo.GetAll()
                            select t).Where(w => w.IsApprove == false).Count();

            var data = (from t in _unitOfWork.SettlementRuleRepo.GetSettlementRuleData()
                        select t).Where(w => w.IsApprove == false).Skip(skip).Take(pageSize);

            ////Sorting   .Skip(skip).Take(pageSize)
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                data = data.AsQueryable().OrderBy(sortColumn + " " + sortColumnDirection);
            }
            //Search
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(t => (t.MerchantName != null && t.MerchantName.ToLower().Contains(searchValue.ToLower())) ||
                                         (t.TerminalName != null && t.TerminalName.ToLower().Contains(searchValue.ToLower())) ||
                                         (t.SettlementRuleId != null && t.SettlementRuleId.ToLower().Contains(searchValue.ToLower())) ||
                                         (t.SettlementType != null && t.SettlementType.ToLower().Contains(searchValue.ToLower())) ||
                                         (t.Frequency.Equals(searchValue.ToLower())) ||
                                         (t.Commission != null && t.Commission.Equals(searchValue.ToLower())) ||
                                         (t.VATPercentage != null && t.VATPercentage.Equals(searchValue.ToLower()))
                                    );
            }
            //var data = merchantData.Skip(skip).Take(pageSize);
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }

        [HttpGet]
        public IActionResult Approve(Int64 Id)
        {
            SettlementRuleViewModel viewModel = new SettlementRuleViewModel();
            SettlementRule _SettlementRule = _unitOfWork.SettlementRuleRepo.GetById(Id);
            if (_SettlementRule != null)
            {
                viewModel = ModelAdapter.ModelMap(viewModel, _SettlementRule);
            }
            return View(viewModel);
        }

        public IActionResult ApproveConfirm(Int64? id)
        {
            SettlementRuleViewModel viewModel = new SettlementRuleViewModel();
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        SettlementRule _SettlementRule = _unitOfWork.SettlementRuleRepo.GetById(id);
                        if (_SettlementRule != null)
                        {
                            _SettlementRule.IsApprove = true; //_SettlementRule.IsActive ? false : true;
                            _SettlementRule.UpdatedBy = 1;
                            _SettlementRule.UpdatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                            _unitOfWork.SettlementRuleRepo.Edit(_SettlementRule);
                            _unitOfWork.Save();
                            transaction.Commit();
                            viewModel.MessageText = Notification.Show("Record updated successfully", "Success", type: ToastType.Success);
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        viewModel.MessageText = Notification.Show("Record failed to update", "Failed", type: ToastType.Error);
                    }
                }
            }
            return View("RuleListApprove", viewModel);
        }


    }
}