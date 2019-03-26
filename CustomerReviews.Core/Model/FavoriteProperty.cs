using VirtoCommerce.Platform.Core.Common;


namespace CustomerReviews.Core.Model
{
    public class FavoriteProperty : Entity
    {
        public string ProductId { get; set; }

        public string PropertyId { get; set; }

        public string Name { get; set; }
    }
}