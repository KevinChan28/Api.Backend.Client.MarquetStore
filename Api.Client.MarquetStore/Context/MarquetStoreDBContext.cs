using Api.Client.MarquetStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Client.MarquetStore.Context
{
    public class MarquetStoreDBContext : DbContext
    {
        public MarquetStoreDBContext(DbContextOptions<MarquetStoreDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(eb =>
            {
                eb.HasKey(c => new { c.Id });
            });
            modelBuilder.Entity<Sale>(eb =>
            {
                eb.HasKey(c => new { c.Id });
            });
            modelBuilder.Entity<Address>(eb =>
            {
                eb.HasKey(c => new { c.Id });
            });
            modelBuilder.Entity<Concept>(eb =>
            {
                eb.HasKey(c => new { c.Id });
            });
            modelBuilder.Entity<Ingredients>(eb =>
            {
                eb.HasKey(c => new { c.Id });
            });
            modelBuilder.Entity<Pay>(eb =>
            {
                eb.HasKey(c => new { c.Id });
            });
            modelBuilder.Entity<PaymentsMethod>(eb =>
            {
                eb.HasKey(c => new { c.Id });
            });
            modelBuilder.Entity<Personalization>(eb =>
            {
                eb.HasKey(c => new { c.Id });
            });
            modelBuilder.Entity<Products>(eb =>
            {
                eb.HasKey(c => new { c.Id });
            });
            modelBuilder.Entity<Roles>(eb =>
            {
                eb.HasKey(c => new { c.Id });
            });
        }

        public DbSet<User> User { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Concept> Concept { get; set; }
        public DbSet<Ingredients> Ingredient { get; set; }
        public DbSet<Pay> Pay { get; set; }
        public DbSet<PaymentsMethod> PaymentsMethods { get; set; }
        public DbSet<Personalization> Personalization { get; set; }
        public DbSet<Products> Product { get; set; }
        public DbSet<Roles> Roles { get; set; }
  
    }
}
