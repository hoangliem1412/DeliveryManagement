namespace ManagementDelivery.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigNotNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GoodsReceipt", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Stock", "ProductId", "dbo.Product");
            DropIndex("dbo.GoodsReceipt", new[] { "ProductId" });
            DropIndex("dbo.Stock", new[] { "ProductId" });
            AlterColumn("dbo.Product", "Price", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.DeliveryDetail", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.DeliveryDetail", "Price", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.DeliveryDetail", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.GoodsReceipt", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.GoodsReceipt", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.GoodsReceipt", "DateReceipt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Stock", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.Stock", "Quantity", c => c.Int(nullable: false));
            CreateIndex("dbo.GoodsReceipt", "ProductId");
            CreateIndex("dbo.Stock", "ProductId");
            AddForeignKey("dbo.GoodsReceipt", "ProductId", "dbo.Product", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Stock", "ProductId", "dbo.Product", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stock", "ProductId", "dbo.Product");
            DropForeignKey("dbo.GoodsReceipt", "ProductId", "dbo.Product");
            DropIndex("dbo.Stock", new[] { "ProductId" });
            DropIndex("dbo.GoodsReceipt", new[] { "ProductId" });
            AlterColumn("dbo.Stock", "Quantity", c => c.Int());
            AlterColumn("dbo.Stock", "ProductId", c => c.Int());
            AlterColumn("dbo.GoodsReceipt", "DateReceipt", c => c.DateTime());
            AlterColumn("dbo.GoodsReceipt", "Quantity", c => c.Int());
            AlterColumn("dbo.GoodsReceipt", "ProductId", c => c.Int());
            AlterColumn("dbo.DeliveryDetail", "Status", c => c.Int());
            AlterColumn("dbo.DeliveryDetail", "Price", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.DeliveryDetail", "Quantity", c => c.Int());
            AlterColumn("dbo.Product", "Price", c => c.Decimal(storeType: "money"));
            CreateIndex("dbo.Stock", "ProductId");
            CreateIndex("dbo.GoodsReceipt", "ProductId");
            AddForeignKey("dbo.Stock", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.GoodsReceipt", "ProductId", "dbo.Product", "Id");
        }
    }
}
