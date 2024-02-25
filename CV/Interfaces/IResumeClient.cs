using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;

namespace CV.Interfaces
{
    public interface IResumeClient
    {
        Task<(Resume? Resume, string? ErrorMessage)> GetResumeAsync();
        Task<string?> SaveResumeAsync(Resume resume);
        Task<string?> SaveResumeAsHTMLAsync(string html);
        Task<string?> UpsertResumeAsync(Resume resume);
        Task<string?> DeleteResumeAsync(int id);

    }
}