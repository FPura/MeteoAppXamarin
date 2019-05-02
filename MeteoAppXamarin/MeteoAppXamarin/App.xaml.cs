using MeteoApp;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MeteoAppXamarin
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            var nav = new NavigationPage(new MeteoListPage())
            {
                BarBackgroundColor = Color.Blue,
                BarTextColor = Color.White
            };

            MainPage = nav;
            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
         //   Gelocator geo = new Gelocator();
       //     geo.GetLocation();
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

       
    }
}
