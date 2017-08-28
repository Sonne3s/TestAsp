using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAsp.Models;

namespace TestAsp.ViewModels
{
    public class ScheduleWithGroup
    {
        public List<Group> Groups { get; set; }
        public List<Schedule> Schedules { get; set; }
        public ScheduleWithGroup(List<Group> groups, List<Schedule> schedules)
        {
            Groups = groups;
            Schedules = schedules;
        }
    }
}