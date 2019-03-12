using System.ComponentModel.DataAnnotations;

using CustomerReviews.Core.Model;

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

        public FavoriteProperty ToModel(FavoriteProperty model)
        {
            model.Id = Id;
            model.ProductId = ProductId;
            model.PropertyId = PropertyId;
            model.Name = Name;

            return model;
        }

        public FavoritePropertyEntity FromModel(FavoriteProperty model, PrimaryKeyResolvingMap pkMap)
        {
            pkMap.AddPair(model, this);

            Id = model.Id;
            ProductId = model.ProductId;
            PropertyId = model.PropertyId;

            return this;
        }

        public void Patch(FavoritePropertyEntity target)
        {
            target.Id = Id;
            target.ProductId = ProductId;
            target.PropertyId = PropertyId;
            target.Name = Name;
        }
    }
}