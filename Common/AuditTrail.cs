//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data;
////using System.Data.Entity;
//using Microsoft.EntityFrameworkCore;
//using System.Data.Entity.Core.EntityClient;
//using System.Data.Entity.Infrastructure;
//using System.Data.SqlClient;
//using System.IO;
//using System.Linq;
//using System.Xml;

//namespace Common
//{
//    /// <summary>
//    /// Usefull link: http://www.softcodearticle.com/2013/07/entity-framework-auditing-implementation/
//    /// For SQL Client : System.Data.SqlClient 4.4.2
//    /// 
//    /// </summary>
//    public class AuditTrail : IDisposable
//    {
//        private int UserID { get; set; }
//        private string SessionID { get; set; }
//        private DbContext Context { get; set; }
//        private int MenuID { get; set; }

//        private AuditTrail()
//        {

//        }
//        public AuditTrail(DbContext dbContext)
//        {
//            this.Context = dbContext;

//          //  this.UserID = SessionUtility.GetLoginUserID();
//           // this.SessionID = SessionUtility.SessionID;
//            //this.MenuID = SessionUtility.MenuID;
//        }

//        internal enum AuditActions
//        {
//            Insert = 1,
//            Update = 2,
//            Delete = 3
//        }

//        public void DoAudit()
//        {
//            var entries = this.Context.ChangeTracker.Entries()
//                .Where
//                (
//                    e => e.State == EntityState.Modified ||
//                        e.State == EntityState.Added ||
//                        e.State == EntityState.Deleted
//                );
//            if (entries.Count() > 0)
//            {
//                XmlDocument doc = new XmlDocument();
//                XmlElement parentNode = doc.CreateElement("root");

//                foreach (var entity in entries)
//                {
//                    if (entity.Entity != null)
//                    {
//                        parentNode.AppendChild(this.AuditTrailFactory(entity, doc));
//                    }
//                }
//                doc.AppendChild(parentNode);

//                StringWriter sw = new StringWriter();
//                XmlTextWriter xw = new XmlTextWriter(sw);
//                doc.WriteTo(xw);

//                string xml = sw.ToString();

//                List<SqlParameter> paramList = new List<SqlParameter>();
//                paramList.Add(new SqlParameter("UpdatedBy", this.UserID));
//                paramList.Add(new SqlParameter("SessionID", this.SessionID));
//                paramList.Add(new SqlParameter("MenuID", this.MenuID));

//                SqlParameter changeDetail = new SqlParameter("ChangeDetail", System.Data.SqlDbType.NVarChar);
//                changeDetail.Value = xml;
//                paramList.Add(changeDetail);

//                ExecuteSqlCommandSmart(this.Context.Database, "Usm_AuditTrail_Insert", paramList);
//            }
//        }
//        public DataTable GetAuditRecordByUserID(int userId)
//        {
//            List<SqlParameter> paramList = new List<SqlParameter>();
//            paramList.Add(new SqlParameter("UserId", userId));

//            return ExecuteStoredProcedureDT(this.Context.Database, "Usm_AuditTrail_GetByUserID", paramList);
//        }

//        private XmlElement AuditTrailFactory(DbEntityEntry entry, XmlDocument doc)
//        {
//            string tableName = GetTableName(entry);
//            XmlElement tableNode = doc.CreateElement(tableName);
//            tableNode.SetAttribute("AuditAction", entry.State.ToString());

//            if (entry.State == EntityState.Added)
//            {
//                GetAddedProperties(entry, tableNode, doc);
//            }
//            else if (entry.State == EntityState.Deleted)
//            {
//                GetDeletedProperties(entry, tableNode, doc);
//            }
//            else if (entry.State == EntityState.Modified)
//            {
//                GetModifiedProperties(entry, tableNode, doc);
//            }

//            return tableNode;
//        }
//        private string GetTableName(DbEntityEntry dbEntry)
//        {
//            if (dbEntry == null) return string.Empty;

//            TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
//            string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;
//            return tableName;
//        }

//        private void GetAddedProperties(DbEntityEntry entry, XmlElement tableNode, XmlDocument doc)
//        {
//            foreach (var propertyName in entry.CurrentValues.PropertyNames)
//            {
//                var newVal = entry.CurrentValues[propertyName];
//                if (newVal != null)
//                {
//                    XmlElement propertyNode = doc.CreateElement(propertyName);
//                    propertyNode.SetAttribute("NewValue", newVal.ToString());
//                    tableNode.AppendChild(propertyNode);
//                }
//            }
//        }
//        private void GetDeletedProperties(DbEntityEntry entry, XmlElement tableNode, XmlDocument doc)
//        {
//            DbPropertyValues dbValues = entry.GetDatabaseValues();
//            foreach (var propertyName in dbValues.PropertyNames)
//            {
//                var oldVal = dbValues[propertyName];
//                if (oldVal != null)
//                {
//                    XmlElement propertyNode = doc.CreateElement(propertyName);
//                    propertyNode.SetAttribute("DeletedValue", oldVal.ToString());
//                    tableNode.AppendChild(propertyNode);
//                }
//            }
//        }
//        private void GetModifiedProperties(DbEntityEntry entry, XmlElement tableNode, XmlDocument doc)
//        {
//            DbPropertyValues dbValues = entry.GetDatabaseValues();
//            foreach (var propertyName in entry.OriginalValues.PropertyNames)
//            {
//                var oldVal = dbValues[propertyName];
//                var newVal = entry.CurrentValues[propertyName];
//                if (oldVal != null && newVal != null && !Equals(oldVal, newVal))
//                {
//                    XmlElement propertyNode = doc.CreateElement(propertyName);
//                    propertyNode.SetAttribute("NewValue", newVal.ToString());
//                    propertyNode.SetAttribute("OldValue", oldVal.ToString());
//                    tableNode.AppendChild(propertyNode);
//                }
//            }
//        }


