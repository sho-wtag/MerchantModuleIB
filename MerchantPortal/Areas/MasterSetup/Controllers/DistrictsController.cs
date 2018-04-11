using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MerchantPortal.Areas.MasterSetup.Data;
using MerchantPortal.Areas.MasterSetup.Models;
using Microsoft.AspNetCore.Authorization;

namespace MerchantPortal.Areas.MasterSetup.Controllers
{
    [Area("MasterSetup")]
    //[Authorize]
    public class DistrictsController : Controller
    {
        private readonly MSDbContext _context;

        public DistrictsController(MSDbContext context)
        {
            _context = context;
        }

        // GET: MasterSetup/MSDistricts
        public async Task<IActionResult> Index()
        {
            return View(await _context.MSDistrict.ToListAsync());
        }

        // GET: MasterSetup/MSDistricts/Details/5
        public async Task<IActionResult> Details(Int32? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mSDistrict = await _context.MSDistrict
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mSDistrict == null)
            {
                return NotFound();
            }

            return View(mSDistrict);
        }

        // GET: MasterSetup/MSDistricts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MasterSetup/MSDistricts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CountryId,Status,Remarks")] District mSDistrict)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mSDistrict);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mSDistrict);
        }

        // GET: MasterSetup/MSDistricts/Edit/5
        public async Task<IActionResult> Edit(Int32? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mSDistrict = await _context.MSDistrict.SingleOrDefaultAsync(m => m.Id == id);
            if (mSDistrict == null)
            {
                return NotFound();
            }
            return View(mSDistrict);
        }

        // POST: MasterSetup/MSDistricts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Name,CountryId,Status,Remarks")] District mSDistrict)
        {
            if (id != mSDistrict.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mSDistrict);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MSDistrictExists(mSDistrict.Id))
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
            return View(mSDistrict);
        }

        // GET: MasterSetup/MSDistricts/Delete/5
        public async Task<IActionResult> Delete(Int32? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mSDistrict = await _context.MSDistrict
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mSDistrict == null)
            {
                return NotFound();
            }

            return View(mSDistrict);
        }

        // POST: MasterSetup/MSDistricts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Int32 id)
        {
            var mSDistrict = await _context.MSDistrict.SingleOrDefaultAsync(m => m.Id == id);
            _context.MSDistrict.Remove(mSDistrict);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MSDistrictExists(Int32 id)
        {
            return _context.MSDistrict.Any(e => e.Id == id);
        }
    }
}
