using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantPortal.Data.Repositories
{   
    public class MctGLSetupRepository
    {
        private MerchantPortalDBContext _dBContext;

        public MctGLSetupRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<MctGLSetup> GetAll()
        {
            //return this._dBContext.MctGLSetup.Where(w => w.IsDeleted == false).ToList();
            var list = (from gl in _dBContext.MctGLSetup
                        join mct in _dBContext.Merchant on gl.MerchantID equals mct.Id
                        join ter in _dBContext.Terminal on gl.TerminalId equals ter.Id                        
                        where
                        gl.IsDeleted == false
                        select new MctGLSetup() { ID = gl.ID, MerchantID = gl.MerchantID, TerminalId = gl.TerminalId, GLNumber=gl.GLNumber, AccountNo=gl.AccountNo, AccountName=gl.AccountName, Descriptions=gl.Descriptions, IsActive = gl.IsActive, IsDeleted = gl.IsDeleted, MerchantName = mct.MerchantName, TerminalName = ter.OrgName}
                           ).ToList();
            return list;
        }
        public MctGLSetup GetById(Int64? id)
        {
            //return this._dBContext.MctGLSetup.SingleOrDefault(m => m.ID == id);
            var _object = (from gl in _dBContext.MctGLSetup
                           join mct in _dBContext.Merchant on gl.MerchantID equals mct.Id
                           join ter in _dBContext.Terminal on gl.TerminalId equals ter.Id                           
                           where
                           gl.IsDeleted == false
                           select new MctGLSetup() { ID = gl.ID, MerchantID = gl.MerchantID, TerminalId = gl.TerminalId, GLNumber = gl.GLNumber, AccountNo = gl.AccountNo, AccountName = gl.AccountName, Descriptions = gl.Descriptions, IsActive = gl.IsActive, IsDeleted = gl.IsDeleted, MerchantName = mct.MerchantName, TerminalName = ter.OrgName }
                           ).SingleOrDefault(m => m.ID == id);

            return _object;
        }
        public void Add(MctGLSetup mctGLSetup)
        {
            this._dBContext.Add(mctGLSetup);
        }
        public void Edit(MctGLSetup mctGLSetup)
        {
            this._dBContext.Attach(mctGLSetup);
            this._dBContext.Entry(mctGLSetup).State = EntityState.Modified;
        }
        public void Delete(MctGLSetup mctGLSetup)
        {
            this._dBContext.Attach(mctGLSetup);
            this._dBContext.Entry(mctGLSetup).State = EntityState.Modified;
        }
    }
}
