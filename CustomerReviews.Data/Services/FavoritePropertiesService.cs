using System;
using System.Collections.Generic;
using System.Linq;

using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;
using CustomerReviews.Data.Repositories;

using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure;


namespace CustomerReviews.Data.Services
{
    public class FavoritePropertiesService : ServiceBase, IFavoritePropertyService
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

        public AveragePropertyRating[] GetAveragePropertyRatings(string productId)
        {
            using (ICustomerReviewRepository repository = _repositoryFactory())
            {
                var favoriteProperties = GetProductFavoriteProperties(productId);
                if (!favoriteProperties.Any())
                    return new AveragePropertyRating[0];

                var reviews = repository.CustomerReviews.Where(r => r.ProductId == productId && r.IsActive).ToArray();
                var ratings = new List<AveragePropertyRating>();
                foreach (FavoriteProperty property in favoriteProperties)
                {
                    var rating = AbstractTypeFactory<AveragePropertyRating>.TryCreateInstance();
                    rating.FavoriteProperty = property;

                    var values = reviews.SelectMany(x => x.PropertyValues.Where(v => v.PropertyId == property.Id));
                    if (values.Any())
                    {
                        rating.Rating = values.Average(x => x.Rating);
                    }
                    
                    ratings.Add(rating);
                }

                return ratings.ToArray();
            }
        }
    }
}