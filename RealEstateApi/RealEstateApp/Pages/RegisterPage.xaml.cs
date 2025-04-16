using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    private async void BtnRegister_Clicked(object sender, EventArgs e)
    {

		var result=await ApiService
			.RegisterUser(EntFullName.Text, EntEmail.Text, EntPhone.Text, EntPassword.Text);

		if(!result)
		{
			await DisplayAlert("", "Oops somthings went wrong", "Cancel");
			return;	
		}

		await DisplayAlert("", "Your account has been created", "Alright");
		await Navigation.PushModalAsync(new LoginPage());
    }

    private async void TapLogin_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new LoginPage());
    }
}