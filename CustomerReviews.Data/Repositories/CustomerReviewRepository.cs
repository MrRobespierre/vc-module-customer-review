using System.Data.Entity;
using System.Linq;
using CustomerReviews.Data.Model;
using VirtoCommerce.Platform.Data.Infrastructure;
using VirtoCommerce.Platform.Data.Infrastructure.Interceptors;

namespace CustomerReviews.Data.Repositories
{
    public sealed class CustomerReviewRepository : EFRepositoryBase, ICustomerReviewRepository
    {
        public CustomerReviewRepository()
        {
        }

        public CustomerReviewRepository(string nameOrConnectionString, params IInterceptor[] interceptors)
            : base(nameOrConnectionString, null, interceptors)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public IQueryable<CustomerReviewEntity> CustomerReviews => GetAsQueryable<CustomerReviewEntity>();
        
        public CustomerReviewEntity[] GetByIds(string[] ids)
        {
            return CustomerReviews.Where(x => ids.Contains(x.Id)).ToArray();
        }

        public void DeleteCustomerReviews(string[] ids)
        {
            CustomerReviewEntity[] items = GetByIds(ids);
            foreach (CustomerReviewEntity item in items)
            {
                Remove(item);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerReviewEntity>().ToTable("CustomerReview").HasKey(x => x.Id).Property(x => x.Id);
            modelBuilder.Entity<FavoritePropertyEntity>().ToTable("FavoriteProperty").HasKey(x => x.Id).Property(x => x.Id);
            modelBuilder.Entity<FavoritePropertyValueEntity>().ToTable("FavoritePropertyValue").HasKey(x => x.Id).Property(x => x.Id);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
