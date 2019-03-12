namespace CustomerReviews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFavoriteProperties : DbMigration
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
                        PropertyId = c.String(nullable: false, maxLength: 128),
                        ReviewId = c.String(nullable: false, maxLength: 128),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FavoriteProperty", t => t.PropertyId, cascadeDelete: true)
                .ForeignKey("dbo.CustomerReview", t => t.ReviewId, cascadeDelete: true)
                .Index(t => t.PropertyId)
                .Index(t => t.ReviewId);
            
            AddColumn("dbo.CustomerReview", "ProductRating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavoritePropertyValue", "ReviewId", "dbo.CustomerReview");
            DropForeignKey("dbo.FavoritePropertyValue", "PropertyId", "dbo.FavoriteProperty");
            DropIndex("dbo.FavoritePropertyValue", new[] { "ReviewId" });
            DropIndex("dbo.FavoritePropertyValue", new[] { "PropertyId" });
            DropColumn("dbo.CustomerReview", "ProductRating");
            DropTable("dbo.FavoritePropertyValue");
            DropTable("dbo.FavoriteProperty");
        }
    }
}
