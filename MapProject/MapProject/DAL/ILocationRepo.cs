using MapProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapProject.DAL
{
    public interface ILocationRepo : IDisposable
    {
        List<LocationViewModel> GetAll();
        List<LocationViewModel> GetByID(int id);
        List<LocationViewModel> GetByOwnerID(string id);

    }
}