using MapProject.DAL;
using MapProject.Models;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MapProject.Controllers
{
    public class MapController : Controller
    {
        private ILocationRepo repo;
        private IPlannedTripsRepo repo2;

        SqlDataReader rdr = null;

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

        public MapController()
        {
            repo = new LocationRepo(new LocationContext());
            repo2 = new PlannedTripsRepo(new LocationContext());
        }

        // GET: Map
        public ActionResult Index()
        {
            //List<LocationViewModel> lvm = repo.GetAll();
            //foreach (var item in lvm)
            //{
            //    string base64String = Convert.ToBase64String(item.Picture, 0, item.Picture.Length);
            //    item.Picture
            //    Image1.ImageUrl = "data:image/png;base64," + base64String;
            //}

            ViewBag.UserId = Request.Cookies["OwnerId"].Value;

            return View(repo.GetAll());
        }

        [AllowAnonymous]
        public ActionResult _PlannedTrips()
        {
            IEnumerable<PlannedTripsModel> result;
            var CurrentUserId = Request.Cookies["OwnerId"];
            if (CurrentUserId != null)
            {
                result = repo2.GetAll().Where(d => d.UserId == CurrentUserId.Value || d.UserId == null).OrderByDescending(c => c.UserId != null ? c.UserId : null);
                ViewBag.UserId = CurrentUserId.Value;

            }
            else
            {
                result = repo2.GetAll().Where(d => d.UserId == null);
            }

            return PartialView(result);
        }

        [HttpPost]
        public ActionResult GetPlannedTrips(int id)
        {
            List<int> Locations = new List<int>();

            SqlCommand cmd = new SqlCommand("dbo.GetLocations", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@TripID", SqlDbType.VarChar).Value = id;

            connection.Open();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Locations.Add((int)rdr["LocationId"]);
            }
            var test = Json(Locations);
            return test;
        }

        [HttpPost]
        public ActionResult GetSavedPlannedTrips(int id)
        {
            string LocationString ="";

            SqlCommand cmd = new SqlCommand("dbo.GetSavedLocations", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@TripID", SqlDbType.VarChar).Value = id;

            connection.Open();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                LocationString = (string)rdr["LocationsIDs"];
            }

            return Content(LocationString);
        }

        [HttpPost]
        public ActionResult SaveItinerary(string id, string[] list)
        {
            string LocationString = "";
            int result;

            foreach (var item in list)
            {
                LocationString += item+",";
            }

            LocationString = LocationString.Remove(LocationString.Length - 1);

            using (connection)
            {
                SqlCommand cmd = new SqlCommand("dbo.SaveLocations", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("@LocationIDs", SqlDbType.VarChar).Value = LocationString;

                IDbDataParameter rVal = cmd.CreateParameter();
                rVal.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(rVal);

                connection.Open();

                cmd.ExecuteNonQuery();
                result = (int)rVal.Value;

                connection.Close();
            }

            //var test = Json(Locations);
            //return test;// Log errror
            //}
            return Content(result.ToString());
        }
    }
}