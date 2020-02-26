namespace MotorDepot.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingbrandandtype : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Autoes", "AutoStatus_Id", "dbo.AutoStatus");
            DropIndex("dbo.Autoes", new[] { "AutoStatus_Id" });
            DropColumn("dbo.Autoes", "StatusId");
            RenameColumn(table: "dbo.Autoes", name: "AutoStatus_Id", newName: "StatusId");
            CreateTable(
                "dbo.AutoBrands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AutoTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Autoes", "Model", c => c.String(maxLength: 64));
            AddColumn("dbo.Autoes", "AutoBrandId", c => c.Int(nullable: false));
            AddColumn("dbo.Autoes", "AutoTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Autoes", "StatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Autoes", "AutoBrandId");
            CreateIndex("dbo.Autoes", "AutoTypeId");
            CreateIndex("dbo.Autoes", "StatusId");
            AddForeignKey("dbo.Autoes", "AutoBrandId", "dbo.AutoBrands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Autoes", "AutoTypeId", "dbo.AutoTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Autoes", "StatusId", "dbo.AutoStatus", "Id", cascadeDelete: true);
            DropColumn("dbo.Autoes", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Autoes", "Name", c => c.String(maxLength: 64));
            DropForeignKey("dbo.Autoes", "StatusId", "dbo.AutoStatus");
            DropForeignKey("dbo.Autoes", "AutoTypeId", "dbo.AutoTypes");
            DropForeignKey("dbo.Autoes", "AutoBrandId", "dbo.AutoBrands");
            DropIndex("dbo.Autoes", new[] { "StatusId" });
            DropIndex("dbo.Autoes", new[] { "AutoTypeId" });
            DropIndex("dbo.Autoes", new[] { "AutoBrandId" });
            AlterColumn("dbo.Autoes", "StatusId", c => c.Int());
            DropColumn("dbo.Autoes", "AutoTypeId");
            DropColumn("dbo.Autoes", "AutoBrandId");
            DropColumn("dbo.Autoes", "Model");
            DropTable("dbo.AutoTypes");
            DropTable("dbo.AutoBrands");
            RenameColumn(table: "dbo.Autoes", name: "StatusId", newName: "AutoStatus_Id");
            AddColumn("dbo.Autoes", "StatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Autoes", "AutoStatus_Id");
            AddForeignKey("dbo.Autoes", "AutoStatus_Id", "dbo.AutoStatus", "Id");
        }
    }
}
