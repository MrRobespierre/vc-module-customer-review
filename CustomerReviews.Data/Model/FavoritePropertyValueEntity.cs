using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CustomerReviews.Core.Model;

using VirtoCommerce.Platform.Core.Common;


namespace CustomerReviews.Data.Model
{
    public sealed class FavoritePropertyValueEntity : Entity
    {
        public FavoritePropertyValueEntity()
        {
        }

        [Required]
        [ForeignKey(nameof(Property))]
        public string PropertyId { get; set; }

        // TODO не надо выгружать каждый раз
        [Required]
        public FavoritePropertyEntity Property { get; set; }

        [Required]
        [ForeignKey(nameof(Review))]
        public string ReviewId { get; set; }

        [Required]
        public CustomerReviewEntity Review { get; set; }

        [Required]
        public int Rating { get; set; }

        public FavoritePropertyValue ToModel(FavoritePropertyValue model, CustomerReview customerReview)
        {
            model.Id = Id;
            model.Property = Property?.ToModel(new FavoriteProperty());
            model.Review = customerReview;
            model.Rating = Rating;

            return model;
        }

        public FavoritePropertyValueEntity FromModel(FavoritePropertyValue model, PrimaryKeyResolvingMap pkMap)
        {
            Id = model.Id;
            
            // TODO надо ли конвертировать эти связи?
            PropertyId = model.Property.Id;
            Property = AbstractTypeFactory<FavoritePropertyEntity>.TryCreateInstance().FromModel(model.Property, pkMap);
            ReviewId = model.Review.Id;
            Review = AbstractTypeFactory<CustomerReviewEntity>.TryCreateInstance().FromModel(model.Review, pkMap);


            Rating = model.Rating;

            return this;
        }
    }
}