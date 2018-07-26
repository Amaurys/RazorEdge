namespace SoporteEnLinea.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Uriel25072018 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reservations", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "Status", c => c.String());
        }
    }
}
