using System;
using System.Collections.Generic;

namespace FastBusMVC.Models
{
    public partial class TTicket
    {
        public int TicketId { get; set; }
        public int? TribId { get; set; }
        public int NumberTicket { get; set; }
        public int? BookedTicket { get; set; }
        public int? AvilableTicket { get; set; }

        public virtual Trip? Trib { get; set; }
    }
}
