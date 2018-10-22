using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iMISClubs.Models;

namespace iMISClubs.Services
{
    class MockContactImageDataStore : IDataStore<ContactImage>
    {

        public Task<bool> AddItemAsync(ContactImage item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(ContactImage item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ContactImage> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ContactImage>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }
    }
}
