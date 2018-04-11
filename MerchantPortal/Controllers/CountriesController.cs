using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data;
using MerchantPortal.Data.Models;
using Microsoft.Extensions.Logging;
using MerchantPortal.Data.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace MerchantPortal.Controllers
{
    /// <summary>
    /// Modified by : Maksud 
    /// Date         : 29-Jan-2018.
    /// Description  : Repository for Branch.
    /// </summary>
    /// 

    [Authorize]
    public class CountriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public CountriesController(IUnitOfWork unitOfWork, ILogger logger = null)
        {
            _unitOfWork = unitOfWork;

            if (logger != null) { _logger = logger; }
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Country.ToListAsync());
            return View(_unitOfWork.CountryRepo.GetAll().ToList());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            Country _country = _unitOfWork.CountryRepo.GetById(id);

            if (_country == null)
            {
                return NotFound();
            }

            return View(_country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumCountryCode,StrCountryName,StrRemarks,ISOCode,BlackListed,StrISOAlpha3,NumISO,Active,IsDeleted")] Country country)
        {
            if (ModelState.IsValid)
            {
                country.UpdatedBy = 1;
                country.UpdateDate = DateTime.Now;
                _unitOfWork.CountryRepo.Add(country);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Country _country = _unitOfWork.CountryRepo.GetById(id);

            if (_country == null)
            {
                return NotFound();
            }
            return View(_country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumCountryCode,StrCountryName,StrRemarks,ISOCode,BlackListed,StrISOAlpha3,NumISO,Active,IsDeleted")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    country.UpdatedBy = 1;
                    country.UpdateDate = DateTime.Now;
                    _unitOfWork.CountryRepo.Edit(country.Id, country);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
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
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Country _country = _unitOfWork.CountryRepo.GetById(id);

            if (_country == null)
            {
                return NotFound();
            }

            return View(_country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Country _country = _unitOfWork.CountryRepo.GetById(id);
            _country.UpdatedBy = 1;
            _country.UpdateDate = DateTime.Now;
            _country.IsDeleted = true;
            _unitOfWork.CountryRepo.Delete(_country);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return _unitOfWork.CountryRepo.GetAll().Any(e => e.Id == id);
        }
    }
}
