using CustomerReviews.Core.Model;


namespace CustomerReviews.Core.Services
{
    public interface ICustomerReviewService
    {
        CustomerReview GetById(string id);

        CustomerReview[] GetByIds(string[] ids);
        
        void SaveCustomerReview(CustomerReview item);

        void DeleteCustomerReviews(string[] ids);

        AverageProductRating GetAverageProductRating(string productId);
    }
}
