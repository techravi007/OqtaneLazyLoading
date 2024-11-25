using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;

namespace LLM.Module.LazyLoadingTest.Services
{
    public class LazyLoadingTestService : ServiceBase, ILazyLoadingTestService, IService
    {
        public LazyLoadingTestService(IHttpClientFactory http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("LazyLoadingTest");

        public async Task<List<Models.LazyLoadingTest>> GetLazyLoadingTestsAsync(int ModuleId)
        {
            List<Models.LazyLoadingTest> LazyLoadingTests = await GetJsonAsync<List<Models.LazyLoadingTest>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.LazyLoadingTest>().ToList());
            return LazyLoadingTests.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.LazyLoadingTest> GetLazyLoadingTestAsync(int LazyLoadingTestId, int ModuleId)
        {
            return await GetJsonAsync<Models.LazyLoadingTest>(CreateAuthorizationPolicyUrl($"{Apiurl}/{LazyLoadingTestId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.LazyLoadingTest> AddLazyLoadingTestAsync(Models.LazyLoadingTest LazyLoadingTest)
        {
            return await PostJsonAsync<Models.LazyLoadingTest>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, LazyLoadingTest.ModuleId), LazyLoadingTest);
        }

        public async Task<Models.LazyLoadingTest> UpdateLazyLoadingTestAsync(Models.LazyLoadingTest LazyLoadingTest)
        {
            return await PutJsonAsync<Models.LazyLoadingTest>(CreateAuthorizationPolicyUrl($"{Apiurl}/{LazyLoadingTest.LazyLoadingTestId}", EntityNames.Module, LazyLoadingTest.ModuleId), LazyLoadingTest);
        }

        public async Task DeleteLazyLoadingTestAsync(int LazyLoadingTestId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{LazyLoadingTestId}", EntityNames.Module, ModuleId));
        }
    }
}
