using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace bits.SqlClient
{
    public class SqlManager : IDisposable
    {
        #region Variable(s)
        private int _connectionTimeOut = 120;
        //private static SqlManager _manager;
        private SqlManager _manager;
        private SqlConnection _connection;
        #endregion

        #region Constructor(s)
        //private SqlManager()
        //{

        //}
        #endregion

        #region Property(s)
        
        /*  Previous Code
         public static SqlManager Manager
         {
            get
            {
                if (_manager == null)
                {
                    _manager = new SqlManager();
                }
                return _manager;
            }
        }*/

        //public static SqlManager Manager
        //{
        //    get
        //    {
        //        //_manager = new SqlManager();
        //        //return _manager;                
        //        return new SqlManager();
        //    }
        //}

        public int ConnectionTimeOut
        {
            set
            {
                _connectionTimeOut = value;

            }
        }
        public SqlConnection Connection
        {
            get
            {
                if (_connection == null || _connection.State != ConnectionState.Open)
                {
                    _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["tempConnection"].ConnectionString);
                }

                return _connection;
            }
        }
        private bool? HasTransaction { get; set; }
        private SqlTransaction Transaction { get; set; }
        #endregion

        #region Sql Connection and Transaction
        public void BeginTransaction(bool hasTrans)
        {
            this.HasTransaction = hasTrans;

            if (this.HasTransaction.HasValue && !this.HasTransaction.Value)
            {
                OpenConnection();
                this.Transaction = this.Connection.BeginTransaction();
            }
        }
        public void CommitTransaction()
        {
            if (this.HasTransaction.HasValue && this.HasTransaction.Value)
            {
                this.Transaction.Commit();
                this.Transaction.Dispose();
                this.CloseConnection();
            }
        }
        public void RollbackTransaction()
        {
            if (this.HasTransaction.HasValue && this.HasTransaction.Value)
            {
                this.Transaction.Rollback();
                this.Transaction.Dispose();
                this.CloseConnection();
            }
        }

        private void OpenConnection()
        {
            if (this.Connection != null && this.Connection.State == ConnectionState.Open)
            {
                throw new Exception("SqlManager is not handled properly.");
            }

            //string connString = ConfigurationManager.ConnectionStrings["tempConnection"].ConnectionString;
            //this.Connection = new SqlConnection(connString);
            this.Connection.Open();
        }
        private void CloseConnection()
        {
            if (this.Connection.State == ConnectionState.Open)
            {
                this.Connection.Close();
                this.Connection.Dispose();
            }
        }
        #endregion

        #region DB Operation(s)
        //private int ExecuteNonQuery(string sql)
        //{
        //    int records = 0;
        //    using (SqlCommand sqlCommand = new SqlCommand())
        //    {
        //        try
        //        {
        //            if (this.Connection == null || this.Connection.State != ConnectionState.Open) this.OpenConnection();

        //            sqlCommand.Connection = this.Connection;
        //            sqlCommand.CommandText = sql;
        //            sqlCommand.CommandType = CommandType.Text;
        //            sqlCommand.CommandTimeout = _connectionTimeOut;
        //            if (this.HasTransaction.HasValue && this.HasTransaction.Value) sqlCommand.Transaction = this.Transaction;

        //            records = sqlCommand.ExecuteNonQuery();
        //        }
        //        catch (Exception exception)
        //        {
        //            throw exception;
        //        }
        //        finally
        //        {
        //            if (!this.HasTransaction.HasValue || (this.HasTransaction.HasValue && !this.HasTransaction.Value))
        //                if (this.Connection != null && this.Connection.State == ConnectionState.Open) this.CloseConnection();
        //        }
        //    }
        //    return records;
        //}

        /// <summary>
        /// Create By : Tarun Kumar
        /// Create Date: 02-02-2015
        /// Description: SQL Bulk Insert
        /// </summary>
        /// <param name="Sourcedata"></param>
        /// <returns></returns>
        public int BulkInsert(DataTable Sourcedata)
        {
            int records = 0;
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                try
                {
                    if (this.Connection == null || this.Connection.State != ConnectionState.Open) this.OpenConnection();
                    sqlCommand.Connection = this.Connection;
                  //  sqlCommand.CommandTimeout = 0;//_connectionTimeOut;
                  
                    if (this.HasTransaction.HasValue && this.HasTransaction.Value) sqlCommand.Transaction = this.Transaction;

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(this.Connection))
                    {
                        bulkCopy.BulkCopyTimeout = 0;
                        bulkCopy.DestinationTableName = "dbo.Gen_Address";
                        bulkCopy.WriteToServer(Sourcedata);
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    if (!this.HasTransaction.HasValue || (this.HasTransaction.HasValue && !this.HasTransaction.Value))
                        if (this.Connection != null && this.Connection.State == ConnectionState.Open) this.CloseConnection();
                }
            }
            return records;
        }

        public int ExecuteNonQuery(string procedureName, List<SqlParameter> paramList = null)
        {
            int records = 0;
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                try
                {
                    if (this.Connection == null || this.Connection.State != ConnectionState.Open) this.OpenConnection();

                    sqlCommand.Connection = this.Connection;
                    sqlCommand.CommandText = procedureName;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = _connectionTimeOut;
                    if (this.HasTransaction.HasValue && this.HasTransaction.Value) sqlCommand.Transaction = this.Transaction;

                    foreach (SqlParameter item in paramList)
                    {
                        sqlCommand.Parameters.Add(item);
                    }

                    records = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    if (!this.HasTransaction.HasValue || (this.HasTransaction.HasValue && !this.HasTransaction.Value))
                        if (this.Connection != null && this.Connection.State == ConnectionState.Open) this.CloseConnection();
                }
            }
            return records;
        }
        /// <summary>
        ///  Create By: Tarun && Muzahid
        ///  Create Date: 20-03-2015
        ///  Description: Single value Return
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public string ExecuteScalarValue(string procedureName, List<SqlParameter> paramList = null)
        {
            string result = String.Empty;
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                try
                {
                    if (this.Connection == null || this.Connection.State != ConnectionState.Open) this.OpenConnection();

                    sqlCommand.Connection = this.Connection;
                    sqlCommand.CommandText = procedureName;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = _connectionTimeOut;
                    if (this.HasTransaction.HasValue && this.HasTransaction.Value) sqlCommand.Transaction = this.Transaction;

                    foreach (SqlParameter item in paramList)
                    {
                        sqlCommand.Parameters.Add(item);
                    }
                    result = sqlCommand.ExecuteScalar().ToString();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    if (!this.HasTransaction.HasValue || (this.HasTransaction.HasValue && !this.HasTransaction.Value))
                        if (this.Connection != null && this.Connection.State == ConnectionState.Open) this.CloseConnection();
                }
            }
            return result;
        }
        public object ExecuteNonQuerySave(string procedureName, List<SqlParameter> paramList, out int id)
        {
            int records = 0;
            id = 0;

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                try
                {
                    if (this.Connection == null || this.Connection.State != ConnectionState.Open) this.OpenConnection();

                    sqlCommand.Connection = this.Connection;
                    sqlCommand.CommandText = procedureName;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = _connectionTimeOut;
                    if (this.HasTransaction.HasValue && this.HasTransaction.Value) sqlCommand.Transaction = this.Transaction;

                    foreach (SqlParameter item in paramList)
                    {
                        sqlCommand.Parameters.Add(item);
                    }

                    sqlCommand.Parameters.Add("@ID", SqlDbType.Int);
                    sqlCommand.Parameters["@ID"].Direction = ParameterDirection.Output;

                    records = sqlCommand.ExecuteNonQuery();

                    id = Convert.ToInt32(sqlCommand.Parameters["@ID"].Value);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    if (!this.HasTransaction.HasValue || (this.HasTransaction.HasValue && !this.HasTransaction.Value))
                        if (this.Connection != null && this.Connection.State == ConnectionState.Open) this.CloseConnection();
                }
            }
            return records;
        }

        //private object ExecuteScalar(string sql)
        //{
        //    object result = null;
        //    using (SqlCommand sqlCommand = new SqlCommand())
        //    {
        //        try
        //        {
        //            if (this.Connection == null || this.Connection.State != ConnectionState.Open) this.OpenConnection();

        //            sqlCommand.Connection = this.Connection;
        //            sqlCommand.CommandText = sql;
        //            sqlCommand.CommandType = CommandType.Text;
        //            sqlCommand.CommandTimeout = _connectionTimeOut;
        //            if (this.HasTransaction.HasValue && this.HasTransaction.Value) sqlCommand.Transaction = this.Transaction;

        //            result = sqlCommand.ExecuteScalar();
        //        }
        //        catch (Exception exception)
        //        {
        //            throw exception;
        //        }
        //        finally
        //        {
        //            if (!this.HasTransaction.HasValue || (this.HasTransaction.HasValue && !this.HasTransaction.Value))
        //                if (this.Connection != null && this.Connection.State == ConnectionState.Open) this.CloseConnection();
        //        }
        //    }
        //    return result;
        //}
        public object ExecuteScalar(string procedureName, List<SqlParameter> paramList = null)
        {
            object result = null;
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                try
                {
                    if (this.Connection == null || this.Connection.State != ConnectionState.Open) this.OpenConnection();

                    sqlCommand.Connection = this.Connection;
                    sqlCommand.CommandText = procedureName;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = _connectionTimeOut;
                    if (this.HasTransaction.HasValue && this.HasTransaction.Value) sqlCommand.Transaction = this.Transaction;

                    foreach (SqlParameter item in paramList)
                    {
                        sqlCommand.Parameters.Add(item);
                    }

                    result = sqlCommand.ExecuteScalar();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    if (!this.HasTransaction.HasValue || (this.HasTransaction.HasValue && !this.HasTransaction.Value))
                        if (this.Connection != null && this.Connection.State == ConnectionState.Open) this.CloseConnection();
                }
            }
            return result;
        }

        //private DataTable FillDataTable(string sql)
        //{
        //    DataTable dtResult = null;
        //    SqlDataAdapter dbAdapter = null;

        //    using (SqlCommand sqlCommand = new SqlCommand())
        //    {
        //        try
        //        {
        //            if (this.Connection == null || this.Connection.State != ConnectionState.Open) this.OpenConnection();

        //            sqlCommand.Connection = this.Connection;
        //            sqlCommand.CommandText = sql;
        //            sqlCommand.CommandType = CommandType.Text;
        //            sqlCommand.CommandTimeout = _connectionTimeOut;
        //            if (this.HasTransaction.HasValue && this.HasTransaction.Value) sqlCommand.Transaction = this.Transaction;

        //            dbAdapter = new SqlDataAdapter(sqlCommand);
        //            dtResult = new DataTable();

        //            dbAdapter.Fill(dtResult);
        //        }
        //        catch (Exception exception)
        //        {
        //            throw exception;
        //        }
        //        finally
        //        {
        //            if (!this.HasTransaction.HasValue || (this.HasTransaction.HasValue && !this.HasTransaction.Value))
        //                if (this.Connection != null && this.Connection.State == ConnectionState.Open) this.CloseConnection();
        //            dbAdapter.Dispose();
        //        }
        //    }
        //    return dtResult;
        //}
        public DataTable FillDataTable(string procedureName, params KeyValuePair<string, object>[] paramList)
        {
            DataTable dtResult = null;
            SqlDataAdapter dbAdapter = null;

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                try
                {
                    if (this.Connection == null || this.Connection.State != ConnectionState.Open) this.OpenConnection();

                    sqlCommand.Connection = this.Connection;
                    sqlCommand.CommandText = procedureName;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = _connectionTimeOut;

                    if (this.HasTransaction.HasValue && this.HasTransaction.Value) sqlCommand.Transaction = this.Transaction;

                    if (paramList != null)
                    {
                        foreach (var sqlParam in paramList.Select(item => new SqlParameter(item.Key, item.Value)))
                        {
                            sqlCommand.Parameters.Add(sqlParam);
                        }
                    }

                    dbAdapter = new SqlDataAdapter(sqlCommand);
                    dtResult = new DataTable(procedureName);

                    dbAdapter.Fill(dtResult);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    if (!this.HasTransaction.HasValue || (this.HasTransaction.HasValue && !this.HasTransaction.Value))
                        if (this.Connection != null && this.Connection.State == ConnectionState.Open) this.CloseConnection();

                    dbAdapter.Dispose();
                }
            }
            return dtResult;
        }
        public DataTable FillDataTable(string procedureName, List<SqlParameter> paramList = null)
        {
            DataTable dtResult = null;
            SqlDataAdapter dbAdapter = null;

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                try
                {
                    if (this.Connection == null || this.Connection.State != ConnectionState.Open) this.OpenConnection();

                    sqlCommand.Connection = this.Connection;
                    sqlCommand.CommandText = procedureName;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = _connectionTimeOut;

                    if (this.HasTransaction.HasValue && this.HasTransaction.Value) sqlCommand.Transaction = this.Transaction;


                    foreach (SqlParameter item in paramList)
                    {
                        sqlCommand.Parameters.Add(item);
                    }
                    dbAdapter = new SqlDataAdapter(sqlCommand);
                    dtResult = new DataTable(procedureName);

                    dbAdapter.Fill(dtResult);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    if (!this.HasTransaction.HasValue || (this.HasTransaction.HasValue && !this.HasTransaction.Value))
                        if (this.Connection != null && this.Connection.State == ConnectionState.Open) this.CloseConnection();

                    dbAdapter.Dispose();
                }
            }
            return dtResult;
        }

        public DataSet FillDataSet(string procedureName, params KeyValuePair<string, object>[] paramList)
        {
            DataSet dtResult = null;
            SqlDataAdapter dbAdapter = null;

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                try
                {
                    if (this.Connection == null || this.Connection.State != ConnectionState.Open) this.OpenConnection();

                    sqlCommand.Connection = this.Connection;
                    sqlCommand.CommandText = procedureName;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = _connectionTimeOut;

                    if (this.HasTransaction.HasValue && this.HasTransaction.Value) sqlCommand.Transaction = this.Transaction;

                    if (paramList != null)
                    {
                        foreach (var sqlParam in paramList.Select(item => new SqlParameter(item.Key, item.Value)))
                        {
                            sqlCommand.Parameters.Add(sqlParam);
                        }
                    }

                    dbAdapter = new SqlDataAdapter(sqlCommand);
                    dtResult = new DataSet(procedureName);

                    dbAdapter.Fill(dtResult);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    if (!this.HasTransaction.HasValue || (this.HasTransaction.HasValue && !this.HasTransaction.Value))
                        if (this.Connection != null && this.Connection.State == ConnectionState.Open) this.CloseConnection();

                    dbAdapter.Dispose();
                }
            }
            return dtResult;
        }
        public DataSet FillDataSet(string procedureName, List<SqlParameter> paramList = null)
        {
            DataSet dtResult = null;
            SqlDataAdapter dbAdapter = null;

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                try
                {
                    if (this.Connection == null || this.Connection.State != ConnectionState.Open) this.OpenConnection();

                    sqlCommand.Connection = this.Connection;
                    sqlCommand.CommandText = procedureName;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = _connectionTimeOut;

                    if (this.HasTransaction.HasValue && this.HasTransaction.Value) sqlCommand.Transaction = this.Transaction;


                    foreach (SqlParameter item in paramList)
                    {
                        sqlCommand.Parameters.Add(item);
                    }
                    dbAdapter = new SqlDataAdapter(sqlCommand);
                    dtResult = new DataSet(procedureName);

                    dbAdapter.Fill(dtResult);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    if (!this.HasTransaction.HasValue || (this.HasTransaction.HasValue && !this.HasTransaction.Value))
                        if (this.Connection != null && this.Connection.State == ConnectionState.Open) this.CloseConnection();

                    dbAdapter.Dispose();
                }
            }
            return dtResult;
        }
        #endregion

        //Developer: Abdullah Al-Muzahid
        //Date: 28th December 2014
        //This method is used to fill and return datatable by using a table valued parameter

        public DataTable FillDataTable(string procedureName, string tableValuedParameterName, DataTable tableValue)
        {
            try
            {
                List<SqlParameter> paramList = new List<SqlParameter>();


                SqlParameter tvpParam = new SqlParameter(tableValuedParameterName, tableValue);
                tvpParam.SqlDbType = SqlDbType.Structured;
                paramList.Add(tvpParam);
                return this.FillDataTable("Trn_GetTransactionsForReconciliation", paramList);
            }
            catch
            {
                throw;
            }
        }


        #region Dispose
        public void Dispose()
        {
            this.HasTransaction = null;
            this.CloseConnection();
            this.Transaction.Dispose();            
        }
        #endregion
    }
}
