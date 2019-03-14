using System.Linq;
using CustomerReviews.Data.Model;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviews.Data.Repositories
{
    public interface ICustomerReviewRepository : IRepository
    {
        IQueryable<CustomerReviewEntity> CustomerReviews { get; }

        FavoritePropertyEntity[] GetProductFavoriteProperties(string productId);

        CustomerReviewEntity GetCustomerReview(string id);

        CustomerReviewEntity[] GetByIds(string[] ids);

        void DeleteCustomerReviews(string[] ids);
    }
}
