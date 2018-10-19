using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using iMISClubs.Models;
using iMISClubs.ViewModels;

namespace iMISClubs.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        RosterMemberDetailViewModel viewModel;

        public ItemDetailPage(RosterMemberDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new RosterMember
            {
                FullName = "Member name",
                Description = "Member nick-name"
            };

            viewModel = new RosterMemberDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}