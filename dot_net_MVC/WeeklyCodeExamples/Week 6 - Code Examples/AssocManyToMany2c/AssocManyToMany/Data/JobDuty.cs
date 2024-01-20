using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssocManyToMany.Data
{
    public class JobDuty
    {

        public JobDuty()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(1000)]
        public string Description { get; set; }

        // Optional to-many employees
        public ICollection<Employee> Employees { get; set; }
    }

}