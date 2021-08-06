﻿namespace work_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScriptA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        cId = c.Int(nullable: false, identity: true),
                        cName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.cId);
            
            CreateTable(
                "dbo.Toys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ToysName = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        StoreDate = c.DateTime(nullable: false, storeType: "date"),
                        PicturePath = c.String(),
                        cId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.cId, cascadeDelete: true)
                .Index(t => t.cId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Toys", "cId", "dbo.Categories");
            DropIndex("dbo.Toys", new[] { "cId" });
            DropTable("dbo.Toys");
            DropTable("dbo.Categories");
        }
    }
}
