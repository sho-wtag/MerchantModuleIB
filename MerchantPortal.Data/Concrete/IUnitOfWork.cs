
using MerchantPortal.Data.Repositories;
using System;

namespace MerchantPortal.Data.Concrete
{
    public interface IUnitOfWork : IDisposable
    {
        AgentRepository AgentRepo { get; }

        BankRepository BankRepo { get; }
        BranchRepository BranchRepo { get; }

        CountryRepository CountryRepo { get; }
        CurrencyRepository CurrencyRepo { get; }

        MerchantPortalDBContext MTDBContext { get; }
        MerchantRepository MerchantRepo { get; }
        TerminalRepository TerminalRepo { get; }
        MctCommissionSetupRepository MctCommissionSetupRepo { get; }
        MctVATSetupRepository MctVATSetupRepo { get; }
        MctGLSetupRepository MctGLSetupRepo { get; }
        ApplicationUserRepository ApplicationUserRepo { get; }
        ApprovalViewRepository ApprovalViewrepo { get; }
        ApplicationRoleRepository ApplicationRoleRepo { get; }
        SettlementRuleRepository SettlementRuleRepo { get; }
        MerchantTransactionRepository MerchantTransactionRepo { get; }
        ControllerActionMappingRepository ControllerActionMappingRepo { get; }
        RolePermissionRepository RolePermissionRepo { get; }
        TransactionStatusRepository TransactionStatusRepo { get; }
        void Save();
        void SaveAsync();
        void MakeAudit(bool isAudit, int userID, int menuId);
    }
}
