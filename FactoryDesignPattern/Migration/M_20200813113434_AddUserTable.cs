using FluentMigrator;

namespace FactoryDesignPattern.Migration
{
    [Migration(20200813113434)]
    public class M_20200813113434_AddUserTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FullName").AsString(150).NotNullable()
                .WithColumn("Email").AsString(150).NotNullable()
                .WithColumn("Password").AsString(150).NotNullable()
                ;
        }

        public override void Down()
        {
        }
    }
}