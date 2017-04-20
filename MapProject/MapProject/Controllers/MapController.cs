using MapProject.DAL;
using MapProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MapProject.Controllers
{
    public class MapController : Controller
    {
        private ILocationRepo repo;
        private IPlannedTripsRepo repo2;

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
            return View(repo.GetAll());
        }

        [AllowAnonymous]
        public ActionResult _PlannedTrips()
        {
            return PartialView(repo2.GetAll());
        }
    }
}