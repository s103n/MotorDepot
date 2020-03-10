namespace MotorDepot.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogExceptionIdProperty : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogExceptions");
        }
    }
}
