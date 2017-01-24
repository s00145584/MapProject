using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MapProject.Models
{
    public class LocationViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Latitude { get; set; }
        [Required]
        public float Longitude { get; set; }
        [Required]
        public string Url { get; set; }
        public string Descritpion { get; set; }
        [Required]
        public string OwnerId { get; set; }
        public float Time { get; set; }

    }

    public class LocationContext:DbContext
    {
        public DbSet<LocationViewModel> Locations { get; set; }

        public LocationContext() : base("DefaultConnection") { }
    }
}