using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using VirtoCommerce.Platform.Core.Common;


namespace CustomerReviews.Data.Model
{
    public sealed class FavoritePropertyValueEntity : Entity
    {
        public FavoritePropertyValueEntity()
        {
        }

        [ForeignKey(nameof(Review))]
        public string ReviewId { get; set; }

        [Required]
        public FavoritePropertyEntity Property { get; set; }

        [Required]
        public CustomerReviewEntity Review { get; set; }

        [Required]
        public int Rating { get; set; }
    }
}