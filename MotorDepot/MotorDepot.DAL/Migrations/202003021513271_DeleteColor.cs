namespace MotorDepot.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteColor : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FlightRequestStatus", "Color");
            DropColumn("dbo.FlightStatus", "Color");
            DropColumn("dbo.AutoStatus", "Color");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AutoStatus", "Color", c => c.String());
            AddColumn("dbo.FlightStatus", "Color", c => c.String());
            AddColumn("dbo.FlightRequestStatus", "Color", c => c.String());
        }
    }
}
