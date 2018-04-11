using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System.Collections.Generic;


namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 11-Jan-2018.
    /// Description  : Repository for agent.
    /// Modified     : kowshik, 14-Jan-2017,
    /// </summary>
    public class AgentRepository
    {
        private MerchantPortalDBContext _dBContext;
        public AgentRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<Agent> GetAll()
        {
            return this._dBContext.Agent;
        }
        public Agent GetById(int? id)
        {
            return this._dBContext.Agent.Find(id);
        }       
        public void Add(Agent agent)
        {
            this._dBContext.Add(agent);            
        }
        public void Edit(int id,Agent agent)
        {
            this._dBContext.Attach(agent);
            this._dBContext.Entry(agent).State = EntityState.Modified;
            
        }
        public void Delete(Agent agent)
        {
            //var agent = _dBContext.Agent.SingleOrDefaultAsync(m => m.Id == id);
            this._dBContext.Attach(agent);
            this._dBContext.Entry(agent).State = EntityState.Modified;            
        }    
       

       

        /*  object dispose*/
        /*
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        */
    }
}
