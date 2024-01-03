using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssocManyToMany.Data
{
    public class Employee
    {
        public Employee()
        {
            JobDuties = new HashSet<JobDuty>();
        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Office { get; set; }

        // Optional to-many job duties
        public ICollection<JobDuty> JobDuties { get; set; }
    }
}