using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MeteoAppXamarin
{
    public partial class App : Application
    {
        static Database db;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            Gelocator geo = new Gelocator();
            geo.GetLocation();
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

        public static Database Database
        {
            get
            {
                if (db == null) // se l'istanza è nulla, la creo
                    db = new Database();
                return db; // ritorno l'istanza
            }
        }
    }
}
