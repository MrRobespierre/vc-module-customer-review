namespace CustomerReviews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavoriteProperty",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ProductId = c.String(nullable: false, maxLength: 128),
                        PropertyId = c.String(maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FavoritePropertyValue",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Rating = c.Int(nullable: false),
                        Property_Id = c.String(nullable: false, maxLength: 128),
                        Review_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FavoriteProperty", t => t.Property_Id, cascadeDelete: true)
                .ForeignKey("dbo.CustomerReview", t => t.Review_Id, cascadeDelete: true)
                .Index(t => t.Property_Id)
                .Index(t => t.Review_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavoritePropertyValue", "Review_Id", "dbo.CustomerReview");
            DropForeignKey("dbo.FavoritePropertyValue", "Property_Id", "dbo.FavoriteProperty");
            DropIndex("dbo.FavoritePropertyValue", new[] { "Review_Id" });
            DropIndex("dbo.FavoritePropertyValue", new[] { "Property_Id" });
            DropTable("dbo.FavoritePropertyValue");
            DropTable("dbo.FavoriteProperty");
        }
    }
}
