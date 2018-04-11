using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 23-Jan-2018.
    /// Description  : Repository for Membership.
    /// </summary>
    ///
    public class TimeZoneRepository
    {
        private MerchantPortalDBContext _dBContext;

        public TimeZoneRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<MerchantPortal.Data.Models.Timezone> GetAll()
        {
            return this._dBContext.TimeZone;
        }
        public MerchantPortal.Data.Models.Timezone GetById(int? id)
        {
            return this._dBContext.TimeZone.Find(id);
        }
        public void Add(MerchantPortal.Data.Models.Timezone TimeZone)
        {
            this._dBContext.Add(TimeZone);
        }
        public void Edit(int id, MerchantPortal.Data.Models.Timezone TimeZone)
        {
            this._dBContext.Attach(TimeZone);
            this._dBContext.Entry(TimeZone).State = EntityState.Modified;

        }
        public void Delete(MerchantPortal.Data.Models.Timezone TimeZone)
        {

            this._dBContext.Attach(TimeZone);
            this._dBContext.Entry(TimeZone).State = EntityState.Modified;
        }
    }
}
