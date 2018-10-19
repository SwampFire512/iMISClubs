using System;
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
        public ObservableCollection<RosterMember> Members { get; set; }
        public Command LoadItemsCommand { get; set; }

        public RosterMembersViewModel()
        {
            Title = "Current Roster";
            System.Console.WriteLine("contruct members viewmodel");
            Members = new ObservableCollection<RosterMember>();
            Members.Add(new RosterMember { FullName = "hi", Description = "hi", Id = Guid.Empty.ToString() });
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, RosterMember>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as RosterMember;
                Members.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            System.Console.WriteLine("load members: " + IsBusy);
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //Members.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Members.Add(item);
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