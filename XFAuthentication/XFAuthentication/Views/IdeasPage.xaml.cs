using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFAuthentication.Models;
using XFAuthentication.ViewModels;

namespace XFAuthentication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IdeasPage : ContentPage
	{
		public IdeasPage ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddIdeaPage());
        }

        private async void IdeaListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var idea = e.Item as Idea;
            await Navigation.PushAsync(new EditIdeaPage(idea));
        }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchIdeaPage());
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}