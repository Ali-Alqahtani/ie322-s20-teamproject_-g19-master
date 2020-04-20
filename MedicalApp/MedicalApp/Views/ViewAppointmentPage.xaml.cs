using MedicalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicalApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewAppointmentPage : ContentPage
    {
        public ViewAppointmentPage()
        {
            InitializeComponent();
        }

        private async void AppointmentView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var appointment = (Appointment)e.Item;
            appointment.Status = Status.Cancelled;

            bool answer = await DisplayAlert("Alert!", "Would you like to cancel this appointment", "Yes", "No");
            if(answer)
            {
                await App.Database.SaveAppointmentAsync(appointment);
                await DisplayAlert("Success!", "You have cancelled the appointment successfully", "Ok");
                OnAppearing();
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var userId = Convert.ToInt32(Application.Current.Properties["LoggedIn"].ToString());
            AppointmentView.ItemsSource = await App.Database.GetAppointmentAsync(userId);
        }
    }
}