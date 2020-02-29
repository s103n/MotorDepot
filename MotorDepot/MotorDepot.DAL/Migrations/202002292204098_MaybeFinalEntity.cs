namespace MotorDepot.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaybeFinalEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FlightRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriverId = c.String(nullable: false, maxLength: 128),
                        DispatcherId = c.String(maxLength: 128),
                        FlightRequestStatusId = c.Int(nullable: false),
                        FlightId = c.Int(),
                        Description = c.String(maxLength: 1024),
                        Date = c.DateTime(nullable: false),
                        EnginePower = c.Int(nullable: false),
                        EngineCapacity = c.Int(nullable: false),
                        BootVolume = c.Double(nullable: false),
                        AutoTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AutoTypes", t => t.AutoTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.DispatcherId)
                .ForeignKey("dbo.AspNetUsers", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.Flights", t => t.FlightId)
                .ForeignKey("dbo.FlightRequestStatus", t => t.FlightRequestStatusId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.DispatcherId)
                .Index(t => t.FlightRequestStatusId)
                .Index(t => t.FlightId)
                .Index(t => t.AutoTypeId);
            
            CreateTable(
                "dbo.FlightRequestStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Color = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Autoes", "Numbers", c => c.String(nullable: false, maxLength: 12));
            AddColumn("dbo.Autoes", "EnginePower", c => c.Int(nullable: false));
            AddColumn("dbo.Autoes", "EngineCapacity", c => c.Double(nullable: false));
            AddColumn("dbo.Autoes", "BootVolumeMax", c => c.Double(nullable: false));
            AddColumn("dbo.Flights", "Distance", c => c.Double(nullable: false));
            AddColumn("dbo.Flights", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Flights", "CreatorId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Autoes", "Model", c => c.String(nullable: false, maxLength: 32));
            CreateIndex("dbo.Flights", "CreatorId");
            AddForeignKey("dbo.Flights", "CreatorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Flights", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Flights", "Date", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.FlightRequests", "FlightRequestStatusId", "dbo.FlightRequestStatus");
            DropForeignKey("dbo.FlightRequests", "FlightId", "dbo.Flights");
            DropForeignKey("dbo.FlightRequests", "DriverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FlightRequests", "DispatcherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FlightRequests", "AutoTypeId", "dbo.AutoTypes");
            DropForeignKey("dbo.Flights", "CreatorId", "dbo.AspNetUsers");
            DropIndex("dbo.FlightRequests", new[] { "AutoTypeId" });
            DropIndex("dbo.FlightRequests", new[] { "FlightId" });
            DropIndex("dbo.FlightRequests", new[] { "FlightRequestStatusId" });
            DropIndex("dbo.FlightRequests", new[] { "DispatcherId" });
            DropIndex("dbo.FlightRequests", new[] { "DriverId" });
            DropIndex("dbo.Flights", new[] { "CreatorId" });
            AlterColumn("dbo.Autoes", "Model", c => c.String(maxLength: 64));
            DropColumn("dbo.Flights", "CreatorId");
            DropColumn("dbo.Flights", "CreateDate");
            DropColumn("dbo.Flights", "Distance");
            DropColumn("dbo.Autoes", "BootVolumeMax");
            DropColumn("dbo.Autoes", "EngineCapacity");
            DropColumn("dbo.Autoes", "EnginePower");
            DropColumn("dbo.Autoes", "Numbers");
            DropTable("dbo.FlightRequestStatus");
            DropTable("dbo.FlightRequests");
        }
    }
}
