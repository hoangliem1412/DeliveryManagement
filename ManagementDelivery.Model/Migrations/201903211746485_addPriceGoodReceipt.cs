namespace ManagementDelivery.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPriceGoodReceipt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoodsReceipt", "PurchasePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoodsReceipt", "PurchasePrice");
        }
    }
}
