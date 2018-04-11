using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MerchantPortal.Data.Concrete;
using MerchantPortal.Data.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using System;
using MerchantPortal.Helper;
using MerchantPortal.Models;
using System.Collections.Generic;

namespace MerchantPortal.Controllers
{
    /// <summary>
    /// Modified by : Maksud 
    /// Date         : 29-Jan-2018.
    /// Description  : Repository for Branch.
    /// </summary>
    /// 
    //[Authorize]
    public class BankController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public BankController(IUnitOfWork unitOfWork, ILogger logger = null)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.MakeAudit(true, 1, 400);
            if (logger != null) { _logger = logger; }
        }

        // GET: Bank
        //[AppAuthorize(Action="index")]
        public IActionResult Index(int? id)
        {
           // Bank _bank = null;
            BankViewModel _bank = new BankViewModel();

            _bank.MessageText = Notification.Show("Successfully Saved", "Success", type: ToastType.Success);

            //ViewData["CountryId"] = new SelectList(_unitOfWork.CountryRepo.GetAll().Where(c => (c.IsDeleted == false)), "Id", "StrCountryName");
            //ViewData["Operation"] = id;


            List<BankViewModel> lBankViewModel = new List<BankViewModel>();

            lBankViewModel = ModelAdapter.ModelMap(lBankViewModel, _unitOfWork.BankRepo.GetAll().ToList());

            ViewData["Bank"] = lBankViewModel;

            if (id != null)
            {
                _bank = (BankViewModel)_unitOfWork.BankRepo.GetById(id);
            }
            return View(_bank);
        }

        // GET: Bank/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Bank bank = await _context.Bank
            //    .Include(b => b.Country)
            //    .SingleOrDefaultAsync(m => m.Id == id);
            Bank _bank = _unitOfWork.BankRepo.GetById(id);
            if (_bank == null)
            {
                return NotFound();
            }
            ViewData["users"] = _unitOfWork.BankRepo.GetAll().ToList();
            return View(_bank);
        }

        // GET: Bank/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_unitOfWork.CountryRepo.GetAll().Where(c => (c.IsDeleted == false)), "Id", "StrCountryName");
            return View();
        }

        // POST: Bank/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SName,Code,SwiftCode,HeadOfficeAddress,ContactNumber,EmailAddress,Description,Active,CountryId")] Bank bank)
        {
            using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _unitOfWork.BankRepo.Add(bank);
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
            }
            ViewData["CountryId"] = new SelectList(_unitOfWork.CountryRepo.GetAll().Where(c => (c.IsDeleted == false)), "Id", "StrCountryName", bank.CountryId);
            return View(bank);
        }

        // GET: Bank/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*var bank = await _context.Bank.SingleOrDefaultAsync(m => m.Id == id);*/
            Bank _bank = _unitOfWork.BankRepo.GetById(id);
            if (_bank == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_unitOfWork.CountryRepo.GetAll().Where(c => (c.IsDeleted == false)), "Id", "StrCountryName", _bank.CountryId);
            return View(_bank);
        }

        // POST: Bank/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SName,Code,SwiftCode,HeadOfficeAddress,ContactNumber,EmailAddress,Description,Active,CountryId")] Bank bank)
        {
            if (id != bank.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var transaction = _unitOfWork.MTDBContext.Database.BeginTransaction())
                    {
                        try
                        {
                            _unitOfWork.BankRepo.Edit(bank.Id, bank);
                            _unitOfWork.Save();
                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankExists(bank.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_unitOfWork.CountryRepo.GetAll().Where(c => (c.IsDeleted == false)), "Id", "StrCountryName", bank.CountryId);
            return View(bank);
        }

        // GET: Bank/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var bank = await _context.Bank
            //    .Include(b => b.Country)
            //    .SingleOrDefaultAsync(m => m.Id == id);
            var _bank = _unitOfWork.BankRepo.GetById(id);
            if (_bank == null)
            {
                return NotFound();
            }

            return View(_bank);
        }

        // POST: Bank/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Bank _bank = _unitOfWork.BankRepo.GetById(id);
            _bank.IsDeleted = true;
            _unitOfWork.BankRepo.Delete(_bank);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        [ActionName("IsBankExist")]
        private bool BankExists(int id)
        {
            return _unitOfWork.BankRepo.GetAll().Any(e => e.Id == id);
        }
    }
}
