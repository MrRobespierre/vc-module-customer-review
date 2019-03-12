using VirtoCommerce.Platform.Core.Common;


namespace CustomerReviews.Core.Model
{
    public sealed class CustomerReview : AuditableEntity
    {
        public CustomerReview()
        {
        }

        public string AuthorNickname { get; set; }

        public string Content { get; set; }

        public bool IsActive { get; set; }

        public string ProductId { get; set; }

        public int ProductRating { get; set; }

        public FavoritePropertyValue[] PropertyValues { get; set; }
    }
}
