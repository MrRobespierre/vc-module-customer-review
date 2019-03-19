using System.Web.Http;
using System.Web.Http.Description;

using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;


namespace CustomerReviews.Web.Controllers.Api
{
    [RoutePrefix("api/favoriteProperties")]
    public class FavoritePropertiesController : ApiController
    {
        private readonly IFavoritePropertyService _favoritePropertyService;

        public FavoritePropertiesController(IFavoritePropertyService favoritePropertyService)
        {
            _favoritePropertyService = favoritePropertyService;
        }

        /// <summary>
        /// Gets favorite properties for product.
        /// </summary>
        /// <param name="productId">The Product id.</param>
        [HttpGet]
        [Route("{productId}")]
        [ResponseType(typeof(FavoriteProperty[]))]
        public IHttpActionResult GetProductFavoriteProperties(string productId)
        {
            var result = _favoritePropertyService.GetProductFavoriteProperties(productId);
            return Ok(result);
        }

        [HttpGet]
        [Route("getAveragePropertyRatings/{productId}")]
        [ResponseType(typeof(AveragePropertyRating[]))]
        public IHttpActionResult GetAveragePropertyRatings(string productId)
        {
            var result = _favoritePropertyService.GetAveragePropertyRatings(productId);
            return Ok(result);
        }
    }
}