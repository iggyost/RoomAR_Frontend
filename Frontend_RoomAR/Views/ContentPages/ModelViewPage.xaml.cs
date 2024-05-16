using Frontend_RoomAR.ApplicationData;
using Newtonsoft.Json;
using System.Text.Json;

namespace Frontend_RoomAR.Views.ContentPages;

public partial class ModelViewPage : ContentPage
{
	public ModelViewPage()
	{
		InitializeComponent();
    }

    private async void backBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"{App.conString}furnituresmodel/get/{App.selectedFurniture}");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<FurnituresModel>(content);
            webViewModel.Source = data.Model;
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            await DisplayAlert("3D модель для данной мебели не найдена!", "Для данной мебели отсутствует 3D модель", "OK");
            await Navigation.PopModalAsync();
        }
    }
}