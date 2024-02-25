using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV.Interfaces
{
    public interface ISkillClient
    {
        public Task<bool> DeleteSkillAsync(int id, string itemType);
    }
}