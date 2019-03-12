using CustomerReviews.Core.Model;


namespace CustomerReviews.Core.Services
{
    public interface ICustomerReviewService
    {
        CustomerReview GetById(string id);

        CustomerReview[] GetByIds(string[] ids);

        void SaveCustomerReviews(CustomerReview[] items);

        void DeleteCustomerReviews(string[] ids);
    }
}
