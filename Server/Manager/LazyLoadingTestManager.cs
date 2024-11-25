using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using LLM.Module.LazyLoadingTest.Repository;
using System.Threading.Tasks;

namespace LLM.Module.LazyLoadingTest.Manager
{
    public class LazyLoadingTestManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly ILazyLoadingTestRepository _LazyLoadingTestRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public LazyLoadingTestManager(ILazyLoadingTestRepository LazyLoadingTestRepository, IDBContextDependencies DBContextDependencies)
        {
            _LazyLoadingTestRepository = LazyLoadingTestRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new LazyLoadingTestContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new LazyLoadingTestContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.LazyLoadingTest> LazyLoadingTests = _LazyLoadingTestRepository.GetLazyLoadingTests(module.ModuleId).ToList();
            if (LazyLoadingTests != null)
            {
                content = JsonSerializer.Serialize(LazyLoadingTests);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.LazyLoadingTest> LazyLoadingTests = null;
            if (!string.IsNullOrEmpty(content))
            {
                LazyLoadingTests = JsonSerializer.Deserialize<List<Models.LazyLoadingTest>>(content);
            }
            if (LazyLoadingTests != null)
            {
                foreach(var LazyLoadingTest in LazyLoadingTests)
                {
                    _LazyLoadingTestRepository.AddLazyLoadingTest(new Models.LazyLoadingTest { ModuleId = module.ModuleId, Name = LazyLoadingTest.Name });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var LazyLoadingTest in _LazyLoadingTestRepository.GetLazyLoadingTests(pageModule.ModuleId))
           {
               if (LazyLoadingTest.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "LLMLazyLoadingTest",
                       EntityId = LazyLoadingTest.LazyLoadingTestId.ToString(),
                       Title = LazyLoadingTest.Name,
                       Body = LazyLoadingTest.Name,
                       ContentModifiedBy = LazyLoadingTest.ModifiedBy,
                       ContentModifiedOn = LazyLoadingTest.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
