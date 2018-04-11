using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MerchantPortal.Data.Concrete;
using MerchantPortal.Data.Models;
using MerchantPortal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;
using MerchantPortal.Helper;

namespace MerchantPortal.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public TransactionController(IUnitOfWork unitOfWork, ILogger logger = null)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.MakeAudit(true, 1, 400);
            if (logger != null) { _logger = logger; }
        }
        [HttpGet]
        public IActionResult Index()
        {
            TransactionViewModel model = new TransactionViewModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult TransactionList(Transaction tran)
        {
            TransactionViewModel model = new TransactionViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult TransactionList()
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

            recordsTotal = (from t in _unitOfWork.MerchantTransactionRepo.GetMerchantTransactionByStatus("Complete")
                            select t).Count();

            var data = (from t in _unitOfWork.MerchantTransactionRepo.GetMerchantTransactionByStatus("Complete")
                        select t).Skip(skip).Take(pageSize);

            ////Sorting   .Skip(skip).Take(pageSize)
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            //{
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
            {
                data = data.AsQueryable().OrderBy(sortColumn + " " + sortColumnDirection);
            }
            //Search
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(t => (t.MerchantName != null && t.MerchantName.ToLower().Contains(searchValue.ToLower())) ||
                                         (t.TerminalName != null && t.TerminalName.ToLower().Contains(searchValue.ToLower())) ||
                                         (t.PrincipalAmount != null && t.PrincipalAmount.Equals(searchValue.ToLower())) ||
                                         (t.ComissionAmount != null && t.ComissionAmount.Equals(searchValue.ToLower())) ||
                                         (t.VatAmount != null && t.VatAmount.Equals(searchValue.ToLower()))
                                    );
            }
            //var data = merchantData.Skip(skip).Take(pageSize);
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
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

            recordsTotal = (from t in _unitOfWork.MerchantTransactionRepo.GetMerchantTransactions()
                            select t).Count();

            var data = (from t in _unitOfWork.MerchantTransactionRepo.GetMerchantTransactions()
                        select t).Skip(skip).Take(pageSize);

            ////Sorting   .Skip(skip).Take(pageSize)
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                data = data.AsQueryable().OrderBy(sortColumn + " " + sortColumnDirection);
            }
            //Search
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(t => (
                                        t.MerchantName.ToLower().Contains(searchValue.ToLower()) ||
                                         t.TerminalName.ToLower().Contains(searchValue.ToLower())
                                        ));
            }
            //var data = merchantData.Skip(skip).Take(pageSize);
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }

        public IActionResult Details(Int64 Id)
        {
            TransactionViewModel viewModel = new TransactionViewModel();
            if (Id > 0)
            {
                Transaction transaction = _unitOfWork.MerchantTransactionRepo.GetById(Id);
                if (transaction != null)
                {
                    viewModel = ModelAdapter.ModelMap(viewModel, transaction);
                }
            }

            return View(viewModel);
        }

        public IActionResult TransactionDetail(Int64 Id)
        {
            TransactionViewModel viewModel = new TransactionViewModel();
            if (Id > 0)
            {
                Transaction transaction = _unitOfWork.MerchantTransactionRepo.GetById(Id);
                if (transaction != null)
                {
                    viewModel = ModelAdapter.ModelMap(viewModel, transaction);
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Settle(Int64 Id)
        {
            TransactionViewModel viewModel = new TransactionViewModel();
            if (Id > 0)
            {
                Transaction transaction = _unitOfWork.MerchantTransactionRepo.GetById(Id);
                if (transaction != null)
                {
                    viewModel = ModelAdapter.ModelMap(viewModel, transaction);
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Settle(Int64 Id, TransactionViewModel viewModel)
        {
            if (Id > 0)
            {
                if (!string.IsNullOrEmpty(viewModel.SettleStatus))
                {
                    Transaction _transaction = _unitOfWork.MerchantTransactionRepo.GetById(Id);
                    if (_transaction != null)
                    {
                        try
                        {
                            if (viewModel.SettleStatus.Equals("S"))
                            {
                                _transaction.TrnxStatusId = _unitOfWork.TransactionStatusRepo.GetByStatusCodeName("TRAN001", "Settled").Id;
                                _transaction.SettledDate = DateTime.Now;
                            }
                            else
                            {
                                _transaction.TrnxStatusId = _unitOfWork.TransactionStatusRepo.GetByStatusCodeName("TRAN001", "Reject").Id;
                                _transaction.SettledDate = null;
                            }
                            _transaction.CheckerId = 1;
                            _transaction.UpdatedBy = 1;
                            _transaction.UpdatedDate = DateTime.Now;
                            _unitOfWork.MerchantTransactionRepo.Edit(_transaction);
                            _unitOfWork.Save();
                            string msg = viewModel.SettleStatus.Equals("S") ? "Transaction settled successfully" : "Transaction rejected successfully";
                            viewModel.MessageText = Notification.Show(msg, "Success", type: ToastType.Success);
                        }
                        catch (Exception ex)
                        {
                            string msg = viewModel.SettleStatus.Equals("S") ? "Transaction settled failed" : "Transaction reject failed";
                            viewModel.MessageText = Notification.Show(msg, "Failed", type: ToastType.Error);
                        }
                    }
                }
                else
                    viewModel.MessageText = Notification.Show("Please select Settle or Reject", "Info", type: ToastType.Info);
            }
            Transaction trans = _unitOfWork.MerchantTransactionRepo.GetById(Id);
            viewModel = ModelAdapter.ModelMap(viewModel, trans);
            return View(viewModel);
        }


        [HttpGet]


        [HttpPost]
        public JsonResult BulkSettlement(List<Transaction> Transactions, string SettleStatus)
        {
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                try
                {
                    int count = 0;
                    foreach (Transaction item in Transactions)
                    {
                        Transaction _trans = _unitOfWork.MerchantTransactionRepo.GetByTransactionId(item.Id);
                        if (_trans != null)
                        {
                            if (SettleStatus.Equals("S"))
                            {
                                _trans.TrnxStatusId = _unitOfWork.TransactionStatusRepo.GetByStatusCodeName("TRAN001", "Settled").Id;
                                _trans.SettledDate = DateTime.Now;
                            }
                            else
                            {
                                _trans.TrnxStatusId = _unitOfWork.TransactionStatusRepo.GetByStatusCodeName("TRAN001", "Reject").Id;
                                _trans.SettledDate = null;
                            }
                            _trans.CheckerId = 1;
                            _trans.UpdatedBy = 1;
                            _trans.UpdatedDate = DateTime.Now;
                            _unitOfWork.MerchantTransactionRepo.Edit(_trans);
                            _unitOfWork.Save();
                            count++;
                        }
                    }
                    transaction.Commit();
                    string msg = SettleStatus.Equals("S") ? count + " transaction settled successfully" : count + " transaction rejected successfully";
                    return Json(new { success = true, message = msg });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    string msg = SettleStatus.Equals("S") ? "Transaction settled failed" : "Transaction reject failed";
                    return Json(new { success = false, message = msg });
                }
            }
        }

        [HttpGet]

        public IActionResult TransactionSearch(Nullable<DateTime> StartStamp, Nullable<DateTime> EndStamp)
        {

            //TransactionViewModel viewModel = new TransactionViewModel();
            List<TransactionViewModel> viewModel = new List<TransactionViewModel>();
            List<Transaction> transactionsearch = GetSearchResult(StartStamp, EndStamp);
            
            if (transactionsearch != null)
            {
                viewModel = ModelAdapter.ModelMap(viewModel, transactionsearch);
            }
          
            return View(viewModel);
            //return PartialView("searchResults", viewModel);
        }
        
        public PartialViewResult TransactionSearchs(Nullable<DateTime> StartStamp, Nullable<DateTime> EndStamp)
        {

            //TransactionViewModel viewModel = new TransactionViewModel();
            List<TransactionViewModel> viewModel = new List<TransactionViewModel>();
            List<Transaction> transactionsearch = GetSearchResult(StartStamp, EndStamp);

            if (transactionsearch != null)
            {
                viewModel = ModelAdapter.ModelMap(viewModel, transactionsearch);
            }

            //return View(viewModel);
            return PartialView("~/Views/_searchresults.cshtml", viewModel);
        }

        private List<Transaction> GetSearchResult(Nullable<DateTime> fromDate, Nullable<DateTime> toDate)
        {
           
            List<Transaction> transactionList = new List<Transaction>();
            if ((fromDate != null && toDate != null))
            {
                var fDate = fromDate.Value.Date;
                var tDate = toDate.Value.Date.AddTicks(-1).AddDays(1);
                transactionList = _unitOfWork.MerchantTransactionRepo.GetMerchantTransactions().Where(d => d.StartStamp >= fDate && d.EndStamp <= tDate).ToList();
            } 
            else
            {
                transactionList = _unitOfWork.MerchantTransactionRepo.GetMerchantTransactions().ToList();
            }

            return transactionList;
        }
    }
}