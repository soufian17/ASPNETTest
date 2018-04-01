using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASPNETTest.Models;

namespace ASPNETTest.ViewModels
{
    public class RoomListViewModel
    {
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public string ReservationStatus { get; set; }

        public void setRoom(Room r)
        {
            Room = r;
        }
    }
    
}