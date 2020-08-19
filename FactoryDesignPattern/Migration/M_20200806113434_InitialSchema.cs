using FluentMigrator;

namespace FactoryDesignPattern.Migration
{
    [Migration(20200806113434)]
    public class M_20200806113434_InitialSchema : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Employees")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FirstName").AsString(50).NotNullable()
                .WithColumn("LastName").AsString(50).NotNullable()
                .WithColumn("HourlyPay").AsInt32().NotNullable()
                .WithColumn("Bonus").AsInt32().NotNullable()
                .WithColumn("EmployeeType").AsInt32().NotNullable()
                ;
        }

        public override void Down()
        {
        }
    }
}