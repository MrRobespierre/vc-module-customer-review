namespace CustomerReviews.Core.Model
{
    public class AverageProductRating
    {
        public string ProductId { get; set; }

        public double Rating { get; set; }

        public double ReviewsCount { get; set; }
    }
}