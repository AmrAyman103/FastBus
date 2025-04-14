using System;
using System.Collections.Generic;

namespace FastBusMVC.Models
{
    public partial class Bus
    {
        public Bus()
        {
            TripBus = new HashSet<TripBu>();
        }

        public int BusId { get; set; }
        public string BusTybe { get; set; } = null!;

        public virtual ICollection<TripBu> TripBus { get; set; }
    }
}
