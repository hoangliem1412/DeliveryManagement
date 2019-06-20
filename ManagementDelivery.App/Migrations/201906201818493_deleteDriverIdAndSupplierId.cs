namespace ManagementDelivery.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteDriverIdAndSupplierId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DeliveryDetail", "DriverId");
            DropColumn("dbo.GoodsReceipt", "SupplierId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GoodsReceipt", "SupplierId", c => c.Int());
            AddColumn("dbo.DeliveryDetail", "DriverId", c => c.Int(nullable: false));
        }
    }
}
