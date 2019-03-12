using System;
using System.Linq;

using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;
using CustomerReviews.Data.Repositories;

using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure;


namespace CustomerReviews.Data.Services
{
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