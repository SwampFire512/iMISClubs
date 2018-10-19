using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using iMISClubs.Models;
using iMISClubs.Views;
using iMISClubs.ViewModels;

namespace iMISClubs.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RosterMembersPage : ContentPage
    {
        RosterMembersViewModel viewModel;

        public RosterMembersPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new RosterMembersViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as RosterMember;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new RosterMemberDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Members.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}