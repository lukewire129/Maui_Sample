using Maui_Sample.ViewModels;

namespace Maui_Sample.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		this.BindingContext = new LoginPageModel();
	}

        private void RegisterCommand(object sender, EventArgs e)
        {
                Application.Current.MainPage.Navigation.PushAsync(new RegisterPage(),true);
        }
} 