using Frontend_RoomAR.ApplicationData;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Frontend_RoomAR.Views.ContentPages;

public partial class CartPage : ContentPage
{
    public static ViewUserCart[] data;
    public static int countFurniture;
    public CartPage()
    {
        InitializeComponent();

    }
    public class CounterList
    {
        public int Count { get; set; }
        public int FurnitureId { get; set; }
        public int CartId { get; set; }
    };

    private async void counterMinusBtn_Clicked(object sender, EventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        var furnitureCartId = int.Parse(btn.AutomationId);
        if (data.Where(x => x.Id == furnitureCartId).Select(s => s.Count > 1).FirstOrDefault())
        {
            var selectedFurniture = data.Where(x => x.Id == furnitureCartId).FirstOrDefault();
            selectedFurniture.Count = selectedFurniture.Count - 1;
            HttpClient client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(selectedFurniture.Count));
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await client.PutAsync($"{App.conString}furniturescarts/putcount/{selectedFurniture.Id}/{selectedFurniture.Count}", content);
            if (response.IsSuccessStatusCode)
            {
                HttpClient refrClient = new HttpClient();

                var refrResponse = await refrClient.GetAsync($"{App.conString}viewuserscarts/{App.enteredUser.UserId}");
                string refrContent = await refrResponse.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<ViewUserCart[]>(refrContent);
                if (refrResponse.IsSuccessStatusCode)
                {
                    cartCv.ItemsSource = data.ToList();
                }
                else
                {
                    await DisplayAlert("Ошибка!", "Не удалось обновить корзину", "Закрыть");
                }
            }
            else
            {
                await DisplayAlert("Ошибка!", "Не удалось обновить количество!", "Закрыть");
            }
        }
    }

    private async void counterPlusBtn_Clicked(object sender, EventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        var furnitureCartId = int.Parse(btn.AutomationId);
        var selectedFurniture = data.Where(x => x.Id == furnitureCartId).FirstOrDefault();
        selectedFurniture.Count = selectedFurniture.Count + 1;
        HttpClient client = new HttpClient();
        var content = new StringContent(JsonConvert.SerializeObject(selectedFurniture.Count));
        content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        var response = await client.PutAsync($"{App.conString}furniturescarts/putcount/{selectedFurniture.Id}/{selectedFurniture.Count}", content);
        if (response.IsSuccessStatusCode)
        {
            HttpClient refrClient = new HttpClient();

            var refrResponse = await refrClient.GetAsync($"{App.conString}viewuserscarts/{App.enteredUser.UserId}");
            string refrContent = await refrResponse.Content.ReadAsStringAsync();
            data = JsonConvert.DeserializeObject<ViewUserCart[]>(refrContent);
            if (refrResponse.IsSuccessStatusCode)
            {
                cartCv.ItemsSource = data.ToList();
            }
            else
            {
                await DisplayAlert("Ошибка!", "Не удалось обновить корзину", "Закрыть");
            }
        }
        else
        {
            await DisplayAlert("Ошибка!", "Не удалось обновить количество!", "Закрыть");
        }

    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();

        var response = await client.GetAsync($"{App.conString}viewuserscarts/{App.enteredUser.UserId}");
        string content = await response.Content.ReadAsStringAsync();
        data = JsonConvert.DeserializeObject<ViewUserCart[]>(content);
        if (response.IsSuccessStatusCode)
        {
            cartCv.ItemsSource = data.ToList();
        }
        else
        {
            await DisplayAlert("Ошибка!", "Не удалось обновить корзину", "Закрыть");
        }
    }

    private async void SwipeItem_Invoked(object sender, EventArgs e)
    {
        SwipeItem swipeItem = (SwipeItem)sender;
        var itemId = int.Parse(swipeItem.AutomationId);
        var selectedItem = data.Where(x => x.Id == itemId).FirstOrDefault();

        HttpClient client = new HttpClient();
        var response = await client.DeleteAsync($"{App.conString}furniturescarts/delete/{selectedItem.Id}");
        if (response.IsSuccessStatusCode)
        {
            HttpClient refrClient = new HttpClient();

            var refrResponse = await refrClient.GetAsync($"{App.conString}viewuserscarts/{App.enteredUser.UserId}");
            string refrContent = await refrResponse.Content.ReadAsStringAsync();
            data = JsonConvert.DeserializeObject<ViewUserCart[]>(refrContent);
            if (refrResponse.IsSuccessStatusCode)
            {
                cartCv.ItemsSource = data.ToList();
            }
            else
            {
                await DisplayAlert("Ошибка!", "Не удалось обновить корзину", "Закрыть");
            }
        }

    }

    private async void refreshCart_Refreshing(object sender, EventArgs e)
    {
        refreshCart.IsRefreshing = true;
        HttpClient client = new HttpClient();

        var response = await client.GetAsync($"{App.conString}viewuserscarts/{App.enteredUser.UserId}");
        string content = await response.Content.ReadAsStringAsync();
        data = JsonConvert.DeserializeObject<ViewUserCart[]>(content);
        if (response.IsSuccessStatusCode)
        {
            cartCv.ItemsSource = data.ToList();
            refreshCart.IsRefreshing = false;
        }
        else
        {
            await DisplayAlert("Ошибка!", "Не удалось обновить корзину", "Закрыть");
        }
    }
}