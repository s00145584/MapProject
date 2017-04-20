using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapProject.Models;

namespace MapProject.DAL
{
    public interface IPlannedTripsRepo:IDisposable
    {
        List<PlannedTripsModel> GetAll();
        //List<PlannedTripsModel> GetByID(int id);
        //List<PlannedTripsModel> GetByOwnerID(string id);

    }
}