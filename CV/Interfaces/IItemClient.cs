using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV.Interfaces
{
    public interface IItemClient
    {
        public Task<bool> DeleteItemAsync(int id, string itemType);
    }
}