using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;

using CustomerReviews.Data.Model;
using CustomerReviews.Data.Repositories;

using VirtoCommerce.Platform.Data.Infrastructure;


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

            CreateReviews(context);
            CreateFavoriteProperties(context);
            CreateFavoritePropertyValues(context);
        }

        private static void CreateReviews(EFRepositoryBase context)
        {
            DateTime now = DateTime.UtcNow;
            context.AddOrUpdate(new CustomerReviewEntity
            {
                Id = "1",
                ProductId = "0f7a77cc1b9a46a29f6a159e5cd49ad1",
                CreatedDate = now,
                CreatedBy = "initial data seed",
                AuthorNickname = "Andrew Peters",
                Content = "Super!",
                ProductRating = 5,
                IsActive = true
            });
            context.AddOrUpdate(new CustomerReviewEntity
            {
                Id = "2",
                ProductId = "0f7a77cc1b9a46a29f6a159e5cd49ad1",
                CreatedDate = now,
                CreatedBy = "initial data seed",
                AuthorNickname = "Mr. Pumpkin",
                Content = "So so",
                ProductRating = 3
            });
            context.AddOrUpdate(new CustomerReviewEntity
            {
                Id = "3",
                ProductId = "0f7a77cc1b9a46a29f6a159e5cd49ad1",
                CreatedDate = now,
                CreatedBy = "initial data seed",
                AuthorNickname = "John Doe",
                Content = "Liked that",
                ProductRating = 3
            });
        }

        private static void CreateFavoriteProperties(EFRepositoryBase context)
        {
            context.AddOrUpdate(new FavoritePropertyEntity
            {
                Id = "1",
                ProductId = "0f7a77cc1b9a46a29f6a159e5cd49ad1",
                Name = "Sound quality"
            });

            context.AddOrUpdate(new FavoritePropertyEntity
            {
                Id = "2",
                ProductId = "0f7a77cc1b9a46a29f6a159e5cd49ad1",
                Name = "Wearing comfort"
            });

            context.AddOrUpdate(new FavoritePropertyEntity
            {
                Id = "3",
                ProductId = "0f7a77cc1b9a46a29f6a159e5cd49ad1",
                Name = "Soundproofing"
            });

            context.AddOrUpdate(new FavoritePropertyEntity
            {
                Id = "4",
                ProductId = "0f7a77cc1b9a46a29f6a159e5cd49ad1",
                Name = "Design"
            });

            context.AddOrUpdate(new FavoritePropertyEntity
            {
                Id = "5",
                ProductId = "0f7a77cc1b9a46a29f6a159e5cd49ad1",
                Name = "Reliability"
            });
        }

        private static void CreateFavoritePropertyValues(EFRepositoryBase context)
        {
            context.AddOrUpdate(new FavoritePropertyValueEntity
            {
                Id = "1",
                ReviewId = "1",
                PropertyId = "1",
                Rating = 5
            });
            context.AddOrUpdate(new FavoritePropertyValueEntity
            {
                Id = "2",
                ReviewId = "1",
                PropertyId = "2",
                Rating = 4
            });
            context.AddOrUpdate(new FavoritePropertyValueEntity
            {
                Id = "3",
                ReviewId = "1",
                PropertyId = "3",
                Rating = 3
            });
            context.AddOrUpdate(new FavoritePropertyValueEntity
            {
                Id = "5",
                ReviewId = "1",
                PropertyId = "5",
                Rating = 1
            });
        }
    }
}
