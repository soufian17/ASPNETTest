using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASPNETTest.Models;

namespace ASPNETTest.ViewModels
{
    public class ReservationViewModel
    {
        public int ReservationId { get; set; }
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}