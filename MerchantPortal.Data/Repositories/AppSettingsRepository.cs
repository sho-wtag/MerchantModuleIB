using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 23-Jan-2018.
    /// Description  : Repository for AppSettings.
    /// </summary>
    public class AppSettingsRepository
    {
        private MerchantPortalDBContext _dBContext;

        public AppSettingsRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<AppSettings> GetAll()
        {
            return this._dBContext.AppSettings;
        }
        public AppSettings GetById(int? id)
        {
            return this._dBContext.AppSettings.Find(id);
        }
        public void Add(AppSettings AppSettings)
        {
            this._dBContext.Add(AppSettings);
        }
        public void Edit(int id, AppSettings AppSettings)
        {
             this._dBContext.Attach(AppSettings);
             this._dBContext.Entry(AppSettings).State = EntityState.Modified;

        }
        public void Delete(AppSettings AppSettings)
        {
            //var AppSettings = _dBContext.AppSettings.SingleOrDefaultAsync(m => m.Id == id);
            this._dBContext.Attach(AppSettings);
            this._dBContext.Entry(AppSettings).State = EntityState.Modified;
        }

        public async Task<bool> Exist(int id)
        {
            return await this._dBContext.AppSettings.AnyAsync(e=> e.Id==id);

        }
    }
}
