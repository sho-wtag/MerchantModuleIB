using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MerchantPortal.Data.Models
{
    /// <summary>
    /// Developed By:Maksudur Rahman
    /// Date: 10-Jan-2018
    /// Decription :Create model class for table Agm_Agent
    /// </summary>
    [Table("Agm_Agent")]
    public class Agent 
    {   
        public int Id { get; set; }        
        public string Name { get; set; }   
        public string Code { get; set; }        
        public string HeadOfficeAddress { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } 
        public bool IsDeleted { get; set; }

        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            this.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

    }


}
