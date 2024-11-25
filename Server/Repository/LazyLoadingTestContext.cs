using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace LLM.Module.LazyLoadingTest.Repository
{
    public class LazyLoadingTestContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.LazyLoadingTest> LazyLoadingTest { get; set; }

        public LazyLoadingTestContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.LazyLoadingTest>().ToTable(ActiveDatabase.RewriteName("LLMLazyLoadingTest"));
        }
    }
}
