using MedicalApp.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicalApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void login_Clicked(object sender, EventArgs e)
        {
            // insert Test Data
            if (await App.Database.GetUserCountAsync() == 0)
            {
                List<User> userList = new List<User>();
                var userEr = new User()
                {
                    FirstName = "Mohammed",
                    LastName = "Ahmed",
                    BloodGroup = "B+",
                    DateOfBirth = new DateTime(1991, 1, 13, 3, 57, 32, 11),
                    Gender = Gender.Male,
                    MobileNo = "0123456789",
                    Nationality = Nationality.Saudi,
                    Username = "nu",
                    Password = "demo123",
                    UserRole = UserRoles.Doctor
                };

                var userCr = new User()
                {
                    FirstName = "Khalid",
                    LastName = "Ammar",
                    BloodGroup = "B+",
                    DateOfBirth = new DateTime(1991, 1, 13, 3, 57, 32, 11),
                    Gender = Gender.Male,
                    MobileNo = "0123456789",
                    Nationality = Nationality.Saudi,
                    Username = "cr",
                    Password = "demo123",
                    UserRole = UserRoles.Doctor
                };

                var userOb = new User()
                {
                    FirstName = "Yousif",
                    LastName = "Ali",
                    BloodGroup = "B+",
                    DateOfBirth = new DateTime(1991, 1, 13, 3, 57, 32, 11),
                    Gender = Gender.Male,
                    MobileNo = "0123456789",
                    Nationality = Nationality.Saudi,
                    Username = "ob",
                    Password = "demo123",
                    UserRole = UserRoles.Doctor
                };

                var userPa = new User()
                {
                    FirstName = "Ismaeel",
                    LastName = "Naser",
                    BloodGroup = "B+",
                    DateOfBirth = new DateTime(1991, 1, 13, 3, 57, 32, 11),
                    Gender = Gender.Male,
                    MobileNo = "0123456789",
                    Nationality = Nationality.Saudi,
                    Username = "patient",
                    Password = "demo123",
                    UserRole = UserRoles.Patient
                };

                var userRe = new User()
                {
                    FirstName = "Ali",
                    LastName = "Nizar",
                    BloodGroup = "B+",
                    DateOfBirth = new DateTime(1991, 1, 13, 3, 57, 32, 11),
                    Gender = Gender.Male,
                    MobileNo = "0123456789",
                    Nationality = Nationality.Saudi,
                    Username = "re",
                    Password = "demo123",
                    UserRole = UserRoles.Receptionist
                };

                userList.Add(userEr);
                userList.Add(userCr);
                userList.Add(userOb);
                userList.Add(userPa);
                userList.Add(userRe);


                foreach (var userData in userList)
                {
                    await App.Database.SaveUserAsync(userData);
                }

                //
                List<string> depts = new List<string>()
                {
                    {
                        "nu"
                    },
                    {
                        "cr"
                    },
                    {
                        "ob"
                    }
                };

                foreach (var dept in depts)
                {
                    switch (dept)
                    {
                        case "nu":
                            var doctorNu = await App.Database.GetUserAsync("nu");
                            await App.Database.SaveDoctorAsync(new Doctor()
                            {
                                Specialization = "Neurologist",
                                Department = "Neurology",
                                UserId = doctorNu.UserId
                            });
                            break;

                        case "cr":
                            var doctorCr = await App.Database.GetUserAsync("cr");
                            await App.Database.SaveDoctorAsync(new Doctor()
                            {
                                Specialization = "Cardiologist",
                                Department = "Cardiology",
                                UserId = doctorCr.UserId
                            });
                            break;

                        case "ob":
                            var doctorOb = await App.Database.GetUserAsync("ob");
                            await App.Database.SaveDoctorAsync(new Doctor()
                            {
                                Specialization = "Ophthalmologist",
                                Department = "Ophthalmology",
                                UserId = doctorOb.UserId
                            });
                            break;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(username.Text) && !string.IsNullOrWhiteSpace(password.Text))
            {
                var result = await App.Database.Authenticated(username.Text, password.Text);
                if (result != null)
                {
                    Application.Current.Properties["LoggedIn"] = result.UserId;
                    // Go to Mainpage
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    await DisplayAlert("Alert", "Invalid username or password!", "OK");
                }
            }
            else
            {
                await DisplayAlert("Alert", "username and password cannot be null", "OK");
            }
        }
    }
}