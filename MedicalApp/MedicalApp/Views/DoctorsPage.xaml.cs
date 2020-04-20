using MedicalApp.Database;
using MedicalApp.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicalApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorsPage : ContentPage
    {
        public DoctorsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var doctorsList = await App.Database.GetDoctorAsync();
            // Temporary List
            List<User> temp = new List<User>();
            foreach (var doctor in doctorsList)
            {
                var doctorData = await App.Database.GetUserAsync(doctor.UserId);
                temp.Add(doctorData);
            }
            DoctorsView.ItemsSource = temp;
        }

        private async void DoctorsView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var userData = (User)e.Item;
            var doctorData = await App.Database.GetDoctorAsync(userData.UserId);
            var modalPage = new BookingPage(doctorData.DoctorId);
            await Navigation.PushModalAsync(modalPage);
        }
    }
}