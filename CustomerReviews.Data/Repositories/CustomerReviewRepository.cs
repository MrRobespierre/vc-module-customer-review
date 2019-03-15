using System.Data.Entity;
using System.Linq;
using CustomerReviews.Data.Model;
using VirtoCommerce.Platform.Data.Infrastructure;
using VirtoCommerce.Platform.Data.Infrastructure.Interceptors;

namespace CustomerReviews.Data.Repositories
{
    public class CustomerReviewRepository : EFRepositoryBase, ICustomerReviewRepository
    {
        public CustomerReviewRepository()
        {
        }

        public CustomerReviewRepository(string nameOrConnectionString, params IInterceptor[] interceptors)
            : base(nameOrConnectionString, null, interceptors)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public IQueryable<FavoritePropertyEntity> FavoriteProperties => GetAsQueryable<FavoritePropertyEntity>();

        public IQueryable<CustomerReviewEntity> CustomerReviews =>
            GetAsQueryable<CustomerReviewEntity>()
                .Include(x => x.PropertyValues)
                .Include(x => x.PropertyValues
                               .Select(y => y.Property));

        public FavoritePropertyEntity[] GetProductFavoriteProperties(string productId) => 
            FavoriteProperties.Where(x => x.ProductId == productId).ToArray();

        public CustomerReviewEntity GetCustomerReview(string id) => 
            CustomerReviews.FirstOrDefault(x => x.Id == id);

        public CustomerReviewEntity[] GetByIds(string[] ids) => 
            CustomerReviews.Where(x => ids.Contains(x.Id)).ToArray();

        public void DeleteCustomerReviews(string[] ids)
        {
            var items = GetByIds(ids);
            foreach (CustomerReviewEntity item in items)
            {
                Remove(item);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerReviewEntity>().ToTable("CustomerReview").HasKey(x => x.Id).Property(x => x.Id);
            modelBuilder.Entity<FavoritePropertyEntity>().ToTable("FavoriteProperty").HasKey(x => x.Id).Property(x => x.Id);
            modelBuilder.Entity<FavoritePropertyValueEntity>()
                        .ToTable("FavoritePropertyValue")
                        .HasKey(x => x.Id)
                        .Property(x => x.Id);
            modelBuilder.Entity<FavoritePropertyValueEntity>()
                        .HasRequired(x => x.Review)
                        .WithMany(x => x.PropertyValues)
                        .HasForeignKey(x => x.ReviewId)
                        .WillCascadeOnDelete(true);
            modelBuilder.Entity<FavoritePropertyValueEntity>()
                        .HasRequired(x => x.Property)
                        .WithMany()
                        .HasForeignKey(x => x.PropertyId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
