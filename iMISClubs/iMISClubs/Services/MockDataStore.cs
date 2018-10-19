using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iMISClubs.Models;

namespace iMISClubs.Services
{
    public class MockDataStore : IDataStore<RosterMember>
    {
        List<RosterMember> items;

        public MockDataStore()
        {
            items = new List<RosterMember>();
            var mockItems = new List<RosterMember>
            {
                new RosterMember { Id = "25255", FullName = "Dylan Cervantes", Description="Dylan" },
                new RosterMember { Id = "70909", FullName = "Adrienne Cole", Description="Adrienne" },
                new RosterMember { Id = "48642", FullName = "Kylie Ford", Description="Kylie" },
                new RosterMember { Id = "46868", FullName = "Fausto Guzman", Description="Fausto" },
                new RosterMember { Id = "62738", FullName = "Trey McCarroll", Description="Trey" },
                new RosterMember { Id = "82090", FullName = "D'Shawna Tovares", Description="D'Shawna" },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(RosterMember item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(RosterMember item)
        {
            var oldItem = items.Where((RosterMember arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((RosterMember arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<RosterMember> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<RosterMember>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}