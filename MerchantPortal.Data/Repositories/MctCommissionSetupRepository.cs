using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantPortal.Data.Repositories
{
    public class MctCommissionSetupRepository
    {
        private MerchantPortalDBContext _dBContext;

        public MctCommissionSetupRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<MctCommissionSetup> GetAll()
        {
            var list = (from com in _dBContext.MctCommissionSetup
                        join mct in _dBContext.Merchant on com.MerchantId equals mct.Id
                        join ter in _dBContext.Terminal on com.TerminalId equals ter.Id
                        where
                        com.IsDeleted == false
                        select new MctCommissionSetup() { Id = com.Id, Description = com.Description, IsPercentage = com.IsPercentage, CommissionAmount = com.CommissionAmount, BankPercentage = com.BankPercentage, IsActive = com.IsActive, IsDeleted = com.IsDeleted, MerchantName = mct.MerchantName, TerminalName = ter.OrgName, CalculationType = com.IsPercentage == true ? "Percentage" : "Flat text" }
                              ).OrderBy(o => o.MerchantName).ToList();
            return list;
        }
        public MctCommissionSetup GetById(Int64? id)
        {
            //return this._dBContext.MctCommissionSetup
            //    .Select(w => new MctCommissionSetup() { Id = w.Id, Description = w.Description, IsPercentage = w.IsPercentage, CommissionAmount = w.CommissionAmount, BankPercentage = w.BankPercentage, IsActive = w.IsActive, IsDeleted = w.IsDeleted })
            //    .SingleOrDefault(m => m.Id == id);

            var _object = (from com in _dBContext.MctCommissionSetup
                           join mct in _dBContext.Merchant on com.MerchantId equals mct.Id
                           join ter in _dBContext.Terminal on com.TerminalId equals ter.Id
                           where
                           com.IsDeleted == false
                           select new MctCommissionSetup() { Id = com.Id, MerchantId = com.MerchantId, TerminalId = com.TerminalId, Description = com.Description, IsPercentage = com.IsPercentage, CommissionAmount = com.CommissionAmount, BankPercentage = com.BankPercentage, IsActive = com.IsActive, IsDeleted = com.IsDeleted, MerchantName = mct.MerchantName, TerminalName = ter.OrgName, CalculationType = com.IsPercentage == true ? "Percentage" : "Flat text" }
                              ).SingleOrDefault(m => m.Id == id);
            return _object;
        }
        public MctCommissionSetup GetTerminalCommissionInfo(Int64? TerminalId)
        {
            return this._dBContext.MctCommissionSetup.SingleOrDefault(t => t.TerminalId == TerminalId && t.IsActive == true);
        }
        public void Add(MctCommissionSetup mctCommissionSetup)
        {
            this._dBContext.Add(mctCommissionSetup);
        }
        public void Edit(MctCommissionSetup mctCommissionSetup)
        {
            this._dBContext.Attach(mctCommissionSetup);
            this._dBContext.Entry(mctCommissionSetup).State = EntityState.Modified;

        }
        public void Delete(MctCommissionSetup mctCommissionSetup)
        {

            this._dBContext.Attach(mctCommissionSetup);
            this._dBContext.Entry(mctCommissionSetup).State = EntityState.Modified;
        }
    }
}
