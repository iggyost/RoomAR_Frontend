using Frontend_RoomAR.ApplicationData;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;

namespace Frontend_RoomAR.Views.ContentPages;

public partial class FurniturePage : ContentPage
{
    public static int countFurn = 0;

    public FurniturePage()
    {
        InitializeComponent();

    }

    //private void decreaseBtn_Clicked(object sender, EventArgs e)
    //{
    //    if (int.Parse(countLabel.Text) == 0)
    //    {

    //    }
    //    else
    //    {
    //        countLabel.Text = Convert.ToString(int.Parse(countLabel.Text) - 1);
    //        countFurn = int.Parse(countLabel.Text);
    //    }
    //}

    //private void increaseBtn_Clicked(object sender, EventArgs e)
    //{
    //    countLabel.Text = Convert.ToString(int.Parse(countLabel.Text) + 1);
    //    countFurn = int.Parse(countLabel.Text);
    //}

    private async void addToCartBtn_Clicked(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();
        var responseCheck = await client.GetAsync($"{App.conString}furniturescarts/check/{App.selectedFurniture}/{App.enteredUserCartId}");
        if (responseCheck.IsSuccessStatusCode)
        {

            FurnituresCart newFurnituresCart = new FurnituresCart()
            {
                FurnitureId = App.selectedFurniture,
                CartId = App.enteredUserCartId,
                Count = countFurn,
            };
            HttpClient postClient = new HttpClient();
            var postFurniture = await postClient.PostAsJsonAsync($"{App.conString}furniturescarts/addtocart/{newFurnituresCart.FurnitureId}/{newFurnituresCart.CartId}/{newFurnituresCart.Count}", newFurnituresCart);
            if (postFurniture.IsSuccessStatusCode)
            {
                await DisplayAlert($"Успешно", "Мебель успешно добавлена в избранное", "Закрыть");
            }
            else
            {
                await DisplayAlert("Ошибка!", "Ошибка при добавлении мебели в избранное!", "Закрыть");
            }


        }
        else
        {
            if (responseCheck.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await DisplayAlert("Ошибка!", "Этот товар уже есть в избранном!", "Закрыть");
            }
            else
            {
                await DisplayAlert("Ошибка!", "Ошибка сервера. Попробуйте снова", "Закрыть");
            }
        }
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();

        HttpResponseMessage responsePhotos = await client.GetAsync($"{App.conString}furnituresphotos/" + App.selectedFurniture);

        if (responsePhotos.IsSuccessStatusCode)
        {
            string content = await responsePhotos.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Photo[]>(content);
            furnitureImagesCv.ItemsSource = data.ToList();
        }
        HttpResponseMessage responseFurnData = await client.GetAsync($"{App.conString}furnitures/selected/" + App.selectedFurniture);

        if (responseFurnData.IsSuccessStatusCode)
        {
            string content = await responseFurnData.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Furniture[]>(content);

            mainPhoto.Source = data.Select(x => x.CoverPhoto).FirstOrDefault();

            nameLbl.Text = data.Select(x => x.Name).FirstOrDefault();
            //costLbl.Text = data.Select(x => x.Cost).FirstOrDefault().ToString();
            //string costFormat = string.Format("{0:F0} руб.", costLbl.Text);
            //costLbl.Text = costFormat;
            descriptionLbl.Text = data.Select(x => x.Description).FirstOrDefault();

            lengthLbl.Text = data.Select(x => x.Length).FirstOrDefault().ToString() + "см";
            widthLbl.Text = data.Select(x => x.Width).FirstOrDefault().ToString() + "см";
            heightLbl.Text = data.Select(x => x.Height).FirstOrDefault().ToString() + "см";

            weightLbl.Text = data.Select(x => x.Weight).FirstOrDefault().ToString() + "кг";
        }
    }

    private async void backBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Home");
    }

    private async void augmentedRealityBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ModelViewPage());
    }

    private void secondaryPhoto_Clicked(object sender, EventArgs e)
    {
        Microsoft.Maui.Controls.ImageButton imageButton = sender as Microsoft.Maui.Controls.ImageButton;
        var idBtn = int.Parse(imageButton.AutomationId);
        if (idBtn != 0)
        {
            var sourceSecondary = imageButton.Source;
            mainPhoto.Source = sourceSecondary;
        }
    }
}