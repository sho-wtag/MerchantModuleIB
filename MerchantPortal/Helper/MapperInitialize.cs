using AutoMapper;
using MerchantPortal.Data.Models;
using MerchantPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Helper
{
    public class MapperInitialize
    {
        public static void ModelMap()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BankViewModel, Bank>();
                cfg.CreateMap<Bank, BankViewModel>(); // Added by Moinul
                cfg.CreateMap<ApplicationRoleViewModel, ApplicationRole>();// added by debashish 
                cfg.CreateMap<ApplicationRole, ApplicationRoleViewModel>();// added by debashish 
                cfg.CreateMap<SettlementRuleViewModel, SettlementRule>();// added by hasan 
                cfg.CreateMap<SettlementRule, SettlementRuleViewModel>();// added by hasan 
                cfg.CreateMap<TransactionViewModel, Transaction>();// added by hasan 
                cfg.CreateMap<Transaction, TransactionViewModel>();// added by hasan 
                
            });
        }
    }
}
