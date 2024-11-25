using System.Collections.Generic;
using System.Threading.Tasks;

namespace LLM.Module.LazyLoadingTest.Repository
{
    public interface ILazyLoadingTestRepository
    {
        IEnumerable<Models.LazyLoadingTest> GetLazyLoadingTests(int ModuleId);
        Models.LazyLoadingTest GetLazyLoadingTest(int LazyLoadingTestId);
        Models.LazyLoadingTest GetLazyLoadingTest(int LazyLoadingTestId, bool tracking);
        Models.LazyLoadingTest AddLazyLoadingTest(Models.LazyLoadingTest LazyLoadingTest);
        Models.LazyLoadingTest UpdateLazyLoadingTest(Models.LazyLoadingTest LazyLoadingTest);
        void DeleteLazyLoadingTest(int LazyLoadingTestId);

        Task<IEnumerable<Models.LazyLoadingTest>> GetLazyLoadingTestsAsync(int ModuleId);
        Task<Models.LazyLoadingTest> GetLazyLoadingTestAsync(int LazyLoadingTestId);
        Task<Models.LazyLoadingTest> GetLazyLoadingTestAsync(int LazyLoadingTestId, bool tracking);
        Task<Models.LazyLoadingTest> AddLazyLoadingTestAsync(Models.LazyLoadingTest LazyLoadingTest);
        Task<Models.LazyLoadingTest> UpdateLazyLoadingTestAsync(Models.LazyLoadingTest LazyLoadingTest);
        Task DeleteLazyLoadingTestAsync(int LazyLoadingTestId);
    }
}
