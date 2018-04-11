using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System.Collections.Generic;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 1-Feb-2018.
    /// Description  : Repository for PaymentMode.
    /// </summary>
    /// 
    public class PaymentModeRepository
    {
        private MerchantPortalDBContext _dBContext;

        public PaymentModeRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<PaymentMode> GetAll()
        {
            return this._dBContext.PaymentMode;
        }
        public PaymentMode GetById(int? id)
        {
            return this._dBContext.PaymentMode.Find(id);
        }
        public void Add(PaymentMode PaymentMode)
        {
            this._dBContext.Add(PaymentMode);
        }
        public void Edit( PaymentMode PaymentMode)
        {
            this._dBContext.Attach(PaymentMode);
            this._dBContext.Entry(PaymentMode).State = EntityState.Modified;

        }
        public void Delete(PaymentMode PaymentMode)
        {

            this._dBContext.Attach(PaymentMode);
            this._dBContext.Entry(PaymentMode).State = EntityState.Modified;
        }
    }
}
