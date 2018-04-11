using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System.Collections.Generic;

namespace MerchantPortal.Data.Repositories
{

    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 23-Jan-2018.
    /// Description  : Repository for ChargeConfig.
    /// </summary>
    /// 
    public class ChargeConfigRepository
    {
        private MerchantPortalDBContext _dBContext;

        public ChargeConfigRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<ChargeConfig> GetAll()
        {
            return this._dBContext.ChargeConfig;
        }
        public ChargeConfig GetById(int? id)
        {
            return this._dBContext.ChargeConfig.Find(id);
        }
        public void Add(ChargeConfig ChargeConfig)
        {
            this._dBContext.Add(ChargeConfig);
        }
        public void Edit(int id, ChargeConfig ChargeConfig)
        {
            this._dBContext.Attach(ChargeConfig);
            this._dBContext.Entry(ChargeConfig).State = EntityState.Modified;

        }
        public void Delete(ChargeConfig ChargeConfig)
        {
      
            this._dBContext.Attach(ChargeConfig);
            this._dBContext.Entry(ChargeConfig).State = EntityState.Modified;
        }
    }
}
