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
    public class SettlementRuleRepository
    {
        private MerchantPortalDBContext _dBContext;

        public SettlementRuleRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<SettlementRule> GetAll()
        {
            return this._dBContext.SettlementRule.Where(w => w.IsDeleted == false).ToList();
        }
        public int GetRuleCount()
        {
            return this._dBContext.SettlementRule.ToList().Count();
        }
        public IEnumerable<SettlementRule> GetSettlementRuleData()
        {
            return (from r in this._dBContext.SettlementRule
                    join t in this._dBContext.Terminal on r.TerminalId equals t.Id
                    join m in this._dBContext.Merchant on r.MerchantId equals m.Id
                    select new SettlementRule
                    {
                        Id = r.Id,
                        MerchantId = r.MerchantId,
                        MerchantName = m.MerchantName,
                        TerminalId = r.TerminalId,
                        TerminalName = t.OrgName,
                        SettlementRuleId = r.SettlementRuleId,
                        Description = r.Description,
                        SettlementType = r.SettlementType,
                        Frequency = r.Frequency,
                        CommissionEnable = r.CommissionEnable,
                        Commission = r.Commission,
                        VATEnable = r.VATEnable,
                        VATPercentage = r.VATPercentage,
                        IsActive = r.IsActive,
                        IsApprove = r.IsApprove,
                        IsDeleted = r.IsDeleted
                    }).Where(w => w.IsDeleted == false).ToList();
        }
        public SettlementRule GetById(Int64? id)
        {
            return (from r in this._dBContext.SettlementRule
                    join t in this._dBContext.Terminal on r.TerminalId equals t.Id
                    join m in this._dBContext.Merchant on r.MerchantId equals m.Id
                    select new SettlementRule
                    {
                        Id = r.Id,
                        MerchantId = r.MerchantId,
                        MerchantName = m.MerchantName,
                        TerminalId = r.TerminalId,
                        TerminalName = t.OrgName,
                        SettlementRuleId = r.SettlementRuleId,
                        Description = r.Description,
                        SettlementType = r.SettlementType,
                        Frequency = r.Frequency,
                        CommissionEnable = r.CommissionEnable,
                        Commission = r.Commission,
                        VATEnable = r.VATEnable,
                        VATPercentage = r.VATPercentage,
                        IsActive = r.IsActive,
                        IsApprove = r.IsApprove,
                        IsDeleted = r.IsDeleted
                    }).Where(t => t.Id == id).SingleOrDefault();
            //return this._dBContext.SettlementRule.SingleOrDefault(t => t.Id == id);
        }
        public void Add(SettlementRule SettlementRule)
        {
            this._dBContext.Add(SettlementRule);
        }
        public void Edit(SettlementRule SettlementRule)
        {
            this._dBContext.Attach(SettlementRule);
            this._dBContext.Entry(SettlementRule).State = EntityState.Modified;
        }
        public void Delete(SettlementRule SettlementRule)
        {
            this._dBContext.Attach(SettlementRule);
            this._dBContext.Entry(SettlementRule).State = EntityState.Modified;
        }
    }
}
