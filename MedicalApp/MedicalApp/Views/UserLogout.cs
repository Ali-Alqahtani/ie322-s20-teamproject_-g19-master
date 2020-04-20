using Xamarin.Forms;

namespace MedicalApp.Views
{
    public class UserLogout : ContentPage
    {
        public UserLogout()
        {
            Application.Current.Properties.Clear();
            Application.Current.MainPage = new NavigationPage(new Login());
        }
    }
}