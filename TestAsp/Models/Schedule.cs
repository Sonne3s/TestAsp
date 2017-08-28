using System;
using System.Collections.Generic;


namespace TestAsp.Models
{
    public enum DayWeek{
        Понедельник,
        Вторник,
        Среда,
        Четверг,
        Пятница,
        Суббота,
        Воскресенье
    }
    public class Schedule
    {
        public int ID { get; set; }
        public DayWeek Day { get; set; }        
        public DateTime STime { get; set; }
        public int GroupID { get; set; }

        public virtual Group Group { get;set; }
    }
}