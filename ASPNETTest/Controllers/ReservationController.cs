using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ASPNETTest.Models;
using ASPNETTest.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace ASPNETTest.Controllers
{
    [Authorize (Roles = "User")]
    public class ReservationController : Controller
    {
        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public ApplicationDbContext context { get; set; }

        public ReservationController()
        {
            context = new ApplicationDbContext();
            //om met users te werken (bij het toevoegen aan een reservation is dat nodig)
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }
        [HttpPost]
        public ActionResult Reserve(ReservationViewModel model)
        {
            if (model.EndTime > model.StartTime)
            {
//                Dit is ook een manier om de user te krijgen
//                ApplicationUser user2 = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                var user = UserManager.FindById(User.Identity.GetUserId());
                var reservation = new Reservation()
                {
                    Description = model.Description,
                    End = model.EndTime,
                    Start = model.StartTime,
                    RoomId = model.RoomId,
                    ApplicationUser = user
                    
                };
                context.Reservations.Add(reservation);
                context.SaveChanges();
                return RedirectToAction("Index", "Room");
            }
            return RedirectToAction("Reservate",model.Room.Id);
        }
        public ActionResult Reservate(int id)
        {
            var RoomDb = context.Rooms.SingleOrDefault(r => r.Id == id);

            var model = new ReservationViewModel()
            {
                Room = RoomDb,
                RoomId = id,
                StartTime = Reservation.setStartTime(),
                EndTime =  Reservation.setEndTime(),
            };
            return View(model);
        }

        public ActionResult MyReservations()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var returnList = new List<ReservationViewModel>();
            var reservations =
                from res in context.Reservations
                join r in context.Rooms on res.RoomId equals r.Id 
                where res.ApplicationUser.Id == user.Id
                select new {Room = r , Reservation = res};

            foreach (var reservation in reservations)
            {
                var model = new ReservationViewModel()
                {
                    ReservationId = reservation.Reservation.Id,
                    Room = reservation.Room,
                    RoomId = reservation.Reservation.RoomId,
                    StartTime = reservation.Reservation.Start,
                    EndTime = reservation.Reservation.End,
                    Description = reservation.Reservation.Description
                };
                returnList.Add(model);
            }
            
            return View(returnList);
        }

        [HttpPost]
        public ActionResult updateDescription(int id,string description)
        {
            Console.WriteLine(id+"  "+description);
            return null;
        }

        [HttpPost]
        public ActionResult UpdateReservations(FormCollection collection)
        {
            string value = Convert.ToString(collection["Description"]);

            return null;
        }


    }

    }