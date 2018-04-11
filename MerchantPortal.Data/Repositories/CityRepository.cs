using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 23-Jan-2018.
    /// Description  : Repository for City.
    /// </summary>
    /// 
    public class CityRepository
    {
        private MerchantPortalDBContext _dBContext;

        public CityRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<City> GetAll()
        {
            return this._dBContext.City.Include(c => c.District);
        }
        public  City GetById(int? id)
        {
             return this._dBContext.City.Find(id);
           // return  this._dBContext.City.Include(c => c.District).SingleOrDefault(m => m.Id == id);
        }
        public void Add(City City)
        {
            this._dBContext.Add(City);
        }
        public void Edit(int id, City City)
        {
            this._dBContext.Attach(City);
            this._dBContext.Entry(City).State = EntityState.Modified;

        }
        public void Delete(City City)
        {

            this._dBContext.Attach(City);
            this._dBContext.Entry(City).State = EntityState.Modified;
        }
    }
}
