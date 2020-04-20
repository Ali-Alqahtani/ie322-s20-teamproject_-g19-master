namespace MedicalApp.Models
{
    public enum MenuItemType
    {
        Home,
        About,
        Logout
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
