using System;
using System.Linq;
using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;
using CustomerReviews.Data.Model;
using CustomerReviews.Data.Repositories;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace CustomerReviews.Data.Services
{
    public class CustomerReviewService : ServiceBase, ICustomerReviewService
    {
        private readonly Func<ICustomerReviewRepository> _repositoryFactory;

        public CustomerReviewService(Func<ICustomerReviewRepository> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public CustomerReview GetById(string id)
        {
            using (ICustomerReviewRepository repository = _repositoryFactory())
            {
                CustomerReviewEntity entity = repository.GetCustomerReview(id);
                return entity.ToModel(AbstractTypeFactory<CustomerReview>.TryCreateInstance());
            }
        }

        public CustomerReview[] GetByIds(string[] ids)
        {
            using (ICustomerReviewRepository repository = _repositoryFactory())
            {
                return repository.
                       GetByIds(ids).
                       Select(x => x.ToModel(AbstractTypeFactory<CustomerReview>.TryCreateInstance())).
                       ToArray();
            }
        }

        public void SaveCustomerReview(CustomerReview item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var pkMap = new PrimaryKeyResolvingMap();
            using (ICustomerReviewRepository repository = _repositoryFactory())
            {
                using (ObservableChangeTracker changeTracker = GetChangeTracker(repository))
                {
                    if (!item.IsTransient())
                    {
                        var sourceEntity = AbstractTypeFactory<CustomerReviewEntity>.TryCreateInstance().FromModel(item, pkMap);
                        var existsEntity = repository.GetCustomerReview(item.Id);
                        if (existsEntity != null)
                        {
                            changeTracker.Attach(existsEntity);
                            sourceEntity.Patch(existsEntity);
                        }
                        else
                        {
                            repository.Add(sourceEntity);
                        }
                    }
                    else
                    {
                        item.Id = Guid.NewGuid().ToString("N");
                        foreach (var value in item.PropertyValues)
                        {
                            value.ReviewId = item.Id;
                        }

                        var sourceEntity = AbstractTypeFactory<CustomerReviewEntity>.TryCreateInstance().FromModel(item, pkMap);
                        repository.Add(sourceEntity);
                    }

                    CommitChanges(repository);
                    pkMap.ResolvePrimaryKeys();
                }
            }
        }

        public void DeleteCustomerReviews(string[] ids)
        {
            using (ICustomerReviewRepository repository = _repositoryFactory())
            {
                repository.DeleteCustomerReviews(ids);
                CommitChanges(repository);
            }
        }

        public AverageProductRating GetAverageProductRating(string productId)
        {
            using (ICustomerReviewRepository repository = _repositoryFactory())
            {
                var reviews = repository.CustomerReviews.Where(x => x.ProductId == productId && x.IsActive).ToArray();
                var result = AbstractTypeFactory<AverageProductRating>.TryCreateInstance();
                result.ProductId = productId;
                if (!reviews.Any())
                    return result;

                result.Rating = reviews.Average(x => x.ProductRating);
                result.ReviewsCount = reviews.Length;
                return result;
            }
        }
    }
}
