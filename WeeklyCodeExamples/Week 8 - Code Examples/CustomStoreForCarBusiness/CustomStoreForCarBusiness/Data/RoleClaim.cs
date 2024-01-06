using System.ComponentModel.DataAnnotations;

namespace CustomStoreForCarBusiness.Data
{
    // TODO 01 - Class that defines a role claim for the app
    // The Home controller Index() method calls the LoadData() method in the Manager
    // The LoadData() method defines all the allowable role claims for the app
    public class RoleClaim
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}