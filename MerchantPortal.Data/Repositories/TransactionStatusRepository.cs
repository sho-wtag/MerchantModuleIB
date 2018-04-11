using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Mahmudul Hasan 
    /// Date         : 14-Mar-2018.
    /// Description  : Repository for Transaction.
    /// </summary>
    /// 
    public class TransactionStatusRepository
    {
        private MerchantPortalDBContext _dBContext;

        public TransactionStatusRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<TransactionStatus> GetAll()
        {
            return this._dBContext.TransactionStatus.ToList();
        }
        public IEnumerable<TransactionStatus> GetStatusByCode(string Code)
        {
            return this._dBContext.TransactionStatus.Where(w => w.StatusName == Code).ToList();
        }
        public TransactionStatus GetById(long? id)
        {
            return this._dBContext.TransactionStatus.SingleOrDefault(m => m.Id == id); ;
        }
        public TransactionStatus GetByStatusCodeName(string Code, string Name)
        {
            return this._dBContext.TransactionStatus.SingleOrDefault(m => m.StatusCode == Code && m.StatusName == Name);
        }
        public void Add(TransactionStatus TransactionStatus)
        {
            this._dBContext.Add(TransactionStatus);
        }
        public void Edit(TransactionStatus TransactionStatus)
        {
            this._dBContext.Attach(TransactionStatus);
            this._dBContext.Entry(TransactionStatus).State = EntityState.Modified;
        }
        public void Delete(TransactionStatus TransactionStatus)
        {
            this._dBContext.Attach(TransactionStatus);
            this._dBContext.Entry(TransactionStatus).State = EntityState.Modified;
        }
    }
}
