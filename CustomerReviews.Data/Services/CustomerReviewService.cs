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
    public sealed class CustomerReviewService : ServiceBase, ICustomerReviewService
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

        public void SaveCustomerReviews(CustomerReview[] items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var pkMap = new PrimaryKeyResolvingMap();
            using (ICustomerReviewRepository repository = _repositoryFactory())
            {
                using (ObservableChangeTracker changeTracker = GetChangeTracker(repository))
                {
                    CustomerReviewEntity[] alreadyExistEntities = repository.GetByIds(
                        items.Where(m => !m.IsTransient()).Select(x => x.Id).ToArray());
                    foreach (CustomerReview derivativeContract in items)
                    {
                        CustomerReviewEntity sourceEntity = AbstractTypeFactory<CustomerReviewEntity>.
                                                            TryCreateInstance().
                                                            FromModel(derivativeContract, pkMap);
                        CustomerReviewEntity targetEntity = alreadyExistEntities.FirstOrDefault(x => x.Id == sourceEntity.Id);
                        if (targetEntity != null)
                        {
                            changeTracker.Attach(targetEntity);
                            sourceEntity.Patch(targetEntity);
                        }
                        else
                        {
                            repository.Add(sourceEntity);
                        }
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
    }

    public sealed class FavoritePropertiesService : ServiceBase, IFavoritePropertyService
    {
        private readonly Func<ICustomerReviewRepository> _repositoryFactory;

        public FavoritePropertiesService(Func<ICustomerReviewRepository> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public FavoriteProperty[] GetProductFavoriteProperties(string productId)
        {
            using (ICustomerReviewRepository repository = _repositoryFactory())
            {
                return repository.GetProductFavoriteProperties(productId).
                                  Select(x => x.ToModel(AbstractTypeFactory<FavoriteProperty>.TryCreateInstance())).
                                  ToArray();
            }
        }
    }
}
