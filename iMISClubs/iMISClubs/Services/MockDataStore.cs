using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                new RosterMember { Id = "25255", FullName = "Dylan Cervantes", Description="Dylan", InstituteType = "School", InstituteTypeName = "Copper Heights Elementary", Status = CheckInStatus.New, MemberAlerts = new Collection<MemberAlert>
                {
                    new MemberAlert{Id = Guid.NewGuid().ToString(), Description = "Alergic to peanuts", Type = MemberAlertType.Urgent},
                    new MemberAlert{Id = Guid.NewGuid().ToString(), Description = "Dylan does not get along with Trey", Type = MemberAlertType.Warning}
                }},
                new RosterMember { Id = "70909", FullName = "Adrienne Cole", Description="Adrienne", InstituteType = "School", InstituteTypeName = "Copper Heights Elementary", Status = CheckInStatus.New , MemberAlerts = new Collection<MemberAlert>
                {
                    new MemberAlert{Id = Guid.NewGuid().ToString(), Description = "Check parent's Id", Type = MemberAlertType.Urgent},
                    new MemberAlert{Id = Guid.NewGuid().ToString(), Description = "Biter", Type = MemberAlertType.Warning}
                }},
                new RosterMember { Id = "48642", FullName = "Kylie Ford", Description="Kylie", InstituteType = "School", InstituteTypeName = "Copper Heights Elementary", Status = CheckInStatus.New , MemberAlerts = new Collection<MemberAlert>
                {
                    new MemberAlert{Id = Guid.NewGuid().ToString(), Description = "Allowed to leave on his own", Type = MemberAlertType.Info}
                }},
                new RosterMember { Id = "46868", FullName = "Fausto Guzman", Description="Fausto", InstituteType = "School", InstituteTypeName = "Copper Heights Elementary", Status = CheckInStatus.New , MemberAlerts = new Collection<MemberAlert>
                {
                    new MemberAlert{Id = Guid.NewGuid().ToString(), Description = "Alergic to peanuts", Type = MemberAlertType.Urgent},
                    new MemberAlert{Id = Guid.NewGuid().ToString(), Description = "Alergic to dairy", Type = MemberAlertType.Urgent},
                    new MemberAlert{Id = Guid.NewGuid().ToString(), Description = "Alergic to gluten", Type = MemberAlertType.Urgent},
                    new MemberAlert{Id = Guid.NewGuid().ToString(), Description = "Carries EPI pen in first backpack pocket", Type = MemberAlertType.Info}
                }},
                new RosterMember { Id = "62738", FullName = "Trey McCarroll", Description="Trey", InstituteType = "School", InstituteTypeName = "Copper Heights Elementary", Status = CheckInStatus.New , MemberAlerts = new Collection<MemberAlert>
                {
                    new MemberAlert{Id = Guid.NewGuid().ToString(), Description = "Trey does not get along with Dylan", Type = MemberAlertType.Warning}
                }},
                new RosterMember { Id = "82090", FullName = "D'Shawna Tovares", Description="D'Shawna", InstituteType = "School", InstituteTypeName = "Copper Heights Elementary", Status = CheckInStatus.New },
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