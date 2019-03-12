using VirtoCommerce.Platform.Core.Common;


namespace CustomerReviews.Core.Model
{
    public sealed class FavoritePropertyValue : Entity
    {
        public FavoritePropertyValue()
        {
        }

        public FavoriteProperty Property { get; set; }

        public CustomerReview Review { get; set; }

        public int Rating { get; set; }
    }
}