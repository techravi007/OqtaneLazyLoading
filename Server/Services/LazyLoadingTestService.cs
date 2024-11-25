using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Security;
using Oqtane.Shared;
using LLM.Module.LazyLoadingTest.Repository;

namespace LLM.Module.LazyLoadingTest.Services
{
    public class ServerLazyLoadingTestService : ILazyLoadingTestService, ITransientService
    {
        private readonly ILazyLoadingTestRepository _LazyLoadingTestRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerLazyLoadingTestService(ILazyLoadingTestRepository LazyLoadingTestRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _LazyLoadingTestRepository = LazyLoadingTestRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public async Task<List<Models.LazyLoadingTest>> GetLazyLoadingTestsAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return (await _LazyLoadingTestRepository.GetLazyLoadingTestsAsync(ModuleId)).ToList();
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized LazyLoadingTest Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public async Task<Models.LazyLoadingTest> GetLazyLoadingTestAsync(int LazyLoadingTestId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return await _LazyLoadingTestRepository.GetLazyLoadingTestAsync(LazyLoadingTestId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized LazyLoadingTest Get Attempt {LazyLoadingTestId} {ModuleId}", LazyLoadingTestId, ModuleId);
                return null;
            }
        }

        public async Task<Models.LazyLoadingTest> AddLazyLoadingTestAsync(Models.LazyLoadingTest LazyLoadingTest)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, LazyLoadingTest.ModuleId, PermissionNames.Edit))
            {
                LazyLoadingTest = await _LazyLoadingTestRepository.AddLazyLoadingTestAsync(LazyLoadingTest);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "LazyLoadingTest Added {LazyLoadingTest}", LazyLoadingTest);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized LazyLoadingTest Add Attempt {LazyLoadingTest}", LazyLoadingTest);
                LazyLoadingTest = null;
            }
            return LazyLoadingTest;
        }

        public async Task<Models.LazyLoadingTest> UpdateLazyLoadingTestAsync(Models.LazyLoadingTest LazyLoadingTest)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, LazyLoadingTest.ModuleId, PermissionNames.Edit))
            {
                LazyLoadingTest = await _LazyLoadingTestRepository.UpdateLazyLoadingTestAsync(LazyLoadingTest);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "LazyLoadingTest Updated {LazyLoadingTest}", LazyLoadingTest);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized LazyLoadingTest Update Attempt {LazyLoadingTest}", LazyLoadingTest);
                LazyLoadingTest = null;
            }
            return LazyLoadingTest;
        }

        public async Task DeleteLazyLoadingTestAsync(int LazyLoadingTestId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                await _LazyLoadingTestRepository.DeleteLazyLoadingTestAsync(LazyLoadingTestId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "LazyLoadingTest Deleted {LazyLoadingTestId}", LazyLoadingTestId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized LazyLoadingTest Delete Attempt {LazyLoadingTestId} {ModuleId}", LazyLoadingTestId, ModuleId);
            }
        }
    }
}
