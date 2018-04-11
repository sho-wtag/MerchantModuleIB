using System;
using System.Collections.Generic;
using System.Text;
using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Maksudur Rahman  
    /// Date         : 14-Jan-2018.
    /// Description  : Repository for AuditTrail.   
    /// </summary>
    public class AuditTrailRepository
    {
        private MerchantPortalDBContext _dBContext;
        public AuditTrailRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<AuditTrail> GetAll()
        {
            return _dBContext.AuditTrail;                 
        }
        public AuditTrail GetById(int? id)
        {
            return this._dBContext.AuditTrail.SingleOrDefault(m => m.Id == id);
        }
        public void Add(AuditTrail auditTrail)
        {
            auditTrail.UpdatedDate = DateTime.Now;
            this._dBContext.Add(auditTrail);
        }
        public void Edit( AuditTrail auditTrail)
        {           
            this._dBContext.Attach(auditTrail);
            this._dBContext.Entry(auditTrail).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var AuditTrail = _dBContext.AuditTrail.SingleOrDefaultAsync(m => m.Id == id);
            //AuditTrail.IsDeleted = true;
            //AuditTrail.
            this._dBContext.Entry(AuditTrail).State = EntityState.Modified;
        }
       
    }
}
