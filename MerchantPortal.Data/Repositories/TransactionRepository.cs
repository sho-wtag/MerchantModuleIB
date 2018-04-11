using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Mahmudul Hasan 
    /// Date         : 14-Mar-2018.
    /// Description  : Repository for Transaction.
    /// </summary>
    /// 
    public class TransactionRepository
    {
        private MerchantPortalDBContext _dBContext;

        public TransactionRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<Transaction> GetAll()
        {
            return this._dBContext.Transaction.ToList(); 
        }
        public Transaction GetById(long? id)
        {
            return this._dBContext.Transaction.SingleOrDefault(m => m.Id == id); ;
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
