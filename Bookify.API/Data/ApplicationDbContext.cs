using Bookify.API.Entities;

namespace Bookify.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options):base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalCopy> RentalCopies { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasSequence<int>("SerialNumber", schema: "shared")
                .StartsAt(10000001);

            builder.Entity<BookCopy>()
                .Property(b => b.SerialNumber)
                .HasDefaultValueSql("NEXT VALUE FOR shared.SerialNumber");

            //  builder.Entity<Category>().Property(e => e.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Entity<BookCategory>().HasKey(e => new { e.CategoryId, e.BookId });
            builder.Entity<RentalCopy>().HasKey(e => new { e.RentalId, e.BookCopyId });
            builder.Entity<Rental>().HasQueryFilter(r => !r.IsDeleted);
            builder.Entity<RentalCopy>().HasQueryFilter(r => !r.Rental!.IsDeleted);
            base.OnModelCreating(builder);

            // Make OnDelete the default in this applicaion will be Restrict
            var cascadeFKs = builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

        
        }

    }
}
