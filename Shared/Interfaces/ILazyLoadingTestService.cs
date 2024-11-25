using System.Collections.Generic;
using System.Threading.Tasks;

namespace LLM.Module.LazyLoadingTest.Services
{
    public interface ILazyLoadingTestService 
    {
        Task<List<Models.LazyLoadingTest>> GetLazyLoadingTestsAsync(int ModuleId);

        Task<Models.LazyLoadingTest> GetLazyLoadingTestAsync(int LazyLoadingTestId, int ModuleId);

        Task<Models.LazyLoadingTest> AddLazyLoadingTestAsync(Models.LazyLoadingTest LazyLoadingTest);

        Task<Models.LazyLoadingTest> UpdateLazyLoadingTestAsync(Models.LazyLoadingTest LazyLoadingTest);

        Task DeleteLazyLoadingTestAsync(int LazyLoadingTestId, int ModuleId);
    }
}
