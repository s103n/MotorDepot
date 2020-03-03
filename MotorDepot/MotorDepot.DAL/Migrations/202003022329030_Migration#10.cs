namespace MotorDepot.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Flights", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FlightRequests", "DriverId", "dbo.AspNetUsers");
            DropIndex("dbo.Flights", new[] { "CreatorId" });
            DropIndex("dbo.FlightRequests", new[] { "DriverId" });
            AlterColumn("dbo.AutoBrands", "Name", c => c.String());
            AlterColumn("dbo.Autoes", "Model", c => c.String());
            AlterColumn("dbo.Autoes", "Numbers", c => c.String());
            AlterColumn("dbo.Flights", "Description", c => c.String());
            AlterColumn("dbo.Flights", "DeparturePlace", c => c.String());
            AlterColumn("dbo.Flights", "ArrivalPlace", c => c.String());
            AlterColumn("dbo.Flights", "CreatorId", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.FlightRequests", "DriverId", c => c.String(maxLength: 128));
            AlterColumn("dbo.FlightRequests", "Description", c => c.String());
            AlterColumn("dbo.AutoTypes", "Name", c => c.String());
            AlterColumn("dbo.FlightRequestStatus", "Name", c => c.String());
            AlterColumn("dbo.FlightStatus", "Name", c => c.String());
            AlterColumn("dbo.AutoStatus", "Name", c => c.String());
            CreateIndex("dbo.Flights", "CreatorId");
            CreateIndex("dbo.FlightRequests", "DriverId");
            AddForeignKey("dbo.Flights", "CreatorId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FlightRequests", "DriverId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FlightRequests", "DriverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Flights", "CreatorId", "dbo.AspNetUsers");
            DropIndex("dbo.FlightRequests", new[] { "DriverId" });
            DropIndex("dbo.Flights", new[] { "CreatorId" });
            AlterColumn("dbo.AutoStatus", "Name", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.FlightStatus", "Name", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.FlightRequestStatus", "Name", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.AutoTypes", "Name", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.FlightRequests", "Description", c => c.String(maxLength: 1024));
            AlterColumn("dbo.FlightRequests", "DriverId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 24));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 24));
            AlterColumn("dbo.Flights", "CreatorId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Flights", "ArrivalPlace", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.Flights", "DeparturePlace", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.Flights", "Description", c => c.String(maxLength: 1024));
            AlterColumn("dbo.Autoes", "Numbers", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.Autoes", "Model", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.AutoBrands", "Name", c => c.String(nullable: false, maxLength: 32));
            CreateIndex("dbo.FlightRequests", "DriverId");
            CreateIndex("dbo.Flights", "CreatorId");
            AddForeignKey("dbo.FlightRequests", "DriverId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Flights", "CreatorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
