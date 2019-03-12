using VirtoCommerce.Platform.Core.Common;


namespace CustomerReviews.Core.Model
{
    public sealed class FavoriteProperty : Entity
    {
        public FavoriteProperty()
        {
        }

        public string ProductId { get; set; }

        public string PropertyId { get; set; }

        public string Name { get; set; }
    }
}