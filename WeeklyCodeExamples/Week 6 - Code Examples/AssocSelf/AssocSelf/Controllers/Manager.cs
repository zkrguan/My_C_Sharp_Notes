using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AssocSelf.Data;

namespace AssocSelf.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBaseViewModel>();

                // TODO 01 - Object mapper definitions

                cfg.CreateMap<Data.Employee, Controllers.EmployeeBaseViewModel>();
                cfg.CreateMap<Data.Employee, Controllers.EmployeeWithOrgInfoViewModel>();

                // TODO 02 - For the HTML Forms
                cfg.CreateMap<Controllers.EmployeeBaseViewModel, Controllers.EmployeeEditSupervisorFormViewModel>();
                cfg.CreateMap<Controllers.EmployeeBaseViewModel, Controllers.EmployeeEditDirectReportsFormViewModel>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        public IEnumerable<EmployeeBaseViewModel> EmployeeGetAll()
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeBaseViewModel>>
                (ds.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName));
        }

        public IEnumerable<EmployeeWithOrgInfoViewModel> EmployeeGetAllWithOrgInfo()
        {
            // Ugh, I hate these property names
            var c = ds.Employees.Include("DirectReports").Include("ReportsTo");

            // Return the result
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeWithOrgInfoViewModel>>
                (c.OrderBy(e => e.LastName).ThenBy(e => e.FirstName));
        }

        public EmployeeWithOrgInfoViewModel EmployeeGetByIdWIthOrgInfo(int id)
        {
            // Attempt to get the matching object
            var o = ds.Employees.Include("DirectReports").Include("ReportsTo")
                .SingleOrDefault(e => e.EmployeeId == id);

            // Return the result, or null if not found
            return (o == null) ? null : mapper.Map<Employee, EmployeeWithOrgInfoViewModel>(o);
        }

        public EmployeeWithOrgInfoViewModel EmployeeEditSupervisor(EmployeeEditSupervisorViewModel newItem)
        {
            // TODO 31 - Update the self-referencing to-one association

            // Attempt to fetch the object
            var o = ds.Employees.Include("DirectReports").Include("ReportsTo")
                .SingleOrDefault(e => e.EmployeeId == newItem.EmployeeId);

            // Attempt to fetch the associated object
            Employee a = null;
            if (newItem.ReportsToId > 0)
            {
                a = ds.Employees.Include("DirectReports").Include("ReportsTo")
                    .SingleOrDefault(e => e.EmployeeId == newItem.ReportsToId);
            }

            // Must do two tests here before continuing
            if (o == null | a == null)
            {
                // Problem - one of the items was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values
                // This will handle the standard properties
                // We could also specifically target the new/updated properties
                ds.Entry(o).CurrentValues.SetValues(newItem);

                // Configure the new supervisor
                // MUST set both properties - the int and the Employee
                o.ReportsTo = a;
                o.ReportsToId = a.EmployeeId;

                ds.SaveChanges();

                // Prepare and return the object
                return mapper.Map<Employee, EmployeeWithOrgInfoViewModel>(o);
            }
        }

        public EmployeeWithOrgInfoViewModel EmployeeEditDirectReports(EmployeeEditDirectReportsViewModel newItem)
        {
            // TODO 36 - Update the self-referencing to-many association

            // Attempt to fetch the object

            // When editing an object with a to-many collection,
            // and you wish to edit the collection,
            // MUST fetch its associated collection
            var o = ds.Employees.Include("DirectReports").Include("ReportsTo")
                .SingleOrDefault(e => e.EmployeeId == newItem.EmployeeId);

            if (o == null)
            {
                // Problem - object was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values

                // First, clear out the existing collection
                // "DirectReports" is the badly-named to-many collection property
                o.DirectReports.Clear();

                // Then, go through the incoming items
                // For each one, add to the fetched object's collection
                foreach (var item in newItem.EmployeeIds)
                {
                    var a = ds.Employees.Find(item);
                    o.DirectReports.Add(a);
                }
                // Save changes
                ds.SaveChanges();

                return mapper.Map<Employee, EmployeeWithOrgInfoViewModel>(o);
            }
        }

    }
}