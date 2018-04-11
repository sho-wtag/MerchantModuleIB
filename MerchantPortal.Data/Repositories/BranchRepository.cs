using System.Collections.Generic;
using System.Linq;
using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 23-Jan-2018.
    /// Description  : Repository for Branch.
    /// </summary>

    public class BranchRepository
    {
        private MerchantPortalDBContext _dBContext;

        public BranchRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<Branch> GetAll()
        {
            return this._dBContext.Branch;
        }
        public Branch GetById(int? id)
        {
            // return this._dBContext.Branch.Include(b => b.Bank).Include(b => b.City).SingleOrDefault(m => m.Id == id);
            return this._dBContext.Branch.Include(b => b.Bank).SingleOrDefault(m => m.Id == id);
        }
        public void Add(Branch Branch)
        {
            this._dBContext.Add(Branch);
        }
        public void Edit(int id, Branch Branch)
        {
            this._dBContext.Attach(Branch);
            this._dBContext.Entry(Branch).State = EntityState.Modified;

        }
        public void Delete(Branch Branch)
        {           
            this._dBContext.Attach(Branch);
            this._dBContext.Entry(Branch).State = EntityState.Modified;
        }
    }
}
