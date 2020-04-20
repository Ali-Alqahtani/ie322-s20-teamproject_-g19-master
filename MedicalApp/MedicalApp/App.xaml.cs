using Xamarin.Forms;
using MedicalApp.Views;
using MedicalApp.Database;
using System.IO;
using System;

namespace MedicalApp
{
    public partial class App : Application
    {
        private static HospitalDatabase database;

        public static HospitalDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new HospitalDatabase();
                }
                return database;
            }
        }

        public App()
        {
            Current.Properties.Clear();
            InitializeComponent();
            if (Current.Properties.ContainsKey("LoggedIn"))
            {
                MainPage = new MainPage();
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
