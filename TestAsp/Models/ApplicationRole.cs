﻿using Microsoft.AspNet.Identity.EntityFramework;
namespace TestAsp.Models
{
    public class ApplicationRole:IdentityRole
    {
        public ApplicationRole() { }
        public string Description { get; set; }
    }
}