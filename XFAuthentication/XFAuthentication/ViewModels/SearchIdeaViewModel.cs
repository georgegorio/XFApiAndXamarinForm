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
    public class SearchIdeaViewModel: INotifyPropertyChanged
    {
        private ApiServices apiServices = new ApiServices();
        private List<Idea> _ideas { get; set; }
        public string Keyword { get; set; }

        public List<Idea> Ideas
        {
            get { return _ideas; }
            set {
                _ideas = value;
                OnPropertyChanged();
                }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new Command(async() =>
                {
                    var accessToken = Settings.AccessToken;
                    Ideas = await apiServices.SearchIdeasAsync(Keyword, accessToken);
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
