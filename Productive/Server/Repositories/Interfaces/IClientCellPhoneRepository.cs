using System;
using Productive.Server.Models;
namespace Productive.Server.Repositories.Interfaces
{
	public interface IClientCellPhoneRepository
	{
        Task<IEnumerable<ClientCellPhone>> GetAsync();
        Task<ClientCellPhone> FindAsync(Guid id);
        Task<ClientCellPhone> GetAsync(Guid id);
        Task<ClientCellPhone> CheckByPhoneNumberAsync(string phone);
        Task<(bool Success, string Error)> CreateAsync(ClientCellPhone advert);
        Task<(bool Success, string Error)> CreateAsync(IEnumerable<ClientCellPhone> adverts);
        Task<(bool Success, string Error)> UpdateAsync(ClientCellPhone advert);
        Task<(bool Success, string Error)> UpdateAsync(IEnumerable<ClientCellPhone> adverts);
        Task<(bool Success, string Error)> DeleteAsync(ClientCellPhone advert);
        Task<(bool Success, string Error)> DeleteAsync(IEnumerable<ClientCellPhone> adverts);
    }
}

