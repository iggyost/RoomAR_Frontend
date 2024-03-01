using Frontend_RoomAR.ApplicationData;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Frontend_RoomAR.Views.ContentPages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }
    private async void LoadData()
    {
        try
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}categories");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<Category[]>(content);
                categoriesCv.ItemsSource = data.ToList();
            }
        }
        catch (Exception)
        {

        }
    }
    private void categoriesCv_Loaded(object sender, EventArgs e)
    {
        LoadData();
    }

    private async void categoryRb_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            RadioButton radioButton = sender as RadioButton;
            var btnId = int.Parse(radioButton.AutomationId);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}furnitures/" + btnId);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<Furniture[]>(content);
                furnituresCv.ItemsSource = data.ToList();

                HttpClient clientCart = new HttpClient();
                var responseCart = await clientCart.GetAsync($"{App.conString}carts/get/{App.enteredUser.UserId}");
                if (responseCart.IsSuccessStatusCode)
                {
                    string contentCart = await responseCart.Content.ReadAsStringAsync();
                    var dataCart = JsonSerializer.Deserialize<int>(contentCart);
                    App.enteredUserCartId = dataCart;
                }
                else
                {
                    await DisplayAlert("Ошибка!", "Корзина пользователя не найдена! Обратитесь к системному администратору или зарегистрируйте новый аккаунт", "Закрыть");
                }
            }
        }
        catch (Exception)
        {

        }
    }

    private void furnituresCv_Loaded(object sender, EventArgs e)
    {

    }

    private void searchField_Completed(object sender, EventArgs e)
    {

    }

    private async void FurnitureItemGesture_Tapped(object sender, EventArgs e)
    {
        Border border = sender as Border;
        var idBorder = Convert.ToInt32(border.AutomationId);
        if (idBorder != 0)
        {
            App.selectedFurniture = idBorder;
            await Navigation.PushModalAsync(new FurniturePage());
        }
        else
        {
            await DisplayAlert($"Ошибка", "Мебель не найдена!", "Закрыть");

        }
    }
}