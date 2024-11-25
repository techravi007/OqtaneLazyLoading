using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace LLM.Module.LazyLoadingTest.Migrations.EntityBuilders
{
    public class LazyLoadingTestEntityBuilder : AuditableBaseEntityBuilder<LazyLoadingTestEntityBuilder>
    {
        private const string _entityTableName = "LLMLazyLoadingTest";
        private readonly PrimaryKey<LazyLoadingTestEntityBuilder> _primaryKey = new("PK_LLMLazyLoadingTest", x => x.LazyLoadingTestId);
        private readonly ForeignKey<LazyLoadingTestEntityBuilder> _moduleForeignKey = new("FK_LLMLazyLoadingTest_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public LazyLoadingTestEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override LazyLoadingTestEntityBuilder BuildTable(ColumnsBuilder table)
        {
            LazyLoadingTestId = AddAutoIncrementColumn(table,"LazyLoadingTestId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> LazyLoadingTestId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
