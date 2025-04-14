using System;
using System.Collections.Generic;

namespace FastBusMVC.Models
{
    public partial class Trip
    {
        public Trip()
        {
            Bookings = new HashSet<Booking>();
            TTickets = new HashSet<TTicket>();
            TripBus = new HashSet<TripBu>();
        }

        public int TripId { get; set; }
        public string DeparureCity { get; set; } = null!;
        public DateTime DeparureDataTime { get; set; }
        public string ArrivalCity { get; set; } = null!;
        public DateTime ArrivalDateTime { get; set; }
        public int Price { get; set; }
        public string? TripImage { get; set; }
        public int? BusId { get; set; }

        public virtual Admin? Bus { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<TTicket> TTickets { get; set; }
        public virtual ICollection<TripBu> TripBus { get; set; }
    }
}
