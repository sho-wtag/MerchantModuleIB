using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System.Collections.Generic;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 23-Jan-2018.
    /// Description  : Repository for Currency.
    /// </summary>
    /// 
    public class CurrencyRepository
    {
        private MerchantPortalDBContext _dBContext;

        public CurrencyRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<Currency> GetAll()
        {
            return this._dBContext.Currency;
        }
        public Currency GetById(int? id)
        {
            return this._dBContext.Currency.Find(id);
        }
        public void Add(Currency Currency)
        {
            this._dBContext.Add(Currency);
        }
        public void Edit(int id, Currency Currency)
        {
            this._dBContext.Attach(Currency);
            this._dBContext.Entry(Currency).State = EntityState.Modified;

        }
        public void Delete(Currency Currency)
        {

            this._dBContext.Attach(Currency);
            this._dBContext.Entry(Currency).State = EntityState.Modified;
        }
    }
}
