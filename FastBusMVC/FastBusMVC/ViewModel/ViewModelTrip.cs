using Microsoft.AspNetCore.Mvc.Rendering;

namespace FastBusMVC.ViewModel
{
    public class ViewModelTrip
    {

        //public List<int> TripIds { get; set; }
        public List<SelectListItem> DeparureCitys { get; set; } = null!;
        public List<SelectListItem> ArrivalCitys { get; set; } = null!;
        //public List<DateTime> DeparureDataTimes { get; set; }
       
        //public List<DateTime> ArrivalDateTimes { get; set; }
        //public List<int> Prices { get; set; }
        //public List<string> TripImages { get; set; }
       
    }
}
