using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using LLM.Module.LazyLoadingTest.Migrations.EntityBuilders;
using LLM.Module.LazyLoadingTest.Repository;

namespace LLM.Module.LazyLoadingTest.Migrations
{
    [DbContext(typeof(LazyLoadingTestContext))]
    [Migration("LLM.Module.LazyLoadingTest.01.00.00.00")]
    public class InitializeModule : MultiDatabaseMigration
    {
        public InitializeModule(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new LazyLoadingTestEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Create();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new LazyLoadingTestEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Drop();
        }
    }
}
