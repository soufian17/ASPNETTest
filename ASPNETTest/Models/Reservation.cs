using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPNETTest.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; } = setStartTime();

        public DateTime End { get; set; } = setEndTime();

        public ApplicationUser ApplicationUser { get; set; }


        public static DateTime setStartTime()
        {
            DateTime s = DateTime.Now;
            TimeSpan ts = new TimeSpan(7, 0, 0);
            return s = s.Date + ts;
        }
        public static DateTime setEndTime()
        {
            DateTime s = DateTime.Now;
            TimeSpan ts = new TimeSpan(17, 0, 0);
            return s = s.Date + ts;
        }
    }
}