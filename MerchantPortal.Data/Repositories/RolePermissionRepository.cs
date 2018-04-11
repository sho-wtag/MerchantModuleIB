using MerchantPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantPortal.Data.Repositories
{
    public class RolePermissionRepository
    {
        private MerchantPortalDBContext _dBContext;

        public RolePermissionRepository(MerchantPortalDBContext dbContext)
        {
            this._dBContext = dbContext;
        }
        public IEnumerable<RolePermission> GetAll()
        {
            return this._dBContext.RolePermission.ToList();
        }
        public RolePermission GetById(Int64? id)
        {
            return this._dBContext.RolePermission.SingleOrDefault(m => m.Id == id);
        }
        public void Add(RolePermission RolePermission)
        {
            this._dBContext.Add(RolePermission);
        }
        public void Edit(RolePermission RolePermission)
        {
            this._dBContext.Attach(RolePermission);
            this._dBContext.Entry(RolePermission).State = EntityState.Modified;
        }
        public void Delete(RolePermission RolePermission)
        {
            this._dBContext.Attach(RolePermission);
            this._dBContext.Entry(RolePermission).State = EntityState.Modified;
        }
        public int HasParentPermission(Int64 CAId, Int64 roleId)
        {
            Menu menu = (from rp in this._dBContext.RolePermission
                         join ca in (from ca in this._dBContext.ControllerActionMapping
                                     join m in this._dBContext.Menu on ca.MenuId equals m.Id
                                     where m.IsDeleted == false && ca.Id == CAId
                                     select m
                                     ) on rp.ControllerActionId equals ca.Parent_Id
                         where rp.RoleId == roleId
                         select ca).SingleOrDefault();
            if (menu != null)
                return menu.Parent_Id;
            return 0;
        }
    }
}
