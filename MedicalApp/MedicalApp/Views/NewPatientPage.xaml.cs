using System;
using System.ComponentModel;
using Xamarin.Forms;

using MedicalApp.Models;
using System.Linq;

namespace MedicalApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewPatientPage : ContentPage
    {
        public User User { get; set; }

        public NewPatientPage()
        {
            InitializeComponent();

            genderPicker.ItemsSource = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            nationalityPicker.ItemsSource = Enum.GetValues(typeof(Nationality)).Cast<Nationality>().ToList();

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(username.Text) && !string.IsNullOrWhiteSpace(Fname.Text) && !string.IsNullOrWhiteSpace(password.Text))
            {
                User = new User()
                {
                    FirstName = Fname.Text,
                    LastName = Lname.Text,
                    Gender = (Gender)genderPicker.SelectedItem,
                    MobileNo = Mobile.Text,
                    Nationality = (Nationality)nationalityPicker.SelectedItem,
                    BloodGroup = bloodType.SelectedItem.ToString(),
                    DateOfBirth = dateOfBirth.Date,
                    Username = username.Text,
                    Password = password.Text,
                    UserRole = UserRoles.Patient
                };

                var isUserExist = await App.Database.SaveUserAsync(User);

                if (isUserExist != 0)
                {
                    await Navigation.PopModalAsync();
                }

                await DisplayAlert("Alert!", "The username entered is not available", "Ok");
            }
            await DisplayAlert("Alert!", "Please enter the form data", "Ok");
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}