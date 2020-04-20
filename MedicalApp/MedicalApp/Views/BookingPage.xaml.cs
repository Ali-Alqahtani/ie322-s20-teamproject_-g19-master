using MedicalApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicalApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingPage : ContentPage
    {
        int _DoctorId;
        public BookingPage(int DoctorId)
        {
            InitializeComponent();
            _DoctorId = DoctorId;
        }

        private async void SaveBooking_Clicked(object sender, EventArgs e)
        {
            var date = bookingDate.Date.ToString("dd/MM/yyyy");
            var time = bookingTime.Time.ToString();
            var DateAndTime = date + " " + time;

            var appointment = new Appointment()
            {
                AppointmentDate = DateTime.ParseExact(DateAndTime, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture),
                DoctorId = _DoctorId,
                UserId = Convert.ToInt32(Application.Current.Properties["LoggedIn"].ToString()),
                Status = Status.New
            };

            await App.Database.SaveAppointmentAsync(appointment);

            await DisplayAlert("Success", "You have successfully created appointment", "OK");

            Application.Current.MainPage = new MainPage();
        }
    }
}