namespace MotorDepot.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Flights", "Dispatcher_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Flights", new[] { "Dispatcher_Id" });
            AddColumn("dbo.Flights", "DeparturePlace", c => c.String(nullable: false, maxLength: 64));
            AddColumn("dbo.Flights", "ArrivalPlace", c => c.String(nullable: false, maxLength: 64));
            DropColumn("dbo.Flights", "DispatcherId");
            DropColumn("dbo.Flights", "Dispatcher_Id");
            DropColumn("dbo.AspNetUsers", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Flights", "Dispatcher_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Flights", "DispatcherId", c => c.Int(nullable: false));
            DropColumn("dbo.Flights", "ArrivalPlace");
            DropColumn("dbo.Flights", "DeparturePlace");
            CreateIndex("dbo.Flights", "Dispatcher_Id");
            AddForeignKey("dbo.Flights", "Dispatcher_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
