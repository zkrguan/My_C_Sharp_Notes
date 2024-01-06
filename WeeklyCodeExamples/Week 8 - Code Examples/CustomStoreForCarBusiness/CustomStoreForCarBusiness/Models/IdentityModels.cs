using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
// new...
using System.Data.Entity.ModelConfiguration.Conventions;
using CustomStoreForCarBusiness.Data;

namespace CustomStoreForCarBusiness.Models
{
    // Add your design model classes to the EntityModels folder. Follow these rules or conventions:
    //   The name of the integer identifier property should be "Id".
    //   Collection properties (including navigation properties) must be of type ICollection<T>
    //   Valid data annotations are pretty much limited to [Required] and [StringLength(n)]
    //   Required to-one navigation properties must include the [Required] attribute
    //   Do NOT configure scalar properties (e.g. int, double) with the [Required] attribute
    //   Initialize DateTime and collection properties in a default constructor



    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DataContext", throwIfV1Schema: false) { }

        // TODO 06 - DbSet<TEntity> properties are defined here
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }




        // Turn OFF cascade delete, which is (unfortunately) the default setting
        // for Code First generated databases
        // For most apps, we do NOT want automatic cascade delete behaviour
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // First, call the base OnModelCreating method,
            // which uses the existing class definitions and conventions

            base.OnModelCreating(modelBuilder);

            // Then, turn off "cascade delete" for 
            // all default convention-based associations

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}