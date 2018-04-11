using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MerchantPortal.Data.Repositories
{
    /// <summary>
    /// Developed by : Maksud 
    /// Date         : 23-Jan-2018.
    /// Description  : Repository for Bank.
    /// </summary>

    public class BankRepository
    {
        private MerchantPortalDBContext _dBContext;

        public BankRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<Bank> GetAll()
        {
            return this._dBContext.Bank;
        }
        public Bank GetById(int? id)
        {
            return this._dBContext.Bank.Find(id);
        }
        public void Add(Bank Bank)
        {
            this._dBContext.Add(Bank);
        }
        public void Edit(int id, Bank Bank)
        {
            this._dBContext.Attach(Bank);
            this._dBContext.Entry(Bank).State = EntityState.Modified;

        }
        public void Delete(Bank Bank)
        {
            //var Bank = _dBContext.Bank.SingleOrDefaultAsync(m => m.Id == id);
            this._dBContext.Attach(Bank);
            this._dBContext.Entry(Bank).State = EntityState.Modified;
        }
    }
}
