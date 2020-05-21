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
	public partial class EditIdeaPage : ContentPage
	{
		public EditIdeaPage (Idea idea)
		{
            var editIdeaViewModel = new EditIdeaViewModel();
            editIdeaViewModel.Idea = idea;
            BindingContext = editIdeaViewModel;

            InitializeComponent();

            //var editIdeaViewModel = BindingContext as EditIdeaViewModel;
            //editIdeaViewModel.Idea = idea;
        }
	}
}