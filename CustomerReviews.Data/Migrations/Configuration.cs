using System;
using System.Data.Entity.Migrations;

using CustomerReviews.Data.Model;
using CustomerReviews.Data.Repositories;


namespace CustomerReviews.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<CustomerReviewRepository>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations";
        }

        protected override void Seed(CustomerReviewRepository context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            DateTime now = DateTime.UtcNow;
            context.AddOrUpdate(new CustomerReviewEntity
            {
                Id = "1",
                ProductId = "0f7a77cc1b9a46a29f6a159e5cd49ad1",
                CreatedDate = now,
                CreatedBy = "initial data seed",
                AuthorNickname = "Andrew Peters",
                Content = "Super!"
            });
            context.AddOrUpdate(new CustomerReviewEntity
            {
                Id = "2",
                ProductId = "0f7a77cc1b9a46a29f6a159e5cd49ad1",
                CreatedDate = now,
                CreatedBy = "initial data seed",
                AuthorNickname = "Mr. Pumpkin",
                Content = "So so"
            });
            context.AddOrUpdate(new CustomerReviewEntity
            {
                Id = "3",
                ProductId = "0f7a77cc1b9a46a29f6a159e5cd49ad1",
                CreatedDate = now,
                CreatedBy = "initial data seed",
                AuthorNickname = "John Doe",
                Content = "Liked that"
            });

            context.AddOrUpdate(new FavoritePropertyEntity
            {
                Id = "299aa36d0426476d985f670a51d37298",
                ProductId = "0f7a77cc1b9a46a29f6a159e5cd49ad1",
                Name = "Sound quality"
            });

            context.AddOrUpdate(new FavoritePropertyEntity
            {
                Id = "2b347ecf337549c9bbe17413446a9d85",
                ProductId = "0f7a77cc1b9a46a29f6a159e5cd49ad1",
                Name = "Wearing comfort"
            });

            context.AddOrUpdate(new FavoritePropertyEntity
            {
                Id = "9e0c553cfc6c483c8a23b467b7e40945",
                ProductId = "0f7a77cc1b9a46a29f6a159e5cd49ad1",
                Name = "Design"
            });
        }
    }
}
