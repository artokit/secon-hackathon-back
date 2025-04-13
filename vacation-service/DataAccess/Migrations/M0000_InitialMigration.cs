using FluentMigrator;

namespace DataAccess.Migrations;

[Migration(0)]
public class M0000_InitialMigration : Migration
{
    public override void Up()
    {
        
        Create.Table("users")
            .WithColumn("id").AsGuid().PrimaryKey().WithDefaultValue(RawSql.Insert("gen_random_uuid()"))
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("surname").AsString().NotNullable()
            .WithColumn("patronymic").AsString().Nullable()
            .WithColumn("image_name").AsString().Nullable()
            .WithColumn("hashed_password").AsString().NotNullable()
            .WithColumn("phone").AsString().Nullable()
            .WithColumn("email").AsString().NotNullable().Unique()
            .WithColumn("telegram_username").AsString().Nullable()
            .WithColumn("department_id").AsGuid().Nullable()
            .WithColumn("role").AsString().NotNullable()
            .WithColumn("hiring_date").AsDateTime().NotNullable().WithDefaultValue("now()")
            .WithColumn("position_name").AsString().Nullable();
        
        Create.Table("departments")
            .WithColumn("id").AsGuid().PrimaryKey().WithDefaultValue(RawSql.Insert("gen_random_uuid()"))
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("description").AsString().Nullable()
            .WithColumn("supervisor_id").AsGuid().Nullable()
            .WithColumn("parent_department_id").AsGuid().Nullable()
            .WithColumn("role").AsString().NotNullable()
            .WithColumn("image_name").AsString().Nullable();

        Create.Table("orders")
            .WithColumn("id").AsGuid().PrimaryKey().WithDefaultValue(RawSql.Insert("gen_random_uuid()"))
            .WithColumn("creator_id").AsGuid().NotNullable()
            .WithColumn("department_id").AsGuid().NotNullable()
            .WithColumn("created_data").AsDateTime().NotNullable()
            .WithColumn("status").AsString().Nullable();

        Create.Table("requests")
            .WithColumn("id").AsGuid().PrimaryKey().WithDefaultValue(RawSql.Insert("gen_random_uuid()"))
            .WithColumn("user_id").AsGuid().NotNullable()
            .WithColumn("start_date").AsDateTime().NotNullable()
            .WithColumn("end_date").AsDateTime().NotNullable()
            .WithColumn("extensions_days").AsInt32().NotNullable()
            .WithColumn("fact_date").AsDateTime().NotNullable()
            .WithColumn("reason_id").AsGuid().NotNullable()
            .WithColumn("comment").AsString().Nullable()
            .WithColumn("department_id").AsGuid().NotNullable()
            .WithColumn("name").AsString().NotNullable();

        Create.Table("positions")
            .WithColumn("id").AsGuid().PrimaryKey().WithDefaultValue(RawSql.Insert("gen_random_uuid()"))
            .WithColumn("department_id").AsGuid().NotNullable()
            .WithColumn("name").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("users");
        Delete.Table("departments");
        Delete.Table("orders");
        Delete.Table("requests");
    }
}