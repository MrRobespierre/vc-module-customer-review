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

        public IQueryable<FavoritePropertyEntity> FavoriteProperties => GetAsQueryable<FavoritePropertyEntity>();

        public IQueryable<CustomerReviewEntity> CustomerReviews => GetAsQueryable<CustomerReviewEntity>().Include(x => x.PropertyValues);

        public FavoritePropertyEntity[] GetProductFavoriteProperties(string productId)
        {
            FavoritePropertyEntity[] result = FavoriteProperties.Where(x => x.ProductId == productId).ToArray();
            return result;
        }

        public CustomerReviewEntity GetCustomerReview(string id)
        {
            return CustomerReviews.FirstOrDefault(x => x.Id == id);
        }

        public CustomerReviewEntity[] GetByIds(string[] ids)
        {
            CustomerReviewEntity[] result = CustomerReviews.Where(x => ids.Contains(x.Id))
                                                           .ToArray();

            return result;
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
