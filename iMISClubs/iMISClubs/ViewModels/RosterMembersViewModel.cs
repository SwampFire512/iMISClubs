﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using iMISClubs.Models;
using iMISClubs.Views;

namespace iMISClubs.ViewModels
{
    public class RosterMembersViewModel : BaseViewModel
    {
        public ObservableCollection<RosterMemberViewModel> Members { get; set; }
        public Command LoadItemsCommand { get; set; }

        public RosterMembersViewModel()
        {
            Title = "Current Roster";
            Members = new ObservableCollection<RosterMemberViewModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, RosterMember>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as RosterMember;
                Members.Add(new RosterMemberViewModel(newItem));
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Members.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Members.Add(new RosterMemberViewModel(item));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}