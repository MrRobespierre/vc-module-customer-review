using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using CustomerReviews.Core.Model;

using VirtoCommerce.Platform.Core.Common;


namespace CustomerReviews.Data.Model
{
    public class CustomerReviewEntity : AuditableEntity
    {
        public CustomerReviewEntity()
        {
            PropertyValues = new NullCollection<FavoritePropertyValueEntity>();
        }


        [StringLength(128)]
        public string AuthorNickname { get; set; }

        [Required]
        [StringLength(1024)]
        public string Content { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [StringLength(128)]
        public string ProductId { get; set; }

        [Required]
        [Range(1, 5)]
        public int ProductRating { get; set; }

        public ObservableCollection<FavoritePropertyValueEntity> PropertyValues { get; set; }

        public CustomerReview ToModel(CustomerReview customerReview)
        {
            if (customerReview == null)
                throw new ArgumentNullException(nameof(customerReview));

            customerReview.Id = Id;
            customerReview.CreatedBy = CreatedBy;
            customerReview.CreatedDate = CreatedDate;
            customerReview.ModifiedBy = ModifiedBy;
            customerReview.ModifiedDate = ModifiedDate;

            customerReview.AuthorNickname = AuthorNickname;
            customerReview.Content = Content;
            customerReview.IsActive = IsActive;
            customerReview.ProductId = ProductId;
            customerReview.ProductRating = ProductRating;
            customerReview.PropertyValues = PropertyValues.Select(
                x => x.ToModel(AbstractTypeFactory<FavoritePropertyValue>.TryCreateInstance())).ToArray();

            return customerReview;
        }

        public CustomerReviewEntity FromModel(CustomerReview customerReview, PrimaryKeyResolvingMap pkMap)
        {
            if (customerReview == null)
                throw new ArgumentNullException(nameof(customerReview));

            pkMap.AddPair(customerReview, this);

            Id = customerReview.Id;
            CreatedBy = customerReview.CreatedBy;
            CreatedDate = customerReview.CreatedDate;
            ModifiedBy = customerReview.ModifiedBy;
            ModifiedDate = customerReview.ModifiedDate;

            AuthorNickname = customerReview.AuthorNickname;
            Content = customerReview.Content;
            IsActive = customerReview.IsActive;
            ProductId = customerReview.ProductId;
            ProductRating = customerReview.ProductRating;
            var propertyValues = customerReview.PropertyValues
                                               .Select(x => AbstractTypeFactory<FavoritePropertyValueEntity>
                                                            .TryCreateInstance()
                                                            .FromModel(x, pkMap));
            PropertyValues = new ObservableCollection<FavoritePropertyValueEntity>(propertyValues);
            
            return this;
        }

        public void Patch(CustomerReviewEntity target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            target.AuthorNickname = AuthorNickname;
            target.Content = Content;
            target.IsActive = IsActive;
            target.ProductId = ProductId;
            target.ProductRating = ProductRating;
        }
    }
}
