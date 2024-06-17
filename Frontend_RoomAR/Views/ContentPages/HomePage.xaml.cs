using Frontend_RoomAR.ApplicationData;
using Newtonsoft.Json;
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
    public static List<Furniture> furnituresList = new List<Furniture>();
    public static int currentCategoryId = 0;
    private async void LoadCategoriesData()
    {
        try
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}categories/get");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Category[]>(content);
                categoriesCv.ItemsSource = data.ToList();
            }
        }
        catch (Exception)
        {

        }
    }
    public async Task GetAllFurnitures()
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"{App.conString}furnitures/get/all");

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            furnituresList = JsonConvert.DeserializeObject<Furniture[]>(content).ToList();
        }
    }
    private void categoriesCv_Loaded(object sender, EventArgs e)
    {

    }

    private async void categoryRb_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        categoriesCv.IsEnabled = false;
        try
        {
            RadioButton radioButton = sender as RadioButton;
            var btnId = int.Parse(radioButton.AutomationId);
            currentCategoryId = btnId;
            //HttpClient client = new HttpClient();
            //HttpResponseMessage response = await client.GetAsync($"{App.conString}furnitures/{btnId}");

            //if (response.IsSuccessStatusCode)
            //{
            //    string content = await response.Content.ReadAsStringAsync();
            //    var data = JsonConvert.DeserializeObject<Furniture[]>(content);
            //    furnituresCv.ItemsSource = data.ToList();

            //}
            furnituresCv.ItemsSource = furnituresList.Where(x => x.CategoryId == btnId).ToList();
        }
        catch (Exception)
        {

        }
        await Task.Delay(400);
        categoriesCv.IsEnabled = true;
    }

    private void furnituresCv_Loaded(object sender, EventArgs e)
    {

    }

    private void searchField_Completed(object sender, EventArgs e)
    {
        try
        {
            if (searchField.Text.Length > 0)
            {
                var currentFurnituresCategoryList = furnituresList.Where(x => x.CategoryId == currentCategoryId).ToList();
                furnituresCv.ItemsSource = currentFurnituresCategoryList.Where(x => x.Name.Contains(searchField.Text)).ToList();
            }
            else
            {
                furnituresCv.ItemsSource = furnituresList.Where(x => x.CategoryId == currentCategoryId).ToList();
            }
        }
        catch (Exception)
        {

        }
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

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        LoadCategoriesData();
        await GetAllFurnitures();
        HttpClient clientCart = new HttpClient();
        var responseCart = await clientCart.GetAsync($"{App.conString}carts/get/{App.enteredUser.UserId}");
        if (responseCart.IsSuccessStatusCode)
        {
            string contentCart = await responseCart.Content.ReadAsStringAsync();
            var dataCart = JsonConvert.DeserializeObject<int>(contentCart);
            App.enteredUserCartId = dataCart;
        }
        else
        {
            await DisplayAlert("Ошибка!", "Корзина пользователя не найдена! Обратитесь к системному администратору или зарегистрируйте новый аккаунт", "Закрыть");
        }
    }
}