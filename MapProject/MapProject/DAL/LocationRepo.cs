using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapProject.Models;

namespace MapProject.DAL
{
    public class LocationRepo : ILocationRepo
    {
        public LocationContext context;

        public LocationRepo(LocationContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public List<LocationViewModel> GetAll()
        {
            return context.Locations.ToList();
        }

        public List<LocationViewModel> GetByID(int id)
        {
            return (context.Locations.Where(lid => lid.Id == id)).ToList();
        }

        public List<LocationViewModel> GetByOwnerID(string id)
        {
            return (context.Locations.Where(lid => lid.OwnerId == id)).ToList();
        }
    }
}