using System;
using Productive.Server.Models;

namespace Productive.Server.Repositories.Interfaces
{
	public interface IClientRepository
	{
        Task<IEnumerable<Models.Client>> GetAsync();
        Task<Models.Client> FindAsync(Guid id);
        Task<Models.Client> GetAsync(Guid id);
        Task<bool> CheckByEmail(string email);
        Task<(bool Success, string Error)> CreateAsync(Models.Client advert);
        Task<(bool Success, string Error)> CreateAsync(IEnumerable<Models.Client> adverts);
        Task<(bool Success, string Error)> UpdateAsync(Models.Client advert);
        Task<(bool Success, string Error)> UpdateAsync(IEnumerable<Models.Client> adverts);
        Task<(bool Success, string Error)> DeleteAsync(Models.Client advert);
        Task<(bool Success, string Error)> DeleteAsync(IEnumerable<Models.Client> adverts);
        Task<bool> RemoveRelationShipAsync(Guid clientId, Guid phoneNumber);
    }
}

