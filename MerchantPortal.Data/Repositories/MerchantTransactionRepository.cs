using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MerchantPortal.Data.Models;
using System.Data.Common;
using System.Data;

namespace MerchantPortal.Data.Repositories
{
    public class MerchantTransactionRepository
    {
        private MerchantPortalDBContext _dBContext;

        public MerchantTransactionRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<Transaction> GetAll()
        {
            return this._dBContext.Transaction.ToList();
        }

        public IEnumerable<Transaction> GetMerchantTransaction()
        {
            List<Transaction> TransactionList;
            try
            {
                _dBContext.Database.OpenConnection();

                DbCommand dbCommand = _dBContext.Database.GetDbConnection().CreateCommand();
                dbCommand.CommandText = "proc_get_transaction";
                dbCommand.CommandType = CommandType.StoredProcedure;

                // DbParameter dbParameter = dbCommand.CreateParameter();
                //dbParameter.ParameterName = "@TerminalId";
                //dbParameter.Value = id.ToString();

                // dbCommand.Parameters.Add(dbParameter);

                using (var reader = dbCommand.ExecuteReader())
                {
                    TransactionList = reader.MapToList<Transaction>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dBContext.Database.CloseConnection();
            }
            return TransactionList;
            // return this._dBContext.Approval;
        }

        public IEnumerable<Transaction> GetMerchantTransactionByStatus(string Status)
        {
            return (from tr in this._dBContext.Transaction
                    join t in this._dBContext.Terminal on tr.TerminalId equals t.Id
                    join m in this._dBContext.Merchant on tr.MerchantId equals m.Id
                    join ts in this._dBContext.TransactionStatus on tr.TrnxStatusId equals ts.Id
                    join c in this._dBContext.Currency on tr.CurrencyId equals c.Id
                    join g in this._dBContext.MctGLSetup on tr.GLId equals g.ID into gl
                    from gltbl in gl.DefaultIfEmpty()
                    where ts.StatusName == Status && ts.StatusCode == "TRAN001"
                    select new Transaction
                    {
                        Id = tr.Id,
                        MerchantId = tr.MerchantId,
                        MerchantName = m.MerchantName,
                        TerminalId = tr.TerminalId,
                        TerminalName = t.OrgName,
                        CurrencyId = tr.CurrencyId,
                        CurrencyName = c.Name,
                        StartStamp = tr.StartStamp,
                        EndStamp = tr.EndStamp,
                        TrnxStatusId = tr.TrnxStatusId,
                        StatusName = ts.StatusName,
                        GLId = tr.GLId,
                        GLNumber = gltbl.AccountNo,
                        PrincipalAmount = tr.PrincipalAmount,
                        ComissionAmount = tr.ComissionAmount,
                        VatAmount = tr.VatAmount,
                        CheckerId = tr.CheckerId,
                        SettledDate = tr.SettledDate,
                        RefundDate = tr.RefundDate,
                        TTUMDownloadDate = tr.TTUMDownloadDate,
                        VisitorIP = tr.VisitorIP,
                        VisitorBrowserInfo = tr.VisitorBrowserInfo
                    }).ToList();
        }
        public IEnumerable<Transaction> GetMerchantTransactions()
        {
            return (from tr in this._dBContext.Transaction
                    join t in this._dBContext.Terminal on tr.TerminalId equals t.Id
                    join m in this._dBContext.Merchant on tr.MerchantId equals m.Id
                    join ts in this._dBContext.TransactionStatus on tr.TrnxStatusId equals ts.Id
                    join c in this._dBContext.Currency on tr.CurrencyId equals c.Id
                    join g in this._dBContext.MctGLSetup on tr.GLId equals g.ID into gl
                    from gltbl in gl.DefaultIfEmpty()
                    //where ts.StatusName == Status && ts.StatusCode == "TRAN001"
                    select new Transaction
                    {
                        Id = tr.Id,
                        MerchantId = tr.MerchantId,
                        MerchantName = m.MerchantName,
                        TerminalId = tr.TerminalId,
                        TerminalName = t.OrgName,
                        CurrencyId = tr.CurrencyId,
                        CurrencyName = c.Name,
                        StartStamp = tr.StartStamp,
                        EndStamp = tr.EndStamp,
                        TrnxStatusId = tr.TrnxStatusId,
                        StatusName = ts.StatusName,
                        GLId = tr.GLId,
                        GLNumber = gltbl.AccountNo,
                        PrincipalAmount = tr.PrincipalAmount,
                        ComissionAmount = tr.ComissionAmount,
                        VatAmount = tr.VatAmount,
                        CheckerId = tr.CheckerId,
                        SettledDate = tr.SettledDate,
                        RefundDate = tr.RefundDate,
                        TTUMDownloadDate = tr.TTUMDownloadDate,
                        VisitorIP = tr.VisitorIP,
                        VisitorBrowserInfo = tr.VisitorBrowserInfo,
                        TransactionRefId=tr.TransactionRefId
                    }).ToList();
        }
        public Transaction GetByTransactionId(long? id)
        {
            return this._dBContext.Transaction.SingleOrDefault(t => t.Id == id); ;
        }
        public Transaction GetById(Int64? id)
        {
            return (from tr in this._dBContext.Transaction
                    join t in this._dBContext.Terminal on tr.TerminalId equals t.Id
                    join m in this._dBContext.Merchant on tr.MerchantId equals m.Id
                    join ts in this._dBContext.TransactionStatus on tr.TrnxStatusId equals ts.Id
                    join c in this._dBContext.Currency on tr.CurrencyId equals c.Id
                    join g in this._dBContext.MctGLSetup on tr.GLId equals g.ID into gl
                    from gltbl in gl.DefaultIfEmpty()
                    where tr.Id == id
                    select new Transaction
                    {
                        Id = tr.Id,
                        MerchantId = tr.MerchantId,
                        MerchantName = m.MerchantName,
                        TerminalId = tr.TerminalId,
                        TerminalName = t.OrgName,
                        CurrencyId = tr.CurrencyId,
                        CurrencyName = c.Name,
                        StartStamp = tr.StartStamp,
                        EndStamp = tr.EndStamp,
                        TrnxStatusId = tr.TrnxStatusId,
                        StatusName = ts.StatusName,
                        GLId = tr.GLId,
                        GLNumber = gltbl.AccountNo,
                        PrincipalAmount = tr.PrincipalAmount,
                        ComissionAmount = tr.ComissionAmount,
                        VatAmount = tr.VatAmount,
                        CheckerId = tr.CheckerId,
                        SettledDate = tr.SettledDate,
                        RefundDate = tr.RefundDate,
                        TTUMDownloadDate = tr.TTUMDownloadDate,
                        VisitorIP = tr.VisitorIP,
                        VisitorBrowserInfo = tr.VisitorBrowserInfo,
                        TransactionRefId = tr.TransactionRefId
                    }).SingleOrDefault();
        }

        public void Add(Transaction Transaction)
        {
            this._dBContext.Add(Transaction);
        }
        public void Edit(Transaction Transaction)
        {
            this._dBContext.Attach(Transaction);
            this._dBContext.Entry(Transaction).State = EntityState.Modified;
        }
        public void Delete(Transaction Transaction)
        {
            this._dBContext.Attach(Transaction);
            this._dBContext.Entry(Transaction).State = EntityState.Modified;
        }
    }
}
