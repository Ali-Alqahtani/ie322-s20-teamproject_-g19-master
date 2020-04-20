using SQLite;
using System;

namespace MedicalApp.Models
{
    public class Appointment
    {
        [PrimaryKey, AutoIncrement]
        public int AppointmentId { get; set; }
        [Indexed]
        public int UserId { get; set; }
        [Indexed]
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Status Status { get; set; }

        public string AppointemtViw 
        { 
            get
            {
                return "Your Appointment On: " + AppointmentDate.DayOfWeek + " | " + AppointmentDate + " | Status: " + Enum.GetName(typeof(Status), Status);
            }
        }
    }
}
