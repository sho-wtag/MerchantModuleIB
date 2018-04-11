using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace MerchantPortal.Data.Concrete
{
    /// <summary>
    /// Class Name      : AuditTrail
    /// Developed By    : Md. Maksudur Rahman
    /// Developed On    : 13-Feb-2018
    /// Description     : Generic Repository class for Entity Operation
    /// Usefull link    : http://www.softcodearticle.com/2013/07/entity-framework-auditing-implementation/
    /// </summary>
    public class AuditTrails : IDisposable
    {
        /*
        private int UserID { get; set; }
        public string SessionID { get; set; }
        public int MenuID { get; set; }
        */
        public DbContext _dbContext { get; set; }

        private AuditTrails()
        {

        }
        public AuditTrails(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        internal enum AuditActions
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }

        public string DoAudit()
        {
            string xml = string.Empty;
            var entries = this._dbContext.ChangeTracker.Entries()
                .Where
                (
                    e => e.State == EntityState.Modified ||
                        e.State == EntityState.Added ||
                        e.State == EntityState.Deleted
                );
            if (entries.Count() > 0)
            {
                XmlDocument doc = new XmlDocument();
                XmlElement parentNode = doc.CreateElement("root");

                foreach (var entity in entries)
                {
                    if (entity.Entity != null)
                    {
                        parentNode.AppendChild(this.AuditTrailFactory(entity, doc));
                    }
                }
                doc.AppendChild(parentNode);

                StringWriter sw = new StringWriter();
                XmlTextWriter xw = new XmlTextWriter(sw);
                doc.WriteTo(xw);

                xml = sw.ToString();

            }
            return xml;
        }

        private XmlElement AuditTrailFactory(EntityEntry entry, XmlDocument doc)
        {
            string tableName = GetTableName(entry);
            XmlElement tableNode = doc.CreateElement(tableName);
            tableNode.SetAttribute("AuditAction", entry.State.ToString());

            if (entry.State == EntityState.Added)
            {
                GetAddedProperties(entry, tableNode, doc);
            }
            else if (entry.State == EntityState.Deleted)
            {
                GetDeletedProperties(entry, tableNode, doc);
            }
            else if (entry.State == EntityState.Modified)
            {
                GetModifiedProperties(entry, tableNode, doc);
            }

            return tableNode;
        }
        private string GetTableName(EntityEntry dbEntry)
        {
            if (dbEntry == null) return string.Empty;

            TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
            string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;
            return tableName;
        }

        private void GetAddedProperties(EntityEntry entry, XmlElement tableNode, XmlDocument doc)
        {
            foreach (var propertyName in entry.CurrentValues.Properties)
            {
                var newVal = entry.CurrentValues[propertyName];
                if (newVal != null)
                {
                    XmlElement propertyNode = doc.CreateElement(propertyName.Name);
                    propertyNode.SetAttribute("NewValue", newVal.ToString());
                    tableNode.AppendChild(propertyNode);
                }
            }
        }
        private void GetDeletedProperties(EntityEntry entry, XmlElement tableNode, XmlDocument doc)
        {
            PropertyValues dbValues = entry.GetDatabaseValues();
            foreach (var propertyName in dbValues.Properties)
            {
                var oldVal = dbValues[propertyName];
                if (oldVal != null)
                {
                    XmlElement propertyNode = doc.CreateElement(propertyName.Name);
                    propertyNode.SetAttribute("DeletedValue", oldVal.ToString());
                    tableNode.AppendChild(propertyNode);
                }
            }
        }

        private void GetModifiedProperties(EntityEntry entry, XmlElement tableNode, XmlDocument doc)
        {
            PropertyValues dbValues = entry.GetDatabaseValues();
            foreach (var propertyName in entry.OriginalValues.Properties)
            {
                var oldVal = dbValues[propertyName];
                var newVal = entry.CurrentValues[propertyName];
                if (oldVal != null && newVal != null && !Equals(oldVal, newVal))
                {
                    XmlElement propertyNode = doc.CreateElement(propertyName.Name);
                    propertyNode.SetAttribute("NewValue", newVal.ToString());
                    propertyNode.SetAttribute("OldValue", oldVal.ToString());
                    tableNode.AppendChild(propertyNode);
                }
            }
        }
        

        
        #region Entity Framework

        #endregion
        #region dispose method
        private bool disposed = false;

        /// <summary>
        /// Object disposing
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
    
}
