using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui_Sample.Views;

namespace Maui_Sample.ViewModels
{
        public partial class LoginPageModel : ObservableObject
        {
                [ObservableProperty]
                [NotifyPropertyChangedFor(nameof(LoginbuttonEnable))]
                private string emailAddress;

                [ObservableProperty]
                [NotifyPropertyChangedFor(nameof(LoginbuttonEnable))]
                private string password;
                public bool LoginbuttonEnable => !(String.IsNullOrEmpty(EmailAddress) && String.IsNullOrEmpty(Password));
        }
}
