using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantPortal.Data.Repositories
{
    public class ControllerActionMappingRepository
    {
        private MerchantPortalDBContext _dBContext;

        public ControllerActionMappingRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<ControllerActionMapping> GetAll()
        {
            return (from ca in this._dBContext.ControllerActionMapping
                    join m in this._dBContext.Menu on ca.MenuId equals m.Id
                    where m.IsChild == true && m.IsDeleted == false
                    select ca
                    ).ToList();

            //return this._dBContext.ControllerActionMapping.Where(w => w.IsDeleted == false).ToList();
        }
        public ControllerActionMapping GetById(Int64? id)
        {
            return this._dBContext.ControllerActionMapping.SingleOrDefault(m => m.Id == id);
        }
        public void Add(ControllerActionMapping ControllerActionMapping)
        {
            this._dBContext.Add(ControllerActionMapping);
        }
        public void Edit(ControllerActionMapping ControllerActionMapping)
        {
            this._dBContext.Attach(ControllerActionMapping);
            this._dBContext.Entry(ControllerActionMapping).State = EntityState.Modified;
        }
        public void Delete(ControllerActionMapping ControllerActionMapping)
        {
            this._dBContext.Attach(ControllerActionMapping);
            this._dBContext.Entry(ControllerActionMapping).State = EntityState.Modified;
        }
    }
}
