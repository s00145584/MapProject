using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MapProject.Models
{
    public class PlannedTripsModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public byte[] Picture { get; set; }
    }

    //public class PlannedTripsContext : DbContext
    //{
    //    public DbSet<PlannedTripsModel> Trips { get; set; }

    //    public PlannedTripsContext() : base("DefaultConnection") { }
    //}
}