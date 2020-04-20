using SQLite;

namespace MedicalApp.Models
{
    public class Doctor
    {
        [PrimaryKey, AutoIncrement]
        public int DoctorId { get; set; }
        [Indexed]
        public int UserId { get; set; }
        public string Department { get; set; }
        public string Specialization { get; set; }
    }
}
