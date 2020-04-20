using MedicalApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MedicalApp.Database
{
    public class HospitalDatabase
    {
        public SQLiteAsyncConnection _database;

        public HospitalDatabase()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HospitalDatabase.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Doctor>().Wait();
            _database.CreateTableAsync<Appointment>().Wait();
        }

        // 1- User
        public async Task<int> GetUserCountAsync()
        {
            return await _database.Table<User>().CountAsync();
        }

        public async Task<User> GetUserAsync(int Id)
        {
            return await _database.Table<User>()
                            .Where(o => o.UserId == Id)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetPatientsAsync()
        {
            return await _database.Table<User>()
                            .Where(o => o.UserRole == UserRoles.Patient)
                            .ToListAsync();
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await _database.Table<User>()
                            .Where(o => o.Username == username)
                            .FirstOrDefaultAsync();
        }

        public async Task<User> Authenticated(string username, string password)
        {
            return await _database.Table<User>()
                            .Where(o => o.Username == username && o.Password == password)
                            .FirstOrDefaultAsync();
        }

        public async Task<int> SaveUserAsync(User user)
        {
            var isUsernameExist = await _database.Table<User>().Where(o => o.Username == user.Username).FirstOrDefaultAsync();

            if (isUsernameExist == null)
            {
                return await _database.InsertAsync(user);
            }

            return 0;
        }

        // 2- DOCTOR
        public async Task<List<Doctor>> GetDoctorAsync()
        {
            return await _database.Table<Doctor>().ToListAsync();
        }

        public async Task<Doctor> GetDoctorAsync(int Id)
        {
            return await _database.Table<Doctor>()
                            .Where(o => o.UserId == Id)
                            .FirstOrDefaultAsync();
        }

        public async Task<int> SaveDoctorAsync(Doctor doctor)
        {
            if (doctor.DoctorId != 0)
            {
                return await _database.UpdateAsync(doctor);
            }
            else
            {
                return await _database.InsertAsync(doctor);
            }
        }

        // 3- Appointment

        public async Task<List<Appointment>> GetAppointmentAsync(int id)
        {
            var userData = await GetUserAsync(id);
            var doctorData = await GetDoctorAsync(userData.UserId);

            if (userData.UserRole == UserRoles.Patient)
            {
                return await _database.Table<Appointment>().Where(o => o.UserId == id).ToListAsync();
            }
            else if (userData.UserRole == UserRoles.Doctor)
            {
                
                return await _database.Table<Appointment>().Where(o => o.DoctorId == doctorData.DoctorId).ToListAsync();
            }
            else
            {
                return await _database.Table<Appointment>().ToListAsync();
            }
        }

        public async Task<int> SaveAppointmentAsync(Appointment appointment)
        {
            if (appointment.AppointmentId != 0)
            {
                return await _database.UpdateAsync(appointment);
            }
            else
            {
                return await _database.InsertAsync(appointment);
            }
        }
    }
}
