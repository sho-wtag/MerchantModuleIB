using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantPortal.Data.Repositories
{
    public class ApplicationRoleRepository
    {
        private MerchantPortalDBContext _dBContext;

        public ApplicationRoleRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<ApplicationRole> GetAll()
        {
            return this._dBContext.ApplicationRole;
        }
        public ApplicationRole GetById(Int64? id)
        {
            return this._dBContext.ApplicationRole.Find(id);
        }
        public void Add(ApplicationRole ApplicationRole)
        {
            this._dBContext.Add(ApplicationRole);
        }
        public void Edit(Int64 id, ApplicationRole ApplicationRole)
        {
            this._dBContext.Attach(ApplicationRole);
            this._dBContext.Entry(ApplicationRole).State = EntityState.Modified;

        }
        public void Delete(ApplicationRole ApplicationRole)
        {
            //var ApplicationRole = _dBContext.ApplicationRole.SingleOrDefaultAsync(m => m.Id == id);
            this._dBContext.Attach(ApplicationRole);
            this._dBContext.Entry(ApplicationRole).State = EntityState.Modified;
        }
    }
}
