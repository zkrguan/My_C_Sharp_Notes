﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using PhotoProperty.Models;
using System.Security.Claims;
using PhotoProperty.Data;

namespace PhotoProperty.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // Reference to AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                // Object mapper definitions

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

                // TODO 4 - Notice the create map statements
                cfg.CreateMap<Data.Vehicle, Models.VehicleBaseViewModel>();
                cfg.CreateMap<Data.Vehicle, Models.VehiclePhotoViewModel>();
                cfg.CreateMap<Models.VehicleAddViewModel, Data.Vehicle>();

            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
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
        // Vehicle

        public IEnumerable<VehicleBaseViewModel> VehicleGetAll()
        {
            return mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleBaseViewModel>>(ds.Vehicles.OrderBy(v => v.Model).ThenByDescending(v => v.MSRP));
        }

        public VehicleBaseViewModel VehicleGetById(int id)
        {
            var o = ds.Vehicles.Find(id);

            return (o == null) ? null : mapper.Map<Vehicle, VehicleBaseViewModel>(o);
        }

        public VehiclePhotoViewModel VehiclePhotoGetById(int id)
        {
            var o = ds.Vehicles.Find(id);

            return (o == null) ? null : mapper.Map<Vehicle, VehiclePhotoViewModel>(o);
        }

        public VehicleBaseViewModel VehicleAdd(VehicleAddViewModel newItem)
        {
            // Attempt to add the new item
            var addedItem = ds.Vehicles.Add(mapper.Map<VehicleAddViewModel, Vehicle>(newItem));

            // At this time, the value of the PhotoUpload property should be non-null
            // That was defined by a [Required] attribute in the view model class

            // TODO 6 - Handle the uploaded photo...

            // First, extract the bytes from the HttpPostedFile object
            byte[] photoBytes = new byte[newItem.PhotoUpload.ContentLength];
            newItem.PhotoUpload.InputStream.Read(photoBytes, 0, newItem.PhotoUpload.ContentLength);

            // Then, configure the new object's properties
            addedItem.Photo = photoBytes;
            addedItem.PhotoContentType = newItem.PhotoUpload.ContentType;

            ds.SaveChanges();

            return (addedItem == null) ? null : mapper.Map<Vehicle, VehicleBaseViewModel>(addedItem);
        }






        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here
                ds.RoleClaims.Add(new RoleClaim { Name = "Engineering" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Sales" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Production" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}