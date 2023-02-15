using Maui_Sample.Views;

namespace Maui_Sample;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		
		var nav = new NavigationPage(new LoginPage());
		nav.BarBackgroundColor = Colors.White;
		nav.BackgroundColor = Colors.White;
		nav.BarTextColor = Colors.Black;
		MainPage = nav;
        }
}
