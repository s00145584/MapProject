using MapProject.DAL;
using MapProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MapProject.Controllers
{
    public class OwnerController : Controller
    {
        private ILocationRepo repo;

        SqlDataReader rdr = null;

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

        public OwnerController()
        {
            repo = new LocationRepo(new LocationContext());
        }

        // GET: Owner
        public ActionResult Index()
        {
            var test = Request.Cookies["OwnerId"];
            try
            {
                ViewBag.OwnerId = test.Value;
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
                throw;
            }
            
            return View(repo.GetByOwnerID(test.Value));
        }

        [AllowAnonymous]
        public ActionResult AddPlace()
        {
            return PartialView();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddPlace(LocationViewModel model)
        {
            
            //if (ModelState.IsValid)
            //{
                using (connection)
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.addPlace", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = model.Name;
                        cmd.Parameters.Add("@latitude", SqlDbType.VarChar).Value = model.Latitude;
                        cmd.Parameters.Add("@longitude", SqlDbType.VarChar).Value = model.Longitude;
                        cmd.Parameters.Add("@url", SqlDbType.VarChar).Value = model.Url;
                        cmd.Parameters.Add("@ownerId", SqlDbType.VarChar).Value = model.OwnerId;
                        cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = model.Description;
                        cmd.Parameters.Add("@time", SqlDbType.VarChar).Value = model.Time;

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            //return RedirectToAction("LogOff", "Account");
            //return RedirectToAction("Index", "Home");
            //}

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index", "Owner");
        }
    }
}