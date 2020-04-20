using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MedicalApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PatientsPage : ContentPage
    {
        public PatientsPage()
        {
            InitializeComponent();
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewPatientPage() { Title = "Add New Patient" }));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            PatientsView.ItemsSource = await App.Database.GetPatientsAsync();
        }
    }
}