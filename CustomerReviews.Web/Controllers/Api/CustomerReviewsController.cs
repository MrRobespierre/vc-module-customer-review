using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;
using CustomerReviews.Web.Security;
using VirtoCommerce.Domain.Commerce.Model.Search;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Web.Security;


namespace CustomerReviews.Web.Controllers.Api
{
    [RoutePrefix("api/customerReviews")]
    public class CustomerReviewsController : ApiController
    {
        private readonly ICustomerReviewSearchService _customerReviewSearchService;
        private readonly ICustomerReviewService _customerReviewService;
        private readonly IFavoritePropertyService _favoritePropertyService;

        public CustomerReviewsController()
        {
        }

        public CustomerReviewsController(
            ICustomerReviewSearchService customerReviewSearchService,
            ICustomerReviewService customerReviewService,
            IFavoritePropertyService favoritePropertyService)
        {
            _customerReviewSearchService = customerReviewSearchService;
            _customerReviewService = customerReviewService;
            _favoritePropertyService = favoritePropertyService;
        }

        /// <summary>
        /// Gets customer review by id.
        /// </summary>
        /// <param name="id">The customer review id.</param>
        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(CustomerReview))]
        public IHttpActionResult GetCustomerReview(string id)
        {
            CustomerReview customerReview = _customerReviewService.GetById(id);
            return Ok(customerReview);
        }

        /// <summary>
        /// Return product Customer review search results
        /// </summary>
        [HttpPost]
        [Route("search")]
        [ResponseType(typeof(GenericSearchResult<CustomerReview>))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewRead)]
        public IHttpActionResult SearchCustomerReviews(CustomerReviewSearchCriteria criteria)
        {
            GenericSearchResult<CustomerReview> result = _customerReviewSearchService.SearchCustomerReviews(criteria);
            return Ok(result);
        }

        /// <summary>
        ///  Create new or update existing customer review
        /// </summary>
        /// <param name="customerReviews">Customer reviews</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewUpdate)]
        public IHttpActionResult Update(CustomerReview[] customerReviews)
        {
            foreach (var customerReview in customerReviews)
            {
                _customerReviewService.SaveCustomerReview(customerReview);
            }
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete Customer Reviews by IDs
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewDelete)]
        public IHttpActionResult Delete([FromUri] string[] ids)
        {
            _customerReviewService.DeleteCustomerReviews(ids);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Gets an average product rating based on active reviews.
        /// </summary>
        [HttpGet]
        [Route("{productId}/averageProductRating")]
        [ResponseType(typeof(AverageProductRating))]
        public IHttpActionResult GetAverageProductRating(string productId)
        {
            var result =  _customerReviewService.GetAverageProductRating(productId);
            return Ok(result);
        }

        [HttpGet]
        [Route("{productId}/averagePropertyRatings")]
        [ResponseType(typeof(AveragePropertyRating[]))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewRead)]
        public IHttpActionResult GetAveragePropertyRatings(string productId)
        {
            var result = _favoritePropertyService.GetAveragePropertyRatings(productId);
            return Ok(result);
        }

        /// <summary>
        /// Gets favorite properties for product.
        /// </summary>
        /// <param name="productId">The Product id.</param>
        [HttpGet]
        [Route("{productId}/favoriteProperties")]
        [ResponseType(typeof(FavoriteProperty[]))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewRead)]
        public IHttpActionResult GetProductFavoriteProperties(string productId)
        {
            var result = _favoritePropertyService.GetProductFavoriteProperties(productId);
            return Ok(result);
        }
    }
}
