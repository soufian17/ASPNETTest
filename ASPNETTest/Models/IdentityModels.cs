using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json.Linq;

namespace ASPNETTest.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
//        public List<Reservation> Reservations { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(@"Server=LAPTOP-IFE265TU\SQLEXPRESS;Database=ASPTest;User Id="+getUser()+"; Password = "+getPassword()+";", throwIfV1Schema: false)
        {
        }

        private static string getSetting(string prop)
        {
            string subfoldername = "Models";
            string filename = "settings.json";
            //In the Models folder add a settins.json file with within a nameless object with 2 variables username and password
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, subfoldername, filename);

            string result = string.Empty;
            using (StreamReader r = new StreamReader(filepath))
            {
                var json = r.ReadToEnd();
                var jobj = JObject.Parse(json);
                foreach (var item in jobj.Properties())
                {
                    if (item.Name == prop)
                        result =  item.Value.ToString();
                }
            }
            return result;
        }

        private static string getUser()
        {
            return getSetting("username");
        }

        private static string getPassword()
        {
            return getSetting("password");
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}