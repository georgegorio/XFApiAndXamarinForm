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
    public class AddNewIdeaViewModel
    {
        private ApiServices apiServices = new ApiServices();
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public ICommand AddCommand
        {
            get
            {
                return new Command(async() =>
                {
                    var idea = new Idea {
                        Title = Title,
                        Description = Description,
                        Category = Category
                    };
                    var response = await apiServices.PostIdeaAsync(idea, Settings.AccessToken);

                });
            }
        }
    }
}
