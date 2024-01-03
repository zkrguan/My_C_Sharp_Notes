using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using AssocManyToMany.Data;

namespace AssocManyToMany.Controllers
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
                cfg.CreateMap<Data.Employee, Controllers.EmployeeWithJobDutiesViewModel>();

                // TODO 02 - For the employee edit form
                cfg.CreateMap<Controllers.EmployeeBaseViewModel, Controllers.EmployeeEditJobDutiesFormViewModel>();

                cfg.CreateMap<Data.JobDuty, Controllers.JobDutyBaseViewModel>();
                cfg.CreateMap<Data.JobDuty, Controllers.JobDutyWithEmployeesViewModel>();

                // TODO 03 - For the job duty edit form
                cfg.CreateMap<Controllers.JobDutyBaseViewModel, Controllers.JobDutyEditEmployeesFormViewModel>();
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

        // ############################################################
        // Employee

        public IEnumerable<EmployeeBaseViewModel> EmployeeGetAll()
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeBaseViewModel>>(ds.Employees.OrderBy(e => e.Name));
        }

        public IEnumerable<EmployeeWithJobDutiesViewModel> EmployeeGetAllWithJobDuties()
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeWithJobDutiesViewModel>>
                (ds.Employees.Include("JobDuties").OrderBy(e => e.Name));
        }

        public EmployeeWithJobDutiesViewModel EmployeeGetByIdWithDetail(int id)
        {
            // Attempt to fetch the object
            var o = ds.Employees.Include("JobDuties").SingleOrDefault(e => e.Id == id);

            // Return the result, or null if not found
            return (o == null) ? null : mapper.Map<Employee, EmployeeWithJobDutiesViewModel>(o);
        }

        // TODO 11 - Edit an employee's job duties
        public EmployeeWithJobDutiesViewModel EmployeeEditJobDuties(EmployeeEditJobDutiesViewModel newItem)
        {
            // Attempt to fetch the object

            // When editing an object with a to-many collection,
            // and you wish to edit the collection,
            // MUST fetch its associated collection
            var employee = ds.Employees.Include("JobDuties")
                                       .SingleOrDefault(e => e.Id == newItem.Id);

            if (employee == null)
            {
                // Problem - object was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values

                // First, clear out the existing collection
                employee.JobDuties.Clear();

                // Then, go through the incoming items
                // For each one, add to the fetched object's collection
                foreach (var jobId in newItem.JobDutyIds)
                {
                    var jobDuty = ds.JobDuties.Find(jobId);
                    employee.JobDuties.Add(jobDuty);
                }
                // Save changes
                ds.SaveChanges();

                return mapper.Map<Employee, EmployeeWithJobDutiesViewModel>(employee);
            }
        }

        // ############################################################
        // JobDuty

        public IEnumerable<JobDutyBaseViewModel> JobDutyGetAll()
        {
            return mapper.Map<IEnumerable<JobDuty>, IEnumerable<JobDutyBaseViewModel>>(ds.JobDuties.OrderBy(j => j.Name));
        }

        public IEnumerable<JobDutyWithEmployeesViewModel> JobDutyGetAllWithEmployees()
        {
            return mapper.Map<IEnumerable<JobDuty>, IEnumerable<JobDutyWithEmployeesViewModel>>
                (ds.JobDuties.Include("Employees").OrderBy(j => j.Name));
        }

        public JobDutyWithEmployeesViewModel JobDutyGetByIdWithDetail(int id)
        {
            // Attempt to fetch the object
            var o = ds.JobDuties.Include("Employees").SingleOrDefault(j => j.Id == id);

            // Return the result, or null if not found
            return (o == null) ? null : mapper.Map<JobDuty, JobDutyWithEmployeesViewModel>(o);
        }

        // TODO 11 - Edit a job duty's list of employees
        public JobDutyWithEmployeesViewModel JobDutyEditEmployees(JobDutyEditEmployeesViewModel newItem)
        {
            // Attempt to fetch the object

            // When editing an object with a to-many collection,
            // and you wish to edit the collection,
            // MUST fetch its associated collection
            var o = ds.JobDuties.Include("Employees")
                .SingleOrDefault(e => e.Id == newItem.Id);

            if (o == null)
            {
                // Problem - object was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values

                // First, clear out the existing collection
                o.Employees.Clear();

                // Then, go through the incoming items
                // For each one, add to the fetched object's collection
                foreach (var item in newItem.EmployeeIds)
                {
                    var a = ds.Employees.Find(item);
                    o.Employees.Add(a);
                }
                // Save changes
                ds.SaveChanges();

                return mapper.Map<JobDuty, JobDutyWithEmployeesViewModel>(o);
            }
        }
    }
}