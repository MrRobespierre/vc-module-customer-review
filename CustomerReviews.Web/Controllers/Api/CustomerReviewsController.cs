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
    public sealed class CustomerReviewsController : ApiController
    {
        private readonly ICustomerReviewSearchService _customerReviewSearchService;
        private readonly ICustomerReviewService _customerReviewService;

        public CustomerReviewsController()
        {
        }

        public CustomerReviewsController(
            ICustomerReviewSearchService customerReviewSearchService,
            ICustomerReviewService customerReviewService)
        {
            _customerReviewSearchService = customerReviewSearchService;
            _customerReviewService = customerReviewService;
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
        /// Gets an average product rating based on active reviews.
        /// </summary>
        [HttpGet]
        [Route("getAverageProductRating/{productId}")]
        [ResponseType(typeof(AverageProductRating))]
        public IHttpActionResult GetAverageProductRating(string productId)
        {
            var criteria = new CustomerReviewSearchCriteria
            {
                IsActive = true,
                ProductIds = new[] { productId }
            };
            var reviews = _customerReviewSearchService.SearchCustomerReviews(criteria);
            if (reviews.TotalCount == 0)
                return Ok(new AverageProductRating(productId, 0, 0));

            double averageRating = reviews.Results.Average(x => x.ProductRating);
            return Ok(new AverageProductRating(productId, averageRating, reviews.TotalCount));
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
            _customerReviewService.SaveCustomerReviews(customerReviews);
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
    }
}
