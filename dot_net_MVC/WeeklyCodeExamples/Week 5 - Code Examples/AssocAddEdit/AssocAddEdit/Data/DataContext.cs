using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AssocAddEdit.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext() : base("name=DataContext")
        { }


        public DbSet<Country> Countries { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Classification> Classifications { get; set; }


        // TODO: 02 - Prevent "delete cascade" from happening

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // The default behaviour for the one (Manufacturer) to many (Vehicles)
            // association is "cascade delete"
            // This means that deleting a Manufacturer will cause the Vehicles to be deleted
            // In this code example, we do NOT want that behaviour

            // We cannot do this with a data annotation
            // We must use this Fluent API

            // First, call the base OnModelCreating method,
            // which uses the existing class definitions and conventions

            base.OnModelCreating(modelBuilder);

            // Then, change the "cascade delete" setting
            // This can be done in at least two ways; un-comment the desired code

            // Alternative #1
            // Turn off "cascade delete" for all default convention-based associations

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Alternative #2
            // Turn off "cascade delete" for a specific association

            //modelBuilder.Entity<Vehicle>()
            //    .HasRequired(m => m.Manufacturer)
            //    .WithMany(v => v.Vehicles)
            //    .WillCascadeOnDelete(false);
        }
    }
}
