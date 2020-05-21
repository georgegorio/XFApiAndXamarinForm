using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XFAuthentication.Helpers;
using XFAuthentication.Services;

namespace XFAuthentication.ViewModels
{
    public class LoginViewModel
    {
        private ApiServices apiServices = new ApiServices();
        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async() =>
                {
                   var accesstoken = await apiServices.LoginAsync(Username, Password);

                    Settings.AccessToken = accesstoken;
                });
            }

        }

        public LoginViewModel()
        {
            Username = Settings.Username;
            Password = Settings.Password;
        }
    }
}
