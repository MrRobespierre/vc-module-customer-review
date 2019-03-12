using CustomerReviews.Core.Model;


namespace CustomerReviews.Core.Services
{
    public interface IFavoritePropertyService
    {
        FavoriteProperty[] GetProductFavoriteProperties(string productId);
    }
}