using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssocOneToMany.Data
{
    public class Team
    {
        public Team()
        {
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }
        public string CodeName { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int YearFounded { get; set; }
        public string Conference { get; set; }
        public string Division { get; set; }
        public string Stadium { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}