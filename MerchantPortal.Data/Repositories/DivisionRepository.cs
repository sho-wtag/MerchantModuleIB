using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 23-Jan-2018.
    /// Description  : Repository for Division.
    /// </summary>
    /// 

    public class DivisionRepository
    {
        private MerchantPortalDBContext _dBContext;

        public DivisionRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<Division> GetAll()
        {
            return this._dBContext.Division;
        }
        public Division GetById(int? id)
        {
            return this._dBContext.Division.Include(d => d.Country).SingleOrDefault(m => m.Id == id);
        }
        public void Add(Division Division)
        {
            this._dBContext.Add(Division);
        }
        public void Edit(Division Division)
        {
            this._dBContext.Attach(Division);
            this._dBContext.Entry(Division).State = EntityState.Modified;

        }
        public void Delete(Division Division)
        {

            this._dBContext.Attach(Division);
            this._dBContext.Entry(Division).State = EntityState.Modified;
        }
    }
}
