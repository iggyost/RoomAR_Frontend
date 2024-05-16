namespace Frontend_RoomAR.Views.ContentPages;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
        nameLabel.Text = App.enteredUser.Name;
        surnameLabel.Text = App.enteredUser.Surname;
        emailLabel.Text = App.enteredUser.Email;
        phoneLabel.Text = App.enteredUser.PhoneNum;
	}

    private void leaveBtn_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new AuthenticationPage();
    }

}