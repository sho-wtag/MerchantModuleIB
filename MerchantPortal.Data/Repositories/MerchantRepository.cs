using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Debashish Mondal 
    /// Date         : 28-Feb-2018.
    /// Description  : Repository for Merchant.
    /// Modified By : Mahmudul Hasan
    /// Date: 04-Mar-2018
    /// </summary>
    /// 
    public class MerchantRepository
    {
        private MerchantPortalDBContext _dBContext;

        public MerchantRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<Merchant> GetAll()
        {
            return this._dBContext.Merchant.Where(w => w.IsDeleted == false).OrderBy(o=>o.MerchantName).ToList();
        }
        public Merchant GetById(Int64? id)
        {
            return this._dBContext.Merchant.SingleOrDefault(m => m.Id == id);
        }
        public void Add(Merchant Merchant)
        {
            this._dBContext.Add(Merchant);
        }
        public void Edit(Merchant Merchant)
        {
            this._dBContext.Attach(Merchant);
            this._dBContext.Entry(Merchant).State = EntityState.Modified;
        }
        public void Delete(Merchant Merchant)
        {
            this._dBContext.Attach(Merchant);
            this._dBContext.Entry(Merchant).State = EntityState.Modified;
        }

       
    }
}
