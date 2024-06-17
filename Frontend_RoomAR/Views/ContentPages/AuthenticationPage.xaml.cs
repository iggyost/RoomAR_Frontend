using Frontend_RoomAR.ApplicationData;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Frontend_RoomAR.Views.ContentPages;

public partial class AuthenticationPage : ContentPage
{
    public AuthenticationPage()
    {
        InitializeComponent();

    }

    private void phoneEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        errorLabel.Text = string.Empty;
    }

    private async void phoneEntry_Completed(object sender, EventArgs e)
    {
        phoneEntry.IsEnabled = false;
        var regex = new Regex("^((\\+7|7|8)+([0-9]){10})$");
        if (regex.IsMatch(phoneEntry.Text))
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}users/phone/{phoneEntry.Text}");

            if (response.IsSuccessStatusCode)
            {
                App.authPhone = phoneEntry.Text;
                Application.Current.MainPage = new PasswordPage();
            }
            else
            {
                App.authPhone = phoneEntry.Text;
                Application.Current.MainPage = new RegistrationPage();
            }

        }
        else
        {
            errorLabel.Text = "Неправильный формат номера!";
        }
        phoneEntry.IsEnabled = true;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {

    }
}