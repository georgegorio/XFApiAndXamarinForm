using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFAuthentication.Helpers;
using XFAuthentication.ViewModels;
using XFAuthentication.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XFAuthentication
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            //MainPage = new NavigationPage(new RegisterPage());
            SetMainPage();
        }

        private void SetMainPage()
        {
            if(!string.IsNullOrEmpty(Settings.AccessToken))
            {
                if(DateTime.UtcNow.AddHours(1) > Settings.AccessTokenExpiration)
                {
                    var vm = new LoginViewModel();
                    vm.Username = Settings.Username;
                    vm.Password = Settings.Password;
                    vm.LoginCommand.Execute(null);
                }
                MainPage = new NavigationPage(new IdeasPage());
            }
            else if(!string.IsNullOrEmpty(Settings.Username) && !string.IsNullOrEmpty(Settings.Password))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new RegisterPage());
            }
        }

        protected override void OnStart()
        {
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
