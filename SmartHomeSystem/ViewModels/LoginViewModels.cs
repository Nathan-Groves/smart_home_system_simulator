// ViewModels/LoginViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace SmartHomeSystem.ViewModels;
public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanLogin))]
    private string _email = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanLogin))]
    private string _password = string.Empty;

    [ObservableProperty]
    private bool _isBusy;

    public bool CanLogin => !string.IsNullOrWhiteSpace(Email) && 
                          !string.IsNullOrWhiteSpace(Password);

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task Login()
    {
        if (IsBusy) return;
        
        try
        {
            IsBusy = true;
            
            // Replace with your actual authentication logic
            bool isAuthenticated = await AuthService.Login(Email, Password);
            
            if (isAuthenticated)
                await Shell.Current.GoToAsync("//main");
            else
                await Shell.Current.DisplayAlert("Error", "Invalid login", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task NavigateToRegister()
    {
        await Shell.Current.GoToAsync("register");
    }

    [RelayCommand]
    private async Task ForgotPassword()
    {
        await Shell.Current.DisplayAlert("Reset Password", 
            "Password reset link will be sent to your email.", "OK");
    }
}