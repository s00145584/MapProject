using MapProject.DAL;
using MapProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MapProject.Controllers
{
    public class OwnerController : Controller
    {
        private ILocationRepo repo;

        public OwnerController()
        {
            repo = new LocationRepo(new LocationContext());
        }

        // GET: Owner
        public ActionResult Index(string OwnerId)
        {
            return View(repo.GetByOwnerID(OwnerId));
        }
    }
}