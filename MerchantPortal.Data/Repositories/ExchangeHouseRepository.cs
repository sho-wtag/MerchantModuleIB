using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System.Collections.Generic;
using System.Linq;


namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 11-Jan-2018.
    /// Description  : Repository for ExchangeHouse.
    /// Modified     : kowshik, 14-Jan-2017,
    /// </summary>
    public class ExchangeHouseRepository
    {
        private MerchantPortalDBContext _dBContext;
        public ExchangeHouseRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<ExchangeHouse> GetAll()
        {
            return this._dBContext.ExchangeHouse.Include(m=> m.Country);
        }
        public ExchangeHouse GetById(int? id)
        {
            return this._dBContext.ExchangeHouse.Include(e => e.Country).SingleOrDefault(m => m.Id == id);
        }       
        public void Add(ExchangeHouse ExchangeHouse)
        {
            this._dBContext.Add(ExchangeHouse);            
        }
        public void Edit(ExchangeHouse ExchangeHouse)
        {
            this._dBContext.Attach(ExchangeHouse);
            this._dBContext.Entry(ExchangeHouse).State = EntityState.Modified;
            
        }
        public void Delete(ExchangeHouse ExchangeHouse)
        {
            //var ExchangeHouse = _dBContext.ExchangeHouse.SingleOrDefaultAsync(m => m.Id == id);
            this._dBContext.Attach(ExchangeHouse);
            this._dBContext.Entry(ExchangeHouse).State = EntityState.Modified;            
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
