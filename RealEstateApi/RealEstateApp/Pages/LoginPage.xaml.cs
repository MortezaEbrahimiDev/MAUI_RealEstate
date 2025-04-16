using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class LoginPage : ContentPage
{

    public LoginPage()
	{
		InitializeComponent();
       
    }

    private async void TapJoinNow_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new RegisterPage());
    }

    private async void BtnLogin_Clicked(object sender, EventArgs e)
    {
       var result=await  ApiService.Login(EntEmail.Text, EntPassword.Text);
        if (result)
        {
            //await Navigation.PushModalAsync(new HomePage());
            Application.Current.MainPage = new NavigationPage(new HomePage());
        }
        else
        {
            await DisplayAlert("", "Email or password is incorrect.", "OK");
        }
         
    }
}