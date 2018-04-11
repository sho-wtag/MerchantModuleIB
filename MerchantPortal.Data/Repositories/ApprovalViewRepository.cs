using System;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MerchantPortal.Data.Models;
using System.Data.Common;
using System.Data;

namespace MerchantPortal.Data.Repositories
{
    public class ApprovalViewRepository
    {
        private MerchantPortalDBContext _dBContext;

        public ApprovalViewRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<ApprovalViewModel> GetAll()
        {
            return this._dBContext.ApprovalViewModel;
        }
        public ApprovalViewModel GetById(int? id)
        {
            return this._dBContext.ApprovalViewModel.Include(d => d.Id).SingleOrDefault(m => m.Id == id);

        }

        public IEnumerable<ApprovalViewModel> ApproveMerchants(Int64? id)
        {
            List<ApprovalViewModel> approvalList;
            try
            {
                _dBContext.Database.OpenConnection();

                DbCommand dbCommand = _dBContext.Database.GetDbConnection().CreateCommand();
                dbCommand.CommandText = "proc_approve_merchant_entities";
                dbCommand.CommandType = CommandType.StoredProcedure;

                DbParameter dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = "@TerminalId";
                dbParameter.Value = id.ToString();

                dbCommand.Parameters.Add(dbParameter);

                using (var reader = dbCommand.ExecuteReader())
                {
                    approvalList = reader.MapToList<ApprovalViewModel>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dBContext.Database.CloseConnection();
            }
            return approvalList;
            // return this._dBContext.Approval;
        }

        public void Add(ApprovalViewModel Approval)
        {
            this._dBContext.Add(Approval);
        }
        public void Edit(ApprovalViewModel Approval)
        {
            this._dBContext.Attach(Approval);
            this._dBContext.Entry(Approval).State = EntityState.Modified;

        }
        public void Delete(ApprovalViewModel Approval)
        {

            this._dBContext.Attach(Approval);
            this._dBContext.Entry(Approval).State = EntityState.Modified;
        }
    }
}
