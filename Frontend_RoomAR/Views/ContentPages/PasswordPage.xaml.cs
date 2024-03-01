using Frontend_RoomAR.ApplicationData;
using Newtonsoft.Json;
using System.Text.Json;

namespace Frontend_RoomAR.Views.ContentPages;

public partial class PasswordPage : ContentPage
{
	public PasswordPage()
	{
		InitializeComponent();
	}

    private async void passwordEntry_Completed(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();

        HttpResponseMessage response = await client.GetAsync($"{App.conString}users/login/{App.authPhone}/{passwordEntry.Text}");

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<User>(content);
            App.enteredUser = data;
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            errorLabel.Text = "Неверный пароль!";
        }
    }

    private void passwordEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        errorLabel.Text = string.Empty;
    }
}