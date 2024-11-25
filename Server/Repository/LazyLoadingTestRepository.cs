using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using System.Threading.Tasks;

namespace LLM.Module.LazyLoadingTest.Repository
{
    public class LazyLoadingTestRepository : ILazyLoadingTestRepository, ITransientService
    {
        private readonly IDbContextFactory<LazyLoadingTestContext> _factory;

        public LazyLoadingTestRepository(IDbContextFactory<LazyLoadingTestContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.LazyLoadingTest> GetLazyLoadingTests(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.LazyLoadingTest.Where(item => item.ModuleId == ModuleId).ToList();
        }

        public Models.LazyLoadingTest GetLazyLoadingTest(int LazyLoadingTestId)
        {
            return GetLazyLoadingTest(LazyLoadingTestId, true);
        }

        public Models.LazyLoadingTest GetLazyLoadingTest(int LazyLoadingTestId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.LazyLoadingTest.Find(LazyLoadingTestId);
            }
            else
            {
                return db.LazyLoadingTest.AsNoTracking().FirstOrDefault(item => item.LazyLoadingTestId == LazyLoadingTestId);
            }
        }

        public Models.LazyLoadingTest AddLazyLoadingTest(Models.LazyLoadingTest LazyLoadingTest)
        {
            using var db = _factory.CreateDbContext();
            db.LazyLoadingTest.Add(LazyLoadingTest);
            db.SaveChanges();
            return LazyLoadingTest;
        }

        public Models.LazyLoadingTest UpdateLazyLoadingTest(Models.LazyLoadingTest LazyLoadingTest)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(LazyLoadingTest).State = EntityState.Modified;
            db.SaveChanges();
            return LazyLoadingTest;
        }

        public void DeleteLazyLoadingTest(int LazyLoadingTestId)
        {
            using var db = _factory.CreateDbContext();
            Models.LazyLoadingTest LazyLoadingTest = db.LazyLoadingTest.Find(LazyLoadingTestId);
            db.LazyLoadingTest.Remove(LazyLoadingTest);
            db.SaveChanges();
        }


        public async Task<IEnumerable<Models.LazyLoadingTest>> GetLazyLoadingTestsAsync(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return await db.LazyLoadingTest.Where(item => item.ModuleId == ModuleId).ToListAsync();
        }

        public async Task<Models.LazyLoadingTest> GetLazyLoadingTestAsync(int LazyLoadingTestId)
        {
            return await GetLazyLoadingTestAsync(LazyLoadingTestId, true);
        }

        public async Task<Models.LazyLoadingTest> GetLazyLoadingTestAsync(int LazyLoadingTestId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return await db.LazyLoadingTest.FindAsync(LazyLoadingTestId);
            }
            else
            {
                return await db.LazyLoadingTest.AsNoTracking().FirstOrDefaultAsync(item => item.LazyLoadingTestId == LazyLoadingTestId);
            }
        }

        public async Task<Models.LazyLoadingTest> AddLazyLoadingTestAsync(Models.LazyLoadingTest LazyLoadingTest)
        {
            using var db = _factory.CreateDbContext();
            db.LazyLoadingTest.Add(LazyLoadingTest);
            await db.SaveChangesAsync();
            return LazyLoadingTest;
        }

        public async Task<Models.LazyLoadingTest> UpdateLazyLoadingTestAsync(Models.LazyLoadingTest LazyLoadingTest)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(LazyLoadingTest).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return LazyLoadingTest;
        }

        public async Task DeleteLazyLoadingTestAsync(int LazyLoadingTestId)
        {
            using var db = _factory.CreateDbContext();
            Models.LazyLoadingTest LazyLoadingTest = db.LazyLoadingTest.Find(LazyLoadingTestId);
            db.LazyLoadingTest.Remove(LazyLoadingTest);
            await db.SaveChangesAsync();
        }
    }
}
