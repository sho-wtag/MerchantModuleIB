using MerchantPortal.Data.Models;
using MerchantPortal.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantPortal.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private int _userID { get; set; }
        private string _sessionID { get; set; }
        private int _menuID { get; set; }
        private string _reqPath { get; set; }
        private bool _isAudit { get; set; }
        private AuditTrails _auditTrails;
        private MerchantPortalDBContext _dbContext;


        private AgentRepository _agentRepo;
        //private SubAgentRepository _subAgentRepo;
        //private LookTypeRepository _lookTypeRepository;
        //private LookupRepository _lookupRepository;
        //private AppSettingsRepository _appSettingsRepository;

        private BankRepository _bankRepo;
        private BranchRepository _branchRepo;
        //private ChargeConfigRepository _chargeConfigRepo;
        //private CityRepository _cityRepo;
        private CountryRepository _countryRepo;
        private CurrencyRepository _currencyRepo;
        //private DistrictRepository _districtRepo;
        //private DivisionRepository _divisionRepo;
        //private MailAndMessageRepository _mailAndMessageRepo;
        //private MembershipCommissionRepository _membershipCommissionRepo;
        //private MembershipRepository _membershipRepo;
        //private PaymentModeRepository _paymentModeRepo;
        //private TestKeyRepository _testKeyRepo;
        //private TimeZoneRepository _timeZoneRepo;
        //private TransactionRepository _transactionRepo;
        //private ExchangeHouseRepository _exchangeHouseRepo;
        private AuditTrailRepository _auditTrailRepo;
        private MerchantRepository _merchantRepo;
        private TerminalRepository _terminalRepo;
        private MctCommissionSetupRepository _mctCommissionSetupRepository;
        private MctVATSetupRepository _mctVATSetupRepository;
        private MctGLSetupRepository _mctGLSetupRepository;
        private ApprovalViewRepository _approvalViewRepository;
        private SettlementRuleRepository _settlementRuleRepository;
        private ApplicationUserRepository _applicationUserRepository;
        private ApplicationRoleRepository _applicationRoleRepository;
        private MerchantTransactionRepository _merchantTransactionRepository;

        private ControllerActionMappingRepository _controllerActionMappingRepository;
        private RolePermissionRepository _rolePermissionRepository;

        private TransactionStatusRepository _transactionStatusRepository;


        public MerchantPortalDBContext MTDBContext
        {
            get
            {
                return this._dbContext;
            }

        }


        public UnitOfWork(MerchantPortalDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        #region Public properties

        public AgentRepository AgentRepo
        {
            get
            {
                if (_agentRepo == null)
                {
                    _agentRepo = new AgentRepository(_dbContext);
                }
                return _agentRepo;
            }

        }

        public BankRepository BankRepo
        {
            get
            {
                if (_bankRepo == null)
                {
                    _bankRepo = new BankRepository(_dbContext);
                }
                return _bankRepo;
            }
        }
        public BranchRepository BranchRepo
        {

            get
            {
                if (_branchRepo == null)
                {
                    _branchRepo = new BranchRepository(_dbContext);
                }
                return _branchRepo;
            }
        }

        public CountryRepository CountryRepo
        {
            get
            {
                if (_countryRepo == null) { _countryRepo = new CountryRepository(_dbContext); }
                return _countryRepo;
            }
        }
        public CurrencyRepository CurrencyRepo
        {
            get
            {
                if (_currencyRepo == null) { _currencyRepo = new CurrencyRepository(_dbContext); }
                return _currencyRepo;
            }
        }

        public AuditTrailRepository AuditTrailRepo
        {
            get
            {
                if (_auditTrailRepo == null)
                {
                    _auditTrailRepo = new AuditTrailRepository(_dbContext);
                }
                return _auditTrailRepo;
            }

        }

        public MerchantRepository MerchantRepo
        {
            get
            {
                if (_merchantRepo == null)
                {
                    _merchantRepo = new MerchantRepository(_dbContext);
                }
                return _merchantRepo;
            }
        }

        public TerminalRepository TerminalRepo
        {
            get
            {
                if (_terminalRepo == null)
                {
                    _terminalRepo = new TerminalRepository(_dbContext);
                }
                return _terminalRepo;
            }
        }



        public MctCommissionSetupRepository MctCommissionSetupRepo
        {
            get
            {
                if (_mctCommissionSetupRepository == null)
                {
                    _mctCommissionSetupRepository = new MctCommissionSetupRepository(_dbContext);
                }
                return _mctCommissionSetupRepository;
            }
        }

        public MctVATSetupRepository MctVATSetupRepo
        {
            get
            {
                if (_mctVATSetupRepository == null)
                {
                    _mctVATSetupRepository = new MctVATSetupRepository(_dbContext);
                }
                return _mctVATSetupRepository;
            }
        }

        public MctGLSetupRepository MctGLSetupRepo
        {
            get
            {
                if (_mctGLSetupRepository == null)
                {
                    _mctGLSetupRepository = new MctGLSetupRepository(_dbContext);
                }
                return _mctGLSetupRepository;
            }
        }


        public ApplicationUserRepository ApplicationUserRepo
        {
            get
            {
                if (_applicationUserRepository == null)
                {
                    _applicationUserRepository = new ApplicationUserRepository(_dbContext);
                }
                return _applicationUserRepository;
            }
        }

        public ApprovalViewRepository ApprovalViewrepo
        {
            get
            {
                if (_approvalViewRepository == null)
                {
                    _approvalViewRepository = new ApprovalViewRepository(_dbContext);
                }
                return _approvalViewRepository;
            }
        }
        public SettlementRuleRepository SettlementRuleRepo
        {
            get
            {
                if (_settlementRuleRepository == null)
                {
                    _settlementRuleRepository = new SettlementRuleRepository(_dbContext);
                }
                return _settlementRuleRepository;
            }
        }

        public MerchantTransactionRepository MerchantTransactionRepo
        {
            get
            {
                if (_merchantTransactionRepository == null)
                {
                    _merchantTransactionRepository = new MerchantTransactionRepository(_dbContext);
                }
                return _merchantTransactionRepository;
            }
        }

        public TransactionStatusRepository TransactionStatusRepo
        {
            get
            {
                if (_transactionStatusRepository == null)
                {
                    _transactionStatusRepository = new TransactionStatusRepository(_dbContext);
                }
                return _transactionStatusRepository;
            }
        }

        public ApplicationRoleRepository ApplicationRoleRepo
        {
            get
            {
                if (_applicationRoleRepository == null)
                {
                    _applicationRoleRepository = new ApplicationRoleRepository(_dbContext);
                }
                return _applicationRoleRepository;
            }
        }
        public ControllerActionMappingRepository ControllerActionMappingRepo
        {
            get
            {
                if (_controllerActionMappingRepository == null)
                {
                    _controllerActionMappingRepository = new ControllerActionMappingRepository(_dbContext);
                }
                return _controllerActionMappingRepository;
            }
        }
        public RolePermissionRepository RolePermissionRepo
        {
            get
            {
                if (_rolePermissionRepository == null)
                {
                    _rolePermissionRepository = new RolePermissionRepository(_dbContext);
                }
                return _rolePermissionRepository;
            }
        }


        #endregion

        #region Database change
        public async void SaveAsync()
        {
            await this._dbContext.SaveChangesAsync();
        }
        /*
        public void Save()
        {
            this._dbContext.SaveChanges();
        }
        */
        public void Save()
        {
            if (_isAudit)
            {

                this.AuditTrailRepo.Add(new AuditTrail
                {
                    MenuID = this._menuID,
                    SessionID = this._sessionID,
                    UpdatedBy = this._userID,
                    UpdatedDate = System.DateTime.Now,
                    ChangeDetail = _auditTrails.DoAudit()

                });

            }
            this._dbContext.SaveChanges();

            /*
            try
            {
                if (_isAudit)
                {

                    this.AuditTrailRepo.Add(new AuditTrail {
                        MenuID = this._menuID,
                        SessionID = this._sessionID,
                        UpdatedBy = this._userID,
                        UpdatedDate = System.DateTime.Now,
                        ChangeDetail = _auditTrails.DoAudit()
                     
                });
                    
                }
                this._dbContext.SaveChanges();
            }
            
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }
            */

        }

        public void MakeAudit(bool isAudit, int userID, int menuId)
        {
            this._userID = userID;
            this._menuID = menuId;
            this._isAudit = isAudit;

            if (_isAudit)
            {
                _auditTrails = new AuditTrails(this._dbContext);//.DoAudit();
                //_auditTrails.MenuID = this._menuID;
                //_userID = this._userID;
            }


        }
        #endregion

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
    }
}
