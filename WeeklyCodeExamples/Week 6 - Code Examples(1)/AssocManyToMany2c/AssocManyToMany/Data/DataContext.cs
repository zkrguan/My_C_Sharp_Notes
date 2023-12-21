using System.Data.Entity;

namespace AssocManyToMany.Data
{

    public partial class DataContext : DbContext
    {
        public DataContext() : base("name=DataContext")
        { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<JobDuty> JobDuties { get; set; }

    }
}
