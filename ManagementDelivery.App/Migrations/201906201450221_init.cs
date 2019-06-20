namespace ManagementDelivery.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DeliveryDetail", "DriverId", "dbo.Driver");
            DropForeignKey("dbo.GoodsReceipt", "SupplierId", "dbo.Supplier");
            DropIndex("dbo.DeliveryDetail", new[] { "DriverId" });
            DropIndex("dbo.GoodsReceipt", new[] { "SupplierId" });
            DropTable("dbo.Driver");
            DropTable("dbo.Supplier");
        }
        
        public override void Down()
        {
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
                        InsertAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
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
                        InsertAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.GoodsReceipt", "SupplierId");
            CreateIndex("dbo.DeliveryDetail", "DriverId");
            AddForeignKey("dbo.GoodsReceipt", "SupplierId", "dbo.Supplier", "Id");
            AddForeignKey("dbo.DeliveryDetail", "DriverId", "dbo.Driver", "Id", cascadeDelete: true);
        }
    }
}
