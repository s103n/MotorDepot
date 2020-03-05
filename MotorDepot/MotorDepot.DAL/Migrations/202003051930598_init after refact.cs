namespace MotorDepot.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initafterrefact : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AutoBrands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Autoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        Numbers = c.String(),
                        EnginePower = c.Int(nullable: false),
                        EngineCapacity = c.Double(nullable: false),
                        BootVolumeMax = c.Double(nullable: false),
                        AutoBrandId = c.Int(nullable: false),
                        AutoTypeLookupId = c.Int(nullable: false),
                        AutoStatusLookupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AutoBrands", t => t.AutoBrandId, cascadeDelete: true)
                .ForeignKey("dbo.AutoTypeLookups", t => t.AutoTypeLookupId, cascadeDelete: true)
                .ForeignKey("dbo.AutoStatusLookups", t => t.AutoStatusLookupId, cascadeDelete: true)
                .Index(t => t.AutoBrandId)
                .Index(t => t.AutoTypeLookupId)
                .Index(t => t.AutoStatusLookupId);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DeparturePlace = c.String(),
                        ArrivalPlace = c.String(),
                        Distance = c.Double(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        FlightStatusLookupId = c.Int(nullable: false),
                        DriverId = c.String(maxLength: 128),
                        AutoId = c.Int(),
                        CreatorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Autoes", t => t.AutoId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId)
                .ForeignKey("dbo.AspNetUsers", t => t.DriverId)
                .ForeignKey("dbo.FlightStatusLookups", t => t.FlightStatusLookupId, cascadeDelete: true)
                .Index(t => t.FlightStatusLookupId)
                .Index(t => t.DriverId)
                .Index(t => t.AutoId)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        IsBlocked = c.Boolean(nullable: false),
                        LastSignUp = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.FlightRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriverId = c.String(maxLength: 128),
                        DispatcherId = c.String(maxLength: 128),
                        FlightRequestStatusLookupId = c.Int(nullable: false),
                        FlightId = c.Int(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        EnginePower = c.Int(nullable: false),
                        EngineCapacity = c.Int(nullable: false),
                        BootVolume = c.Double(nullable: false),
                        AutoTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AutoTypeLookups", t => t.AutoTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.DispatcherId)
                .ForeignKey("dbo.AspNetUsers", t => t.DriverId)
                .ForeignKey("dbo.Flights", t => t.FlightId)
                .ForeignKey("dbo.FlightRequestStatusLookups", t => t.FlightRequestStatusLookupId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.DispatcherId)
                .Index(t => t.FlightRequestStatusLookupId)
                .Index(t => t.FlightId)
                .Index(t => t.AutoTypeId);
            
            CreateTable(
                "dbo.AutoTypeLookups",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FlightRequestStatusLookups",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FlightStatusLookups",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AutoStatusLookups",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Autoes", "AutoStatusLookupId", "dbo.AutoStatusLookups");
            DropForeignKey("dbo.Flights", "FlightStatusLookupId", "dbo.FlightStatusLookups");
            DropForeignKey("dbo.FlightRequests", "FlightRequestStatusLookupId", "dbo.FlightRequestStatusLookups");
            DropForeignKey("dbo.FlightRequests", "FlightId", "dbo.Flights");
            DropForeignKey("dbo.FlightRequests", "DriverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FlightRequests", "DispatcherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FlightRequests", "AutoTypeId", "dbo.AutoTypeLookups");
            DropForeignKey("dbo.Autoes", "AutoTypeLookupId", "dbo.AutoTypeLookups");
            DropForeignKey("dbo.Flights", "DriverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Flights", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Flights", "AutoId", "dbo.Autoes");
            DropForeignKey("dbo.Autoes", "AutoBrandId", "dbo.AutoBrands");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.FlightRequests", new[] { "AutoTypeId" });
            DropIndex("dbo.FlightRequests", new[] { "FlightId" });
            DropIndex("dbo.FlightRequests", new[] { "FlightRequestStatusLookupId" });
            DropIndex("dbo.FlightRequests", new[] { "DispatcherId" });
            DropIndex("dbo.FlightRequests", new[] { "DriverId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Flights", new[] { "CreatorId" });
            DropIndex("dbo.Flights", new[] { "AutoId" });
            DropIndex("dbo.Flights", new[] { "DriverId" });
            DropIndex("dbo.Flights", new[] { "FlightStatusLookupId" });
            DropIndex("dbo.Autoes", new[] { "AutoStatusLookupId" });
            DropIndex("dbo.Autoes", new[] { "AutoTypeLookupId" });
            DropIndex("dbo.Autoes", new[] { "AutoBrandId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AutoStatusLookups");
            DropTable("dbo.FlightStatusLookups");
            DropTable("dbo.FlightRequestStatusLookups");
            DropTable("dbo.AutoTypeLookups");
            DropTable("dbo.FlightRequests");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Flights");
            DropTable("dbo.Autoes");
            DropTable("dbo.AutoBrands");
        }
    }
}
