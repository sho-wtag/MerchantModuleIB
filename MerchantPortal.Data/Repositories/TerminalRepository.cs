using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Mahmudul Hasan 
    /// Date         : 06-Mar-2018
    /// Description  : Repository for Merchant.
    /// Modified By  : 
    /// Date: 
    /// </summary>
    /// 
    public class TerminalRepository
    {
        private MerchantPortalDBContext _dBContext;

        public TerminalRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<Terminal> GetAll()
        {
            return this._dBContext.Terminal.Where(w => w.IsDeleted == false).OrderBy(o => o.OrgName).ToList();
        }
        public IEnumerable<Terminal> GetTerminalData()
        {
            return (from t in this._dBContext.Terminal
                    join m in this._dBContext.Merchant on t.MerchantId equals m.Id
                    select new Terminal
                    {
                        Id = t.Id,
                        MerchantId = t.MerchantId,
                        MerchantName = m.MerchantName,
                        //Merchant = t.Merchant,
                        OrgName = t.OrgName,
                        OwnerName = t.OwnerName,
                        ContactAddess = t.ContactAddess,
                        PhoneNo = t.PhoneNo,
                        EmailId = t.EmailId,
                        FaxNo = t.FaxNo,
                        ContactPerson = t.ContactPerson,
                        ContactPersonPhone = t.ContactPersonPhone,
                        ContactPersonAddress = t.ContactPersonAddress,
                        ContactPersonEmailId = t.ContactPersonEmailId,
                        TradeLicenseNo = t.TradeLicenseNo,
                        VATRegistrationNo = t.VATRegistrationNo,
                        IsActive = t.IsActive,
                        IsApprove = t.IsApprove,
                        IsDeleted = t.IsDeleted
                    }).Where(w => w.IsDeleted == false).ToList();
        }
        public Terminal GetById(Int64? id)
        {
            return this._dBContext.Terminal.SingleOrDefault(t => t.Id == id);
        }
        public void Add(Terminal Terminal)
        {
            this._dBContext.Add(Terminal);
        }
        public void Edit(Terminal Terminal)
        {
            this._dBContext.Attach(Terminal);
            this._dBContext.Entry(Terminal).State = EntityState.Modified;
        }
        public void Delete(Terminal Terminal)
        {
            this._dBContext.Attach(Terminal);
            this._dBContext.Entry(Terminal).State = EntityState.Modified;
        }
    }
}
