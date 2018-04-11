using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantPortal.Data.Repositories
{
    public class MctVATSetupRepository
    {
        private MerchantPortalDBContext _dBContext;

        public MctVATSetupRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<MctVATSetup> GetAll()
        {
            // return this._dBContext.MctVATSetup.Where(w => w.IsDeleted == false).ToList();

            var list = (from vat in _dBContext.MctVATSetup
                        join mct in _dBContext.Merchant on vat.MerchantID equals mct.Id
                        join ter in _dBContext.Terminal on vat.TerminalId equals ter.Id
                        join con in _dBContext.Country.DefaultIfEmpty() on vat.CountryID equals con.Id
                        where
                        vat.IsDeleted == false
                        select new MctVATSetup() { ID = vat.ID, VATID = vat.VATID, MerchantID=vat.MerchantID, TerminalId=vat.TerminalId, Descriptions=vat.Descriptions, CountryID=vat.CountryID, VATRegNo=vat.VATRegNo, IsContinuousType=vat.IsContinuousType, Percentage = vat.Percentage, IsActive = vat.IsActive, IsDeleted = vat.IsDeleted, MerchantName = mct.MerchantName, TerminalName = ter.OrgName, CountryName=con.StrCountryName }
                            ).ToList();
            return list;
        }
        public MctVATSetup GetById(Int64? id)
        {
            var _object = (from vat in _dBContext.MctVATSetup
                        join mct in _dBContext.Merchant on vat.MerchantID equals mct.Id
                        join ter in _dBContext.Terminal on vat.TerminalId equals ter.Id
                        join con in _dBContext.Country.DefaultIfEmpty() on vat.CountryID equals con.Id
                        where
                        vat.IsDeleted == false
                        select new MctVATSetup() { ID = vat.ID, VATID = vat.VATID, MerchantID = vat.MerchantID, TerminalId = vat.TerminalId, Descriptions = vat.Descriptions, CountryID = vat.CountryID, VATRegNo = vat.VATRegNo, IsContinuousType = vat.IsContinuousType, Percentage = vat.Percentage, IsActive = vat.IsActive, IsDeleted = vat.IsDeleted, MerchantName = mct.MerchantName, TerminalName = ter.OrgName, CountryName = con.StrCountryName }
                           ).SingleOrDefault(m => m.ID == id);

            return _object; 
            //return this._dBContext.MctVATSetup.SingleOrDefault(m => m.ID == id);
        }
        public MctVATSetup GetTerminalVATInfo(Int64? TerminalId)
        {
            return this._dBContext.MctVATSetup.SingleOrDefault(t => t.TerminalId == TerminalId && t.IsActive == true);
        }
        public void Add(MctVATSetup mctVATSetup)
        {
            this._dBContext.Add(mctVATSetup);
        }
        public void Edit(MctVATSetup mctVATSetup)
        {
            this._dBContext.Attach(mctVATSetup);
            this._dBContext.Entry(mctVATSetup).State = EntityState.Modified;
        }
        public void Delete(MctVATSetup mctCommissionSetup)
        {
            this._dBContext.Attach(mctCommissionSetup);
            this._dBContext.Entry(mctCommissionSetup).State = EntityState.Modified;
        }
    }
}

