using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace MerchantPortal.Data.Repositories
{

    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 23-Jan-2018.
    /// Description  : Repository for Membership.
    /// </summary>
    ///

    public class TestKeyRepository
    {
        private MerchantPortalDBContext _dBContext;

        public TestKeyRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<TestKey> GetAll()
        {
            return this._dBContext.TestKey.Include(t => t.ExchangeHouse);
        }
        public TestKey GetById(int? id)
        {
            return this._dBContext.TestKey.Include(t => t.ExchangeHouse)
                .SingleOrDefault(m => m.Id == id);
        }
        public void Add(TestKey TestKey)
        {
            this._dBContext.Add(TestKey);
        }
        public void Edit( TestKey TestKey)
        {
            this._dBContext.Attach(TestKey);
            this._dBContext.Entry(TestKey).State = EntityState.Modified;

        }
        public void Delete(TestKey TestKey)
        {

            this._dBContext.Attach(TestKey);
            this._dBContext.Entry(TestKey).State = EntityState.Modified;
        }
    }
}
