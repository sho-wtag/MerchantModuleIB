using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System.Collections.Generic;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 23-Jan-2018.
    /// Description  : Repository for Division.
    /// </summary>
    /// 

    public class MailAndMessageRepository
    {
        private MerchantPortalDBContext _dBContext;

        public MailAndMessageRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<MailAndMessage> GetAll()
        {
            return this._dBContext.MailAndMessage;
        }
        public MailAndMessage GetById(int? id)
        {
            return this._dBContext.MailAndMessage.Find(id);
        }
        public void Add(MailAndMessage MailAndMessage)
        {
            this._dBContext.Add(MailAndMessage);
        }
        public void Edit(MailAndMessage MailAndMessage)
        {
            this._dBContext.Attach(MailAndMessage);
            this._dBContext.Entry(MailAndMessage).State = EntityState.Modified;

        }
        public void Delete(MailAndMessage MailAndMessage)
        {

            this._dBContext.Attach(MailAndMessage);
            this._dBContext.Entry(MailAndMessage).State = EntityState.Modified;
        }
    }
}
