using System;
using System.Collections.Generic;

namespace FastBusMVC.Models
{
    public partial class SpBooking
    {
        public int BookId { get; set; }
        public int? UId { get; set; }
        public DateTime BookingDateTime { get; set; }
        public string RequiredSeats { get; set; } = null!;
        public string Perpose { get; set; } = null!;

        public virtual User? UIdNavigation { get; set; }
    }
}
