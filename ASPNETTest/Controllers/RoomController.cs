using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ASPNETTest.Models;
using ASPNETTest.ViewModels;


namespace ASPNETTest.Controllers
{
    [Authorize (Roles = "User")]
    public class RoomController : Controller
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

        // GET: Rooms

        public ActionResult Index()
        {
            var modelToReturn = new List<RoomListViewModel>();
            //            var rooms = context.Rooms.ToList();
            //            var reservations = context.Reservations.ToList();
            //            foreach (var room in rooms)
            //            {
            //                var viewmodel = new RoomListViewModel();
            //                foreach (var reservation in reservations)
            //                {
            //                    if (reservation.End >= DateTime.Now)
            //                    {
            //                        viewmodel.Room = room;
            //                        viewmodel.ReservationStatus = "Reserved";
            //                        modelToReturn.Add(viewmodel);
            //                    }
            //                }
            //                viewmodel.Room = room;
            //                viewmodel.ReservationStatus = "Free";
            //                modelToReturn.Add(viewmodel);
            //
            //            }

            var allFreeRooms =
                from room in context.Rooms
                from res in context.Reservations.Where(x => x.RoomId == room.Id).DefaultIfEmpty()
                where res.End < DateTime.Now || res.End == null
                select room;
            foreach (var freeRoom in allFreeRooms)
            {
                var viewmodel = new RoomListViewModel();
                viewmodel.Room = freeRoom;
                viewmodel.RoomId = freeRoom.Id;
                viewmodel.ReservationStatus = "Free";
                modelToReturn.Add(viewmodel);
            }


            var allReservedRooms =
                from room in context.Rooms
                from res in context.Reservations.Where(x => x.RoomId == room.Id)
                select room;
            foreach (var reservedRoom in allReservedRooms)
            {
                var viewmodel = new RoomListViewModel();
                viewmodel.Room = reservedRoom;
                viewmodel.RoomId = reservedRoom.Id;
                viewmodel.ReservationStatus = "Reserved";
                modelToReturn.Add(viewmodel);
            }
//            var test = 
//                from r in context.Rooms 
//                from res in context.Reservations.Where()

            var r = context.Database.SqlQuery<RoomListViewModel>("SELECT IIF(res.\"End\" > SYSDATETIME() and res.\"Start\" < SYSDATETIME(),'Reserved','Free') as \"ReservationStatus\",r.Id as \"RoomId\", res.\"Start\", res.\"End\" FROM ROOMS r left JOIN Reservations res on r.Id = res.RoomId ;").ToList();
            foreach (var v in r)
            {
               v.setRoom(context.Rooms.SingleOrDefault(x => x.Id == v.RoomId));
            }
            return View(r);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                context.Rooms.Add(room);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room);
        }
        [AllowAnonymous]
        public ActionResult AvaliableRooms()
        {
//            var queryInSql = 
//                "SELECT R.ID FROM ROOMS R " +
//                "LEFT JOIN RESERVATIONS RES ON RES.ROOMID = R.ID " +
//                "WHERE RES.\"End\" < GETDATE() OR  RES.\"End\" is null;";


            var queryInLinq = 
                from room in context.Rooms
                from res in context.Reservations.Where(x => x.RoomId == room.Id).DefaultIfEmpty()// defaultIfEmpty is hoe je een left join voor elkaar krijgt
                where res.End < DateTime.Now || res.End == null //als deze line wegvalt krijg je alleen Room1 te zien (die is gereserveerd)
                select room ;

//            var roomsToReturn = new List<Room>();
//            var rooms = context.Rooms.ToList();
//            var reservations = context.Reservations.ToList();
//            foreach (var room in rooms)
//            {
//                foreach (var reservation in reservations)
//                {
//                    if (reservation.End >= DateTime.Now )
//                    {
//                    }
//                }
//                roomsToReturn.Add(room);
//            }
            return View(queryInLinq);
        }

        public ActionResult Edit(int Id)
        {
            var room = context.Rooms.SingleOrDefault(r=>r.Id ==Id);
            if (room == null)
                return HttpNotFound();
            return View(room);
        }

        [HttpPost]
        public ActionResult Edit(int Id, Room room)
        {
            var roomInDb = context.Rooms.SingleOrDefault(r => r.Id == Id);
            if (roomInDb == null)
                return HttpNotFound();

            roomInDb.Name = room.Name;
            roomInDb.RoomNumber = room.RoomNumber;

            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}