using System.Collections.Generic;

using CustomerReviews.Core.Model;


namespace CustomerReviews.Core.Services
{
    public interface IFavoritePropertyService
    {
        FavoriteProperty[] GetProductProperties(string productId);
    }
}