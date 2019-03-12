using System.ComponentModel.DataAnnotations;

using VirtoCommerce.Platform.Core.Common;


namespace CustomerReviews.Data.Model
{
    public sealed class FavoritePropertyEntity : Entity
    {
        public FavoritePropertyEntity()
        {
        }

        [Required]
        [StringLength(128)]
        public string ProductId { get; set; }

        [StringLength(128)]
        public string PropertyId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }
    }
}