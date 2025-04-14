using System;
using System.Collections.Generic;

namespace FastBusMVC.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Trips = new HashSet<Trip>();
        }

        public int AdminId { get; set; }
        public string PassWord { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string AdminType { get; set; } = null!;

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
