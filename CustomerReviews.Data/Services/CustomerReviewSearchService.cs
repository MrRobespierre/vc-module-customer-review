using System;
using System.Collections.Generic;
using System.Linq;

using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;
using CustomerReviews.Data.Model;
using CustomerReviews.Data.Repositories;

using VirtoCommerce.Domain.Commerce.Model.Search;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure;


namespace CustomerReviews.Data.Services
{
    public sealed class CustomerReviewSearchService : ServiceBase, ICustomerReviewSearchService
    {
        private readonly Func<ICustomerReviewRepository> _repositoryFactory;
        private readonly ICustomerReviewService _customerReviewService;

        public CustomerReviewSearchService(Func<ICustomerReviewRepository> repositoryFactory, 
            ICustomerReviewService customerReviewService)
        {
            _repositoryFactory = repositoryFactory;
            _customerReviewService = customerReviewService;
        }

        public GenericSearchResult<CustomerReview> SearchCustomerReviews(CustomerReviewSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException($"{ nameof(criteria) } must be set");
            }

            var retVal = new GenericSearchResult<CustomerReview>();

            using (ICustomerReviewRepository repository = _repositoryFactory())
            {
                IQueryable<CustomerReviewEntity> query = repository.CustomerReviews;

                if (!criteria.ProductIds.IsNullOrEmpty())
                {
                    query = query.Where(x => criteria.ProductIds.Contains(x.ProductId));
                }

                if (criteria.IsActive.HasValue)
                {
                    query = query.Where(x => x.IsActive == criteria.IsActive);
                }

                if (!criteria.SearchPhrase.IsNullOrEmpty())
                {
                    query = query.Where(x => x.Content.Contains(criteria.SearchPhrase));
                }

                SortInfo[] sortInfos = criteria.SortInfos;
                if (sortInfos.IsNullOrEmpty())
                {
                    sortInfos = new[] { new SortInfo { SortColumn = "CreatedDate", SortDirection = SortDirection.Descending } };
                }
                query = query.OrderBySortInfos(sortInfos);

                retVal.TotalCount = query.Count();

                List<string> customerReviewIds = query.Skip(criteria.Skip)
                                 .Take(criteria.Take)
                                 .Select(x => x.Id)
                                 .ToList();

                retVal.Results = _customerReviewService.GetByIds(customerReviewIds.ToArray())
                                                       .OrderBy(x => customerReviewIds.IndexOf(x.Id)).ToList();
                return retVal;
            }
        }
    }
}
