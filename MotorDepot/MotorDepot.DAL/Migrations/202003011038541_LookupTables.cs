namespace MotorDepot.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LookupTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Autoes", "AutoTypeId", "dbo.AutoTypes");
            DropForeignKey("dbo.FlightRequests", "AutoTypeId", "dbo.AutoTypes");
            DropForeignKey("dbo.FlightRequests", "FlightRequestStatusId", "dbo.FlightRequestStatus");
            DropForeignKey("dbo.Flights", "StatusId", "dbo.FlightStatus");
            DropForeignKey("dbo.Autoes", "StatusId", "dbo.AutoStatus");
            DropPrimaryKey("dbo.AutoTypes");
            DropPrimaryKey("dbo.FlightRequestStatus");
            DropPrimaryKey("dbo.FlightStatus");
            DropPrimaryKey("dbo.AutoStatus");
            AlterColumn("dbo.AutoTypes", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.FlightRequestStatus", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.FlightRequestStatus", "Name", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.FlightStatus", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.FlightStatus", "Name", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.AutoStatus", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AutoStatus", "Name", c => c.String(nullable: false, maxLength: 32));
            AddPrimaryKey("dbo.AutoTypes", "Id");
            AddPrimaryKey("dbo.FlightRequestStatus", "Id");
            AddPrimaryKey("dbo.FlightStatus", "Id");
            AddPrimaryKey("dbo.AutoStatus", "Id");
            AddForeignKey("dbo.Autoes", "AutoTypeId", "dbo.AutoTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FlightRequests", "AutoTypeId", "dbo.AutoTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FlightRequests", "FlightRequestStatusId", "dbo.FlightRequestStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Flights", "StatusId", "dbo.FlightStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Autoes", "StatusId", "dbo.AutoStatus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Autoes", "StatusId", "dbo.AutoStatus");
            DropForeignKey("dbo.Flights", "StatusId", "dbo.FlightStatus");
            DropForeignKey("dbo.FlightRequests", "FlightRequestStatusId", "dbo.FlightRequestStatus");
            DropForeignKey("dbo.FlightRequests", "AutoTypeId", "dbo.AutoTypes");
            DropForeignKey("dbo.Autoes", "AutoTypeId", "dbo.AutoTypes");
            DropPrimaryKey("dbo.AutoStatus");
            DropPrimaryKey("dbo.FlightStatus");
            DropPrimaryKey("dbo.FlightRequestStatus");
            DropPrimaryKey("dbo.AutoTypes");
            AlterColumn("dbo.AutoStatus", "Name", c => c.String());
            AlterColumn("dbo.AutoStatus", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.FlightStatus", "Name", c => c.String());
            AlterColumn("dbo.FlightStatus", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.FlightRequestStatus", "Name", c => c.String());
            AlterColumn("dbo.FlightRequestStatus", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.AutoTypes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.AutoStatus", "Id");
            AddPrimaryKey("dbo.FlightStatus", "Id");
            AddPrimaryKey("dbo.FlightRequestStatus", "Id");
            AddPrimaryKey("dbo.AutoTypes", "Id");
            AddForeignKey("dbo.Autoes", "StatusId", "dbo.AutoStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Flights", "StatusId", "dbo.FlightStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FlightRequests", "FlightRequestStatusId", "dbo.FlightRequestStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FlightRequests", "AutoTypeId", "dbo.AutoTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Autoes", "AutoTypeId", "dbo.AutoTypes", "Id", cascadeDelete: true);
        }
    }
}
