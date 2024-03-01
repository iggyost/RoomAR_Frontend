using Frontend_RoomAR.ApplicationData;
using Frontend_RoomAR.Views.ContentPages;

namespace Frontend_RoomAR
{
    public partial class App : Application
    {
        public static int selectedFurniture;
        public static User enteredUser;
        public static int enteredUserCartId;
        public static FurnituresCart userCartList;
        public static string authPhone;
        public static string conString = "http://192.168.0.10:45456/api/";
        public App()
        {
            InitializeComponent();

            MainPage = new AuthenticationPage();

//            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
//            {
//#if ANDROID
//                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
//#endif
//            });
        }
    }
}