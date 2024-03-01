using Frontend_RoomAR.Views.ContentPages;

namespace Frontend_RoomAR
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Auth", typeof(AuthenticationPage));
            Routing.RegisterRoute("Home", typeof(HomePage));
            Routing.RegisterRoute("Furniture", typeof(FurniturePage));
        }
    }
}