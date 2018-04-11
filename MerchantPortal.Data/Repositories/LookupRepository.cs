using System;
using System.Collections.Generic;
using System.Text;
using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MerchantPortal.Data.Repositories
{
    public class LookupRepository
    {
        private MerchantPortalDBContext _dBContext;
        public LookupRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public List<Lookup> GetAll()
        {
            return  this._dBContext.Lookup.Include(l=> l.LookType).ToList();
        }
        public Lookup GetById(int? id)
        {
            return this._dBContext.Lookup.Include(l => l.LookType).SingleOrDefault(l=> l.Id ==id);
        }
        public void Add(Lookup lookup)
        {
            this._dBContext.Add(lookup);
        }
        public void Edit( Lookup lookup)
        {
            this._dBContext.Attach(lookup);
            this._dBContext.Entry(lookup).State = EntityState.Modified;

        }
        public void Delete(Lookup lookup)
        {            
            this._dBContext.Attach(lookup);
            this._dBContext.Entry(lookup).State = EntityState.Modified;
        }

    }
}
