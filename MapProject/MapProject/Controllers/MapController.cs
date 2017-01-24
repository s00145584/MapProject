﻿using MapProject.DAL;
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

        public MapController()
        {
            repo = new LocationRepo(new LocationContext());
        }

        // GET: Map
        public ActionResult Index()
        {
            return View(repo.GetAll());
        }
    }
}