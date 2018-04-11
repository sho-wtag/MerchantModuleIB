using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 23-Jan-2018.
    /// Description  : Repository for District.
    /// </summary>
    /// 

    public class DistrictRepository
    {

        private MerchantPortalDBContext _dBContext;

        public DistrictRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<District> GetAll()
        {
            return this._dBContext.District;
        }
        public District GetById(int? id)
        {
           // return this._dBContext.District.Find(id);
            return this._dBContext.District.Include(d => d.Division).SingleOrDefault(m => m.Id == id);
        }
        public void Add(District District)
        {
            this._dBContext.Add(District);
        }
        public void Edit(District District)
        {
            this._dBContext.Attach(District);
            this._dBContext.Entry(District).State = EntityState.Modified;

        }
        public void Delete(District district)
        {

            this._dBContext.Attach(district);
            this._dBContext.Entry(district).State = EntityState.Modified;
        }
    }
}
