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
using System.IO;
using System.Drawing;

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
            List<SelectListItem> list = new List<SelectListItem>();
            using (connection)
            {
                SqlCommand cmd = new SqlCommand("dbo.GetCategories", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    list.Add(new SelectListItem
                    {
                        Text = (string)rdr["Category"],
                        Value = rdr["Id"].ToString()
                    });
                }
                connection.Close();
            }

            ViewBag.Categories = list;

            return View(repo.GetByOwnerID(test.Value));
        }
        //[HttpGet]
        //[AllowAnonymous]
        public PartialViewResult AddPlace(string id)
        {
            if (id == null)
            {
                // Create new record (this is the view in Create mode)
                return PartialView();
            }
            else
            {
                LocationViewModel Location = null;
                // Edit record (view in Edit mode)
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("dbo.GetLocation", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@LocationID", SqlDbType.Int).Value = Int32.Parse(id);

                    connection.Open();
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Location = new LocationViewModel() { Id = (int)rdr["Id"], Name = (string)rdr["Name"], Latitude = (float)rdr["Latitude"], Longitude = (float)rdr["Longitude"], OwnerId = (string)rdr["OwnerId"], Description = (string)rdr["Description"], Website = (string)rdr["Website"], CategoryId = (int)rdr["CategoryId"] };
                    }
                    connection.Close();
                    if (Location == null) { return PartialView("_error"); }
                    // ...
                    return PartialView(Location);
                }
            }
            //return PartialView();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddPlace(LocationViewModel x)
        //{
        //    if (x.id == null)
        //    {
        //        // insert new record
        //    }
        //    else
        //    {
        //        // update record
        //    }
        //}

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddPlace(LocationViewModel model)
        {

            HttpPostedFileBase file = Request.Files["iPicture"];
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(file.InputStream);
            imageBytes = reader.ReadBytes((int)file.ContentLength);

            //if (ModelState.IsValid)
            //{
            using (connection)
            {
                using (SqlCommand cmd = new SqlCommand("dbo.addPlace", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = model.Name;
                    cmd.Parameters.Add("@latitude", SqlDbType.Real).Value = model.Latitude;
                    cmd.Parameters.Add("@longitude", SqlDbType.Real).Value = model.Longitude;
                    cmd.Parameters.Add("@imageBytes", SqlDbType.VarBinary).Value = imageBytes;
                    cmd.Parameters.Add("@ownerId", SqlDbType.NVarChar).Value = model.OwnerId;
                    cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = model.Description;
                    cmd.Parameters.Add("@time", SqlDbType.Real).Value = model.Time;
                    cmd.Parameters.Add("@website", SqlDbType.NVarChar).Value = model.Website;
                    cmd.Parameters.Add("@category", SqlDbType.Int).Value = model.CategoryId;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index", "Owner");

            //if (model.Name == null)
            //{
            //    HttpPostedFileBase file = Request.Files["iPicture"];
            //    byte[] imageBytes = null;
            //    BinaryReader reader = new BinaryReader(file.InputStream);
            //    imageBytes = reader.ReadBytes((int)file.ContentLength);

            //    //if (ModelState.IsValid)
            //    //{
            //    using (connection)
            //    {
            //        using (SqlCommand cmd = new SqlCommand("dbo.addPlace", connection))
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;

            //            cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = model.Name;
            //            cmd.Parameters.Add("@latitude", SqlDbType.Real).Value = model.Latitude;
            //            cmd.Parameters.Add("@longitude", SqlDbType.Real).Value = model.Longitude;
            //            cmd.Parameters.Add("@imageBytes", SqlDbType.VarBinary).Value = imageBytes;
            //            cmd.Parameters.Add("@ownerId", SqlDbType.NVarChar).Value = model.OwnerId;
            //            cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = model.Description;
            //            cmd.Parameters.Add("@time", SqlDbType.Real).Value = model.Time;
            //            cmd.Parameters.Add("@website", SqlDbType.NVarChar).Value = model.Website;
            //            cmd.Parameters.Add("@category", SqlDbType.Int).Value = model.CategoryId;

            //            connection.Open();
            //            cmd.ExecuteNonQuery();
            //        }
            //    }

            //    return PartialView("AddPlace", "Owner");
            //}
            //else
            //{
            //    HttpPostedFileBase file = Request.Files["iPicture"];
            //    byte[] imageBytes = null;
            //    BinaryReader reader = new BinaryReader(file.InputStream);
            //    imageBytes = reader.ReadBytes((int)file.ContentLength);

            //    //if (ModelState.IsValid)
            //    //{
            //    string proc;
            //    if (imageBytes != null)
            //    {
            //        proc = "dbo.updatePlace";
            //    }
            //    else
            //    {
            //        proc = "dbo.updatePlacePic";
            //    }
            //    using (connection)
            //    {
            //        using (SqlCommand cmd = new SqlCommand(proc, connection))
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;

            //            cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = model.Id;
            //            cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = model.Name;
            //            cmd.Parameters.Add("@latitude", SqlDbType.Real).Value = model.Latitude;
            //            cmd.Parameters.Add("@longitude", SqlDbType.Real).Value = model.Longitude;
            //            if (imageBytes!=null)
            //            {
            //                cmd.Parameters.Add("@imageBytes", SqlDbType.VarBinary).Value = imageBytes;
            //            }
            //            cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = model.Description;
            //            cmd.Parameters.Add("@website", SqlDbType.NVarChar).Value = model.Website;
            //            cmd.Parameters.Add("@category", SqlDbType.Int).Value = model.CategoryId;

            //            connection.Open();
            //            cmd.ExecuteNonQuery();
            //        }
            //    }

            //    return PartialView("AddPlace", "Owner");
            //}

            //HttpPostedFileBase file = Request.Files["iPicture"];
            //byte[] imageBytes = null;
            //BinaryReader reader = new BinaryReader(file.InputStream);
            //imageBytes = reader.ReadBytes((int)file.ContentLength);

            ////if (ModelState.IsValid)
            ////{
            //using (connection)
            //    {
            //        using (SqlCommand cmd = new SqlCommand("dbo.addPlace", connection))
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;

            //            cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = model.Name;
            //            cmd.Parameters.Add("@latitude", SqlDbType.Real).Value = model.Latitude;
            //            cmd.Parameters.Add("@longitude", SqlDbType.Real).Value = model.Longitude;
            //            cmd.Parameters.Add("@imageBytes", SqlDbType.VarBinary).Value = imageBytes;
            //            cmd.Parameters.Add("@ownerId", SqlDbType.NVarChar).Value = model.OwnerId;
            //            cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = model.Description;
            //            cmd.Parameters.Add("@time", SqlDbType.Real).Value = model.Time;
            //            cmd.Parameters.Add("@website", SqlDbType.NVarChar).Value = model.Website;
            //            cmd.Parameters.Add("@category", SqlDbType.NVarChar).Value = model.CategoryId;

            //            connection.Open();
            //            cmd.ExecuteNonQuery();
            //        }
            //    }
            ////return RedirectToAction("LogOff", "Account");
            ////return RedirectToAction("Index", "Home");
            ////}

            //// If we got this far, something failed, redisplay form
            //return RedirectToAction("Index", "Owner");
        }
    }
}