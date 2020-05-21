using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XFAuthentication.Helpers;
using XFAuthentication.Models;
using XFAuthentication.Services;

namespace XFAuthentication.ViewModels
{
    public class EditIdeaViewModel
    {
        private ApiServices apiServices = new ApiServices();
        public Idea Idea { get; set; }

       public ICommand EditCommand
        {
            get
            {
                return new Command(async() => {

                    var accessToken = Settings.AccessToken;
                    var response = await apiServices.PutIdeaAsync(Idea, accessToken);

                });
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new Command(async () => {

                    var accessToken = Settings.AccessToken;
                    var response = await apiServices.DeleteIdeaAsync(Idea.Id, accessToken);

                });
            }
        }
    }
}
