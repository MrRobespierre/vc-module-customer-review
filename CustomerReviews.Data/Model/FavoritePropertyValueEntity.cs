using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CustomerReviews.Core.Model;

using VirtoCommerce.Platform.Core.Common;


namespace CustomerReviews.Data.Model
{
    public class FavoritePropertyValueEntity : Entity
    {
        [Required]
        [ForeignKey(nameof(Property))]
        public string PropertyId { get; set; }

        public FavoritePropertyEntity Property { get; set; }

        [Required]
        [ForeignKey(nameof(Review))]
        public string ReviewId { get; set; }

        public CustomerReviewEntity Review { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public FavoritePropertyValue ToModel(FavoritePropertyValue model)
        {
            model.Id = Id;
            model.PropertyId = PropertyId;
            model.Property = Property.ToModel(AbstractTypeFactory<FavoriteProperty>.TryCreateInstance());
            model.ReviewId = ReviewId;
            model.Rating = Rating;

            return model;
        }

        public FavoritePropertyValueEntity FromModel(FavoritePropertyValue model, PrimaryKeyResolvingMap pkMap)
        {
            pkMap.AddPair(model, this);

            Id = model.Id;
            PropertyId = model.PropertyId;
            ReviewId = model.ReviewId;
            Rating = model.Rating;

            return this;
        }
    }
}