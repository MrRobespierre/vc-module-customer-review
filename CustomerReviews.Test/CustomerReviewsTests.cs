using System;
using System.Data.Entity;
using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;
using CustomerReviews.Data.Migrations;
using CustomerReviews.Data.Model;
using CustomerReviews.Data.Repositories;
using CustomerReviews.Data.Services;

using VirtoCommerce.Domain.Commerce.Model.Search;
using VirtoCommerce.Platform.Data.Infrastructure;
using VirtoCommerce.Platform.Data.Infrastructure.Interceptors;
using VirtoCommerce.Platform.Testing.Bases;
using Xunit;

namespace CustomerReviews.Test
{
    public sealed class CustomerReviewsTests : FunctionalTestBase
    {
        private const string ProductId = "testProductId";
        private const string CustomerReviewId = "testId";

        public CustomerReviewsTests()
        {
            ConnectionString = "VirtoCommerce";
        }

        [Fact]
        public void CanDoCRUDandSearch()
        {
            // Read non-existing item
            CustomerReview[] getByIdsResult = CustomerReviewService.GetByIds(new[] { CustomerReviewId });
            Assert.NotNull(getByIdsResult);
            Assert.Empty(getByIdsResult);

            // Create
            var item = new CustomerReview
            {
                Id = CustomerReviewId,
                ProductId = ProductId,
                CreatedDate = DateTime.Now,
                CreatedBy = "initial data seed",
                AuthorNickname = "John Doe",
                Content = "Liked that"
            };

            CustomerReviewService.SaveCustomerReview(item);

            getByIdsResult = CustomerReviewService.GetByIds(new[] { CustomerReviewId });
            Assert.Single(getByIdsResult);

            item = getByIdsResult[0];
            Assert.Equal(CustomerReviewId, item.Id);

            // Update
            string updatedContent = "Updated content";
            Assert.NotEqual(updatedContent, item.Content);

            item.Content = updatedContent;
            CustomerReviewService.SaveCustomerReview(item);
            getByIdsResult = CustomerReviewService.GetByIds(new[] { CustomerReviewId });
            Assert.Single(getByIdsResult);

            item = getByIdsResult[0];
            Assert.Equal(updatedContent, item.Content);

            // Search
            Assert.Throws<ArgumentNullException>(() => CustomerReviewSearchService.SearchCustomerReviews(null));

            var criteria = new CustomerReviewSearchCriteria { ProductIds = new[] { ProductId } };
            GenericSearchResult<CustomerReview> searchResult = CustomerReviewSearchService.SearchCustomerReviews(criteria);

            Assert.NotNull(searchResult);
            Assert.Equal(1, searchResult.TotalCount);
            Assert.Single(searchResult.Results);

            // Delete
            CanDeleteCustomerReviews();
        }

        [Fact]
        public void CanCreateCustomerReview()
        {
            var item = new CustomerReview
            {
                ProductId = ProductId,
                AuthorNickname = "John Doe",
                Content = "Liked that",
                IsActive = true,
                ProductRating = 4,
                PropertyValues = new[]
                {
                    new FavoritePropertyValue
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        Property = new FavoriteProperty
                        {
                            Id = "1",
                            ProductId = ProductId,
                            Name = "Sound quality"
                        },
                        PropertyId = "1",
                        Rating = 5
                    }
                }
            };

            try
            {
                CustomerReviewService.SaveCustomerReview(item);

                Assert.False(item.IsTransient());
                var getByIdItem = CustomerReviewService.GetById(item.Id);
                Assert.NotNull(getByIdItem);
            }
            finally
            {
                CustomerReviewService.DeleteCustomerReviews(new[] { item.Id });
            }
        }

        [Fact]
        public void CanDeleteCustomerReviews()
        {
            CustomerReviewService.DeleteCustomerReviews(new[] { CustomerReviewId });

            CustomerReview[] getByIdsResult = CustomerReviewService.GetByIds(new[] { CustomerReviewId });
            Assert.NotNull(getByIdsResult);
            Assert.Empty(getByIdsResult);
        }

        [Fact]
        public void CheckCalculateAverageProductRating()
        {
            const int firstRating = 2;
            const int secondRating = 5;
            const int reviewsCount = 2;
            const double averageRating = 3.5D;

            var firstItem = new CustomerReview
            {
                ProductId = ProductId,
                AuthorNickname = "Test author",
                Content = "Test content",
                IsActive = true,
                ProductRating = firstRating
            };

            var secondItem = new CustomerReview
            {
                ProductId = ProductId,
                AuthorNickname = "Test author",
                Content = "Test content",
                IsActive = true,
                ProductRating = secondRating
            };

            try
            {
                CustomerReviewService.SaveCustomerReview(firstItem);
                CustomerReviewService.SaveCustomerReview(secondItem);

                var productRating = CustomerReviewService.GetAverageProductRating(ProductId);
                Assert.Equal(ProductId, productRating.ProductId);
                Assert.Equal(reviewsCount, productRating.ReviewsCount);
                Assert.Equal(averageRating, productRating.Rating);
            }
            finally
            {
                CustomerReviewService.DeleteCustomerReviews(new[] { firstItem.Id, secondItem.Id });
            }
        }

        private ICustomerReviewSearchService CustomerReviewSearchService =>
            new CustomerReviewSearchService(GetRepository, CustomerReviewService);

        private ICustomerReviewService CustomerReviewService =>
            new CustomerReviewService(GetRepository);


        private ICustomerReviewRepository GetRepository()
        {
            var repository = new CustomerReviewRepository(ConnectionString, new EntityPrimaryKeyGeneratorInterceptor(), new AuditableInterceptor(null));
            EnsureDatabaseInitialized(() => new CustomerReviewRepository(ConnectionString), () => Database.SetInitializer(new SetupDatabaseInitializer<CustomerReviewRepository, Configuration>()));
            return repository;
        }
    }
}
