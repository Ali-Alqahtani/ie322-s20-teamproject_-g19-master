using SQLite;
using System;

namespace MedicalApp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; private set; }
        public Nationality Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BloodGroup { get; set; }
        public string UserRole { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User()
        {
            var Today = DateTime.Today;
            var age = Today.Year - DateOfBirth.Year;
            Age = age;
        }

        public string ListDisplayData
        { 
            get
            {
                if (UserRole == UserRoles.Doctor)
                {
                    return "Dr. " + FirstName + " " + LastName + " | ID:" + UserId;
                }
                else
                {
                    return  FirstName + " " + LastName + " | ID:" + UserId;
                }
            }
        }
    }
}
