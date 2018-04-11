using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantPortal.Data.Repositories
{
    public class ApplicationUserRepository
    {
        private MerchantPortalDBContext _dBContext;

        public ApplicationUserRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<ApplicationUser> GetAll()
        {
            return this._dBContext.ApplicationUser;
        }
        public ApplicationUser GetById(Int64? id)
        {
            return this._dBContext.ApplicationUser.Find(id);
        }
        public void Add(ApplicationUser ApplicationUser)
        {
            this._dBContext.Add(ApplicationUser);
        }
        public void Edit(Int64 id, ApplicationUser ApplicationUser)
        {
            this._dBContext.Attach(ApplicationUser);
            this._dBContext.Entry(ApplicationUser).State = EntityState.Modified;

        }
        public void Delete(ApplicationUser ApplicationUser)
        {
            //var Bank = _dBContext.Bank.SingleOrDefaultAsync(m => m.Id == id);
            this._dBContext.Attach(ApplicationUser);
            this._dBContext.Entry(ApplicationUser).State = EntityState.Modified;
        }
    }
}
