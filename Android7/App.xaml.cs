using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Android7
{
    public partial class App : Application
    {

        public BaseUdpReceiver udpReceiver;


        public App()
        {
            InitializeComponent();
            udpReceiver = new BaseUdpReceiver();
        }

        protected override void OnStart()
        {
            if ( MainPage  is Android7.MainPage)
            {
                Android7.MainPage2 MyMainPage = new Android7.MainPage2();
                MainPage = MyMainPage;
                MyMainPage.Start(udpReceiver);
            }
            else
            {
                Android7.MainPage MyMainPage = new Android7.MainPage();
                MainPage = MyMainPage;
                MyMainPage.Start(udpReceiver);
            }

            udpReceiver.Start(); // udpReceiver.StartDebug();
        }


        protected override void OnSleep()
        {
            udpReceiver.End();
        }


        protected override void OnResume()
        {
            OnStart();
        }


    }
}
