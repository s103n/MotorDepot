namespace MotorDepot.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StringId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Flights", new[] { "Driver_Id" });
            DropColumn("dbo.Flights", "DriverId");
            RenameColumn(table: "dbo.Flights", name: "Driver_Id", newName: "DriverId");
            AlterColumn("dbo.Flights", "DriverId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Flights", "DriverId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Flights", new[] { "DriverId" });
            AlterColumn("dbo.Flights", "DriverId", c => c.Int());
            RenameColumn(table: "dbo.Flights", name: "DriverId", newName: "Driver_Id");
            AddColumn("dbo.Flights", "DriverId", c => c.Int());
            CreateIndex("dbo.Flights", "Driver_Id");
        }
    }
}
