namespace Frontend_RoomAR.Views.ContentPages;

public partial class ModelViewPage : ContentPage
{
	public ModelViewPage()
	{
		InitializeComponent();
		webViewModel.Source = "https://sketchfab.com/models/33a982d268d749ddb803263ea7da84b0/embed?autostart=1&internal=1&tracking=0&ui_infos=0&ui_snapshots=1&ui_stop=0&ui_watermark=0";
    }

    private async void backBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Home");
    }
}