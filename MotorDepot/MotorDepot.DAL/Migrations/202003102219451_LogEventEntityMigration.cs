namespace MotorDepot.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogEventEntityMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        Time = c.DateTime(nullable: false),
                        Ip = c.String(),
                        RouteValues = c.String(),
                        StackTrace = c.String(),
                        LogTypeLookupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LogTypeLookups", t => t.LogTypeLookupId, cascadeDelete: true)
                .Index(t => t.LogTypeLookupId);
            
            CreateTable(
                "dbo.LogTypeLookups",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.LogExceptions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LogExceptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.LogEvents", "LogTypeLookupId", "dbo.LogTypeLookups");
            DropIndex("dbo.LogEvents", new[] { "LogTypeLookupId" });
            DropTable("dbo.LogTypeLookups");
            DropTable("dbo.LogEvents");
        }
    }
}
