using Frontend_RoomAR.ApplicationData;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Frontend_RoomAR.Views.ContentPages;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage()
    {
        InitializeComponent();
        phoneEntry.Text = App.authPhone;
    }
    Regex nameRegex = new Regex(@"\p{IsCyrillic}+");
    Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
    Regex phoneRegex = new Regex("^((\\+7|7|8)+([0-9]){10})$");

    private async void regBtn_Clicked(object sender, EventArgs e)
    {
        if (passwordCompleteEntry.Text == passwordEntry.Text &&
            errorEmailLabel.Text == string.Empty &&
            errorNameLabel.Text == string.Empty &&
            errorPasswordLabel.Text == string.Empty &&
            errorPhoneLabel.Text == string.Empty &&
            errorSurnameLabel.Text == string.Empty)
        {
            if (passwordEntry.Text.Length > 3 && passwordEntry.Text.Length < 21)
            {
                string name = nameEntry.Text;
                string surname = surnameEntry.Text;
                string phone_num = phoneEntry.Text;
                string email = emailEntry.Text;
                string password = passwordEntry.Text;
                User newUser = new User()
                {
                    Name = name,
                    Surname = surname,
                    PhoneNum = phone_num,
                    Email = email,
                    Password = password
                };
                HttpClient client = new HttpClient();
                var response = await client.PostAsJsonAsync($"{App.conString}users/reg", newUser);
                if (response.IsSuccessStatusCode)
                {
                    HttpClient getClient = new HttpClient();
                    var getUser = await getClient.GetAsync($"{App.conString}users/login/{newUser.PhoneNum}/{newUser.Password}");
                    string content = await getUser.Content.ReadAsStringAsync();
                    if (getUser.IsSuccessStatusCode)
                    {
                        var dataUser = JsonSerializer.Deserialize<User>(content);
                        App.enteredUser = dataUser;
                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        await DisplayAlert("Ошибка!", "Ошибка при переходе на новую страницу, пользователь был зарегистрирован", "Закрыть");
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    await DisplayAlert("Ошибка!", "Пользователь с таким номером телефона уже существует!", "Закрыть");
                }
                else
                {
                    await DisplayAlert("Ошибка!", "Ошибка при регистрации пользователя! Проверьте данные и повторите попытку!", "Закрыть");
                }
            }
            else
            {
                await DisplayAlert("Ошибка!", "Длина пароля не может быть менее 4 символов и не более 21", "Закрыть");
            }
        }
        else
        {
            await DisplayAlert("Ошибка!", "Проверьте правильность заполнения полей!", "Закрыть");
        }
    }
    private void phoneEntry_Completed(object sender, EventArgs e)
    {
        nameEntry.Focus();
    }

    private void phoneEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (phoneRegex.IsMatch(phoneEntry.Text))
        {
            errorPhoneLabel.Text = string.Empty;
        }
        else
        {
            errorPhoneLabel.Text = "Неправильный формат телефона!";
        }
    }


    private void nameEntry_Completed(object sender, EventArgs e)
    {
        surnameEntry.Focus();
    }
    private void nameEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (nameRegex.IsMatch(nameEntry.Text))
        {
            errorNameLabel.Text = string.Empty;
        }
        else
        {
            errorNameLabel.Text = "Только кириллица!";
        }
    }


    private void surnameEntry_Completed(object sender, EventArgs e)
    {
        emailEntry.Focus();
    }
    private void surnameEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (nameRegex.IsMatch(surnameEntry.Text))
        {
            errorSurnameLabel.Text = string.Empty;
        }
        else
        {
            errorSurnameLabel.Text = "Только кириллица!";
        }
    }


    private void emailEntry_Completed(object sender, EventArgs e)
    {
        passwordEntry.Focus();
    }
    private void emailEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (emailRegex.IsMatch(emailEntry.Text))
        {
            errorEmailLabel.Text = string.Empty;
        }
        else
        {
            errorEmailLabel.Text = "Почта не соответствует формату!";
        }
    }


    private void passwordEntry_Completed(object sender, EventArgs e)
    {
        passwordCompleteEntry.Focus();
    }
    private void passwordEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (passwordCompleteEntry.Text != passwordEntry.Text)
        {
            errorPasswordLabel.Text = "Пароли не совпадают!";
        }
        else
        {
            errorPasswordLabel.Text = string.Empty;
        }
    }


    private async void passwordCompleteEntry_Completed(object sender, EventArgs e)
    {
        if (passwordCompleteEntry.Text == passwordEntry.Text &&
            errorEmailLabel.Text == string.Empty &&
            errorNameLabel.Text == string.Empty &&
            errorPasswordLabel.Text == string.Empty &&
            errorPhoneLabel.Text == string.Empty &&
            errorSurnameLabel.Text == string.Empty)
        {
            if (passwordEntry.Text.Length > 3 && passwordEntry.Text.Length < 21)
            {
                User newUser = new User()
                {
                    Name = nameEntry.Text,
                    Surname = surnameEntry.Text,
                    PhoneNum = phoneEntry.Text,
                    Email = emailEntry.Text,
                    Password = passwordEntry.Text,
                };
                var json = JsonSerializer.Serialize(newUser);
                var data = new StringContent(json, Encoding.UTF8);
                HttpClient client = new HttpClient();
                var response = await client.PostAsJsonAsync($"{App.conString}users/reg", newUser);
                if (response.IsSuccessStatusCode)
                {
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("Ошибка!", "Ошибка при регистрации пользователя! Проверьте данные и повторите попытку!", "Закрыть");
                }
            }
        }
        else
        {
            await DisplayAlert("Ошибка!", "Проверьте правильность заполнения полей!", "Закрыть");
        }

    }
    private void passwordCompleteEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (passwordCompleteEntry.Text != passwordEntry.Text)
        {
            errorPasswordLabel.Text = "Пароли не совпадают!";
        }
        else
        {
            errorPasswordLabel.Text = string.Empty;
        }
    }

}