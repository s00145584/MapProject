using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapProject.Models;

namespace MapProject.DAL
{
    public class PlannedTripsRepo : IPlannedTripsRepo
    {
        public LocationContext context;

        public PlannedTripsRepo(LocationContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public List<PlannedTripsModel> GetAll()
        {
            //.Where(d => d.UserId == null)
            return context.Trips.ToList();
        }
    }
}