using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantPortal.Data.Repositories
{
    public class SubAgentRepository
    {
        private MerchantPortalDBContext _dBContext;
        public SubAgentRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public void Add(SubAgent agent)
        {
            this._dBContext.Add(agent);
        }
        public void Delete(SubAgent agent)
        {
            this._dBContext.Remove(agent);
        }
        public void Edit(SubAgent agent)
        {
            this._dBContext.Attach(agent);
            this._dBContext.Entry(agent).State= EntityState.Modified;
          
        }

        public SubAgent GetById(int? id)
        {
            return _dBContext.SubAgent.Find(id);
        }
        public IEnumerable<SubAgent> GetAll()
        {
            return this._dBContext.SubAgent;
        }
    }
}
