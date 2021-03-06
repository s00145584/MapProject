﻿using System;
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
        [Display(Name = "Picture")]
        public byte[] Picture { get; set; }
        public string Description { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [Display(Name = "Location Time")]
        public float Time { get; set; }
        public string Website { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

    }

    public class LocationContext:DbContext
    {
        public DbSet<LocationViewModel> Locations { get; set; }
        public DbSet<PlannedTripsModel> Trips { get; set; }

        public LocationContext() : base("DefaultConnection") { }
    }
}