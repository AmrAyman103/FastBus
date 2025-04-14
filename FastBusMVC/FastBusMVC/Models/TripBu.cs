using System;
using System.Collections.Generic;

namespace FastBusMVC.Models
{
    public partial class TripBu
    {
        public int Code { get; set; }
        public int? SeatNumber { get; set; }
        public int? BusId { get; set; }
        public int? TripId { get; set; }

        public virtual Bus? Bus { get; set; }
        public virtual Trip? Trip { get; set; }
    }
}
