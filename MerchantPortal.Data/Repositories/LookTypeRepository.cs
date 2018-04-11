using System;
using System.Collections.Generic;
using System.Text;
using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Kowshik  
    /// Date         : 14-Jan-2018.
    /// Description  : Repository for LookType.   
    /// </summary>
    public class LookTypeRepository
    {
        private MerchantPortalDBContext _dBContext;
        public LookTypeRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<LookType> GetAll()
        {
            return _dBContext.LookType;                 
        }
        public LookType GetById(int? id)
        {
            return this._dBContext.LookType.SingleOrDefault(m => m.Id == id);
        }
        public void Add(LookType lookType)
        {
            lookType.UpdateDate = DateTime.Now;
            this._dBContext.Add(lookType);
        }
        public void Edit( LookType lookType)
        {           
            this._dBContext.Attach(lookType);
            this._dBContext.Entry(lookType).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var lookType = _dBContext.LookType.SingleOrDefaultAsync(m => m.Id == id);
            //LookType.IsDeleted = true;
            //LookType.
            this._dBContext.Entry(lookType).State = EntityState.Modified;
        }
        /*  object dispose*/
        /*
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        */
    }
}
