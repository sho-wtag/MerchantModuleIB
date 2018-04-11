using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System.Collections.Generic;
using System.Linq;
namespace MerchantPortal.Data.Repositories

{
    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 23-Jan-2018.
    /// Description  : Repository for MembershipCommission.
    /// </summary>
    /// 

    public class MembershipCommissionRepository
    {
        private MerchantPortalDBContext _dBContext;

        public MembershipCommissionRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<MembershipCommission> GetAll()
        {
            return this._dBContext.MembershipCommission.Include(m => m.Branch).Include(m => m.Membership).Include(m => m.PaymentMode);
        }
        public MembershipCommission GetById(int? id)
        {
            return this._dBContext.MembershipCommission
                .Include(m => m.Branch)
                .Include(m => m.Membership)
                .Include(m => m.PaymentMode)
                .SingleOrDefault(m => m.Id == id);
        }
        public void Add(MembershipCommission MembershipCommission)
        {
            this._dBContext.Add(MembershipCommission);
        }
        public void Edit( MembershipCommission MembershipCommission)
        {
            this._dBContext.Attach(MembershipCommission);
            this._dBContext.Entry(MembershipCommission).State = EntityState.Modified;

        }
        public void Delete(MembershipCommission MembershipCommission)
        {

            this._dBContext.Attach(MembershipCommission);
            this._dBContext.Entry(MembershipCommission).State = EntityState.Modified;
        }

    }
}
