namespace Frontend_RoomAR.Views.ContentPages;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    private void leaveBtn_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new AuthenticationPage();
    }

}