using MedicalApp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicalApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Homepage : ContentPage
    {
        public Homepage()
        {
            InitializeComponent();
        }

        private async void Doctor_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new DoctorsPage() { Title = "" });
        }

        private async void Patients_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new PatientsPage() { Title = "" });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var userId = Convert.ToInt32(Application.Current.Properties["LoggedIn"].ToString());

            var userData = await App.Database.GetUserAsync(userId);

            if(userData.UserRole == UserRoles.Patient)
            {
                staffOnly.IsVisible = false;
            }
        }

        private async void ViewAppointment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewAppointmentPage() { Title = "View Appointments" });
        }
    }
}