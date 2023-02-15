using Maui_Sample.ViewModels;

namespace Maui_Sample.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		this.BindingContext = new RegisterPageModel();
	}
}