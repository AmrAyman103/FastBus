using System;
using System.Collections.Generic;

namespace FastBusMVC.Models
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
            SpBookings = new HashSet<SpBooking>();
        }

        public int UId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public string Gendr { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string BathDate { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? UImage { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<SpBooking> SpBookings { get; set; }
    }
}
