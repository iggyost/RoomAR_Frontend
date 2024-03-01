using Android.App;
using Android.Runtime;

namespace Frontend_RoomAR
{
#if DEBUG                                   // connect to local service on the
    [Application(UsesCleartextTraffic = true)]  // emulator's host for debugging,
#else
    [Application]
#endif
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}