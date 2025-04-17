using RealEstateApp.Models;
using RealEstateApp.Pages;

namespace RealEstateApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var token = Preferences.Get(nameof(Token.AccessToken), string.Empty);
            if (string.IsNullOrEmpty(token))
            {
                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new CustomTabbedPage();
            }
           
        }
    }
}
