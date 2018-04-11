using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System.Collections.Generic;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 23-Jan-2018.
    /// Description  : Repository for Membership.
    /// </summary>
    /// 

    public class MembershipRepository
    {
        private MerchantPortalDBContext _dBContext;

        public MembershipRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<Membership> GetAll()
        {
            return this._dBContext.Membership;
        }
        public Membership GetById(int? id)
        {
            return this._dBContext.Membership.Find(id);
        }
        public void Add(Membership Membership)
        {
            this._dBContext.Add(Membership);
        }
        public void Edit(Membership Membership)
        {
            this._dBContext.Attach(Membership);
            this._dBContext.Entry(Membership).State = EntityState.Modified;

        }
        public void Delete(Membership Membership)
        {

            this._dBContext.Attach(Membership);
            this._dBContext.Entry(Membership).State = EntityState.Modified;
        }
    }
}
