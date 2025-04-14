using FastBusMVC.Models;

namespace FastBusMVC.ViewModel
{
    public class AvailableTripsViewModel
    {
        public List<Trip> Trips { get; set; }
        public int totalPriceMV {  get; set; } 
        public int ticketCountMV {  get; set; }
       public int TripIdVM {  get; set; }   
        public List<int> TripIds { get; set; }
        public List<string> DeparureCitys { get; set; }
        public List<string> ArrivalCitys { get; set; }
        public List<int> Prices { get; set; }
        public List<DateTime> DeparureDataTimes { get; set; }
        public List<DateTime> ArrivalDateTimes { get; set; }
        public List<string> TripImages { get; set; }
    }

    

}
