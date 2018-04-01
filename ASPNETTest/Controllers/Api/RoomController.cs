using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ASPNETTest.Models;

namespace ASPNETTest.Controllers.Api
{
    [Authorize (Roles = "User")]
    public class RoomController : ApiController
    {
        public ApplicationDbContext context { get; set; }

        public RoomController()
        {
            context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }
        [HttpDelete]
        public void DeleteRoom(int Id)
        {
            var roomInDb = context.Rooms.SingleOrDefault(r => r.Id == Id);
            if(roomInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            context.Rooms.Remove(roomInDb);
            context.SaveChanges();
        }
    }
}
