namespace SoporteEnLinea.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableReservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdEmployee = c.Int(nullable: false),
                        IdUser = c.String(),
                        TurnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.IdEmployee, cascadeDelete: true)
                .Index(t => t.IdEmployee);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "IdEmployee", "dbo.Employees");
            DropIndex("dbo.Reservations", new[] { "IdEmployee" });
            DropTable("dbo.Reservations");
        }
    }
}
