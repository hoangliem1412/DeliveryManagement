namespace ManagementDelivery.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Note = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        InsertAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CategoryId = c.Int(),
                        Price = c.Decimal(storeType: "money"),
                        PurchasePrice = c.Decimal(storeType: "money"),
                        Description = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        InsertAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategory", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.DeliveryDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DeliveryId = c.Long(nullable: false),
                        ProductId = c.Int(nullable: false),
                        DriverId = c.Int(nullable: false),
                        Quantity = c.Int(),
                        Price = c.Decimal(storeType: "money"),
                        Status = c.Int(),
                        InsertAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Delivery", t => t.DeliveryId, cascadeDelete: true)
                .ForeignKey("dbo.Driver", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.DeliveryId)
                .Index(t => t.ProductId)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.Delivery",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        TotalPrice = c.Decimal(storeType: "money"),
                        IsDelete = c.Boolean(nullable: false),
                        InsertAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Address = c.String(),
                        Phone = c.String(maxLength: 20, unicode: false),
                        MoreInfo = c.String(),
                        Note = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        InsertAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Driver",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Address = c.String(),
                        Phone = c.String(maxLength: 20),
                        MoreInfo = c.String(),
                        Note = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        InsertAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GoodsReceipt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(),
                        SupplierId = c.Int(),
                        Quantity = c.Int(),
                        DateReceipt = c.DateTime(),
                        InsertAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.Supplier", t => t.SupplierId)
                .Index(t => t.ProductId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Address = c.String(),
                        Phone = c.String(maxLength: 20),
                        MoreInfo = c.String(),
                        Note = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        InsertAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(),
                        Quantity = c.Int(),
                        IsDelete = c.Boolean(nullable: false),
                        InsertAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stock", "ProductId", "dbo.Product");
            DropForeignKey("dbo.GoodsReceipt", "SupplierId", "dbo.Supplier");
            DropForeignKey("dbo.GoodsReceipt", "ProductId", "dbo.Product");
            DropForeignKey("dbo.DeliveryDetail", "ProductId", "dbo.Product");
            DropForeignKey("dbo.DeliveryDetail", "DriverId", "dbo.Driver");
            DropForeignKey("dbo.DeliveryDetail", "DeliveryId", "dbo.Delivery");
            DropForeignKey("dbo.Delivery", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.ProductCategory");
            DropIndex("dbo.Stock", new[] { "ProductId" });
            DropIndex("dbo.GoodsReceipt", new[] { "SupplierId" });
            DropIndex("dbo.GoodsReceipt", new[] { "ProductId" });
            DropIndex("dbo.Delivery", new[] { "CustomerId" });
            DropIndex("dbo.DeliveryDetail", new[] { "DriverId" });
            DropIndex("dbo.DeliveryDetail", new[] { "ProductId" });
            DropIndex("dbo.DeliveryDetail", new[] { "DeliveryId" });
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropTable("dbo.Stock");
            DropTable("dbo.Supplier");
            DropTable("dbo.GoodsReceipt");
            DropTable("dbo.Driver");
            DropTable("dbo.Customer");
            DropTable("dbo.Delivery");
            DropTable("dbo.DeliveryDetail");
            DropTable("dbo.Product");
            DropTable("dbo.ProductCategory");
        }
    }
}
