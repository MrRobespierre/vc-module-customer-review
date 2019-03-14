using VirtoCommerce.Platform.Core.Common;


namespace CustomerReviews.Core.Model
{
    public sealed class FavoritePropertyValue : Entity
    {
        public FavoritePropertyValue()
        {
        }

        public string PropertyId { get; set; }

        public FavoriteProperty Property { get; set; }

        public string ReviewId { get; set; }

        public int Rating { get; set; }
    }
}