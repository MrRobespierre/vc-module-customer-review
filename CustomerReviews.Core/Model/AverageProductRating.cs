namespace CustomerReviews.Core.Model
{
    public class AverageProductRating
    {
        public AverageProductRating(string productId, double rating, double reviewsCount)
        {
            ProductId = productId;
            Rating = rating;
            ReviewsCount = reviewsCount;
        }

        public string ProductId { get; }

        public double Rating { get; }

        public double ReviewsCount { get; }
    }
}