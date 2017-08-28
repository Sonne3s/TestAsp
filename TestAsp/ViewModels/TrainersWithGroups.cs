using System;
using System.Collections.Generic;
using System.Linq;
using TestAsp.Models;

namespace TestAsp.ViewModels
{
    public class TrainersWithGroups
    {
        public List<Trainer> Trainers { get; set; }
        public List<Group> Groups { get; set; }
        public TrainersWithGroups()
        {
        }
    }
}