//        #region Entity Framework
//        private int ExecuteSqlCommandSmart(Database self, string storedProcedure, IEnumerable<SqlParameter> parameters = null)
//        {
//            if (self == null)
//                throw new ArgumentNullException("self");
//            if (string.IsNullOrEmpty(storedProcedure))
//                throw new ArgumentException("storedProcedure");

//            var arguments = PrepareArguments(storedProcedure, parameters);
//            return self.ExecuteSqlCommand(arguments.Item1, arguments.Item2);
//        }
//        private static IEnumerable<TElement> SqlQuerySmart<TElement>(Database self, string storedProcedure, IEnumerable<SqlParameter> parameters = null)
//        {
//            if (self == null)
//                throw new ArgumentNullException("self");
//            if (string.IsNullOrEmpty(storedProcedure))
//                throw new ArgumentException("storedProcedure");

//            var arguments = PrepareArguments(storedProcedure, parameters);
//            return self.SqlQuery<TElement>(arguments.Item1, arguments.Item2);
//        }
//        private static DataSet ExecuteStoredProcedure(Database db, string storedProcedureName, IEnumerable<SqlParameter> parameters)
//        {
//            var entityConnection = (EntityConnection)db.Connection;
//            var conn = entityConnection.StoreConnection;
//            var initialState = conn.State;

//            DataSet dataSet = new DataSet();

//            try
//            {
//                if (initialState != ConnectionState.Open)
//                    conn.Open();
//                using (var cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = storedProcedureName;
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    foreach (var parameter in parameters)
//                    {
//                        cmd.Parameters.Add(parameter);
//                    }

//                    using (var reader = cmd.ExecuteReader())
//                    {
//                        do
//                        {
//                            DataTable dt = new DataTable();
//                            dt.Load(reader);
//                            dataSet.Tables.Add(dt);
//                        }
//                        while (reader.NextResult());
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                if (initialState != ConnectionState.Open)
//                    conn.Close();
//            }
//            return dataSet;
//        }
//        private static DataTable ExecuteStoredProcedureDT(Database db, string storedProcedureName, IEnumerable<SqlParameter> parameters)
//        {
//            IDbConnection entityConnection = db.Connection;
//            var initialState = entityConnection.State;

//            DataTable dt = new DataTable();

//            try
//            {
//                if (initialState != ConnectionState.Open)
//                    entityConnection.Open();
//                using (var cmd = entityConnection.CreateCommand())
//                {
//                    cmd.CommandText = storedProcedureName;
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    foreach (var parameter in parameters)
//                    {
//                        cmd.Parameters.Add(parameter);
//                    }

//                    using (var reader = cmd.ExecuteReader())
//                    {
//                        dt.Load(reader);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                if (initialState != ConnectionState.Open)
//                    entityConnection.Close();
//            }
//            return dt;
//        }

//        private static IEnumerable SqlQuerySmart(Database self, Type elementType, string storedProcedure, IEnumerable<SqlParameter> parameters = null)
//        {
//            if (self == null)
//                throw new ArgumentNullException("self");
//            if (elementType == null)
//                throw new ArgumentNullException("elementType");
//            if (string.IsNullOrEmpty(storedProcedure))
//                throw new ArgumentException("storedProcedure");

//            var arguments = PrepareArguments(storedProcedure, parameters);
//            return self.SqlQuery(elementType, arguments.Item1, arguments.Item2);
//        }
//        private static Tuple<string, object[]> PrepareArguments(string storedProcedure, IEnumerable<SqlParameter> parameters)
//        {
//            var parameterNames = new List<string>();
//            var parameterParameters = new List<object>();

//            if (parameters != null)
//            {
//                //foreach (PropertyInfo propertyInfo in parameters.GetType().GetProperties())
//                //{
//                //    string name = "@" + propertyInfo.Name;
//                //    object value = propertyInfo.GetValue(parameters, null);

//                //    parameterNames.Add(name);
//                //    parameterParameters.Add(new SqlParameter(name, value ?? DBNull.Value));
//                //}
//                foreach (SqlParameter param in parameters)
//                {
//                    string name = string.Format("@{0}", param.ParameterName);
//                    object value = param.Value;

//                    parameterNames.Add(name);
//                    parameterParameters.Add(new SqlParameter(name, value ?? DBNull.Value));
//                }
//            }

//            if (parameterNames.Count > 0)
//                storedProcedure += " " + string.Join(", ", parameterNames);

//            return new Tuple<string, object[]>(storedProcedure, parameterParameters.ToArray());
//        }
//        #endregion

//        public void Dispose()
//        {
//            this.UserID = 0;
//            this.SessionID = string.Empty;
//            this.Context.Dispose();
//            this.MenuID = 0;
//        }
//    }
//}
