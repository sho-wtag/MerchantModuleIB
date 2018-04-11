using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System.Collections.Generic;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 23-Jan-2018.
    /// Description  : Repository for Country.
    /// </summary>
    /// 
    public class CountryRepository
    {
        private MerchantPortalDBContext _dBContext;

        public CountryRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<Country> GetAll()
        {
            return this._dBContext.Country;
        }
        public Country GetById(int? id)
        {
            return this._dBContext.Country.Find(id);
        }
        public void Add(Country Country)
        {
            this._dBContext.Add(Country);
        }
        public void Edit(int id, Country Country)
        {
            this._dBContext.Attach(Country);
            this._dBContext.Entry(Country).State = EntityState.Modified;

        }
        public void Delete(Country Country)
        {

            this._dBContext.Attach(Country);
            this._dBContext.Entry(Country).State = EntityState.Modified;
        }
    }
}
