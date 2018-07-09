namespace SoporteEnLinea.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddadressField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Numero", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Customers", "Direccion", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Customers", "PhoneNumber");
            DropColumn("dbo.Customers", "Adress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Adress", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Customers", "PhoneNumber", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Customers", "Direccion");
            DropColumn("dbo.Customers", "Numero");
        }
    }
}
