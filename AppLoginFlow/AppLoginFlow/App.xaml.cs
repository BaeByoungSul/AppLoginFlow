using AppLoginFlow.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppLoginFlow
{
    public partial class App : Application
    {
        public static  LoginUser LoginUserInfo { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new LoginPage() );
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
