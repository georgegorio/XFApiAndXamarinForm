using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XFAuthentication.Helpers;
using XFAuthentication.Models;
using XFAuthentication.Services;

namespace XFAuthentication.ViewModels
{
    public class IdeasViewModel : INotifyPropertyChanged
    {
        private ApiServices apiServices = new ApiServices();
        private List<Idea> _ideas;

        //public string AccessToken { get; set; }

        public List<Idea> Ideas
        {
            get { return _ideas; }
            set { _ideas = value; OnPropertyChanged(); }
        }
        public ICommand GetIdeaCommand
        {
            get
            {
                return new Command(async () =>
               {
                   var accesstoken = Settings.AccessToken;
                   Ideas = await apiServices.GetIdeasAsync(accesstoken);
               });
            }

        }

        public ICommand LogoutCommand
        {
            get
            {
                return new Command(() =>
                {                    
                    Settings.AccessToken = "";
                    Settings.Username = "";
                    Settings.Password = "";

                });
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
