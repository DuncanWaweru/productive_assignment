using System;
using Microsoft.EntityFrameworkCore;
using Productive.Server.Data;
using Productive.Server.Models;
using Productive.Server.Repositories.Interfaces;

namespace Productive.Server.Repositories
{
	public class ClientCellPhoneRepository : IClientCellPhoneRepository
    {
        protected readonly ApplicationDbContext _context;
        public ClientCellPhoneRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ClientCellPhone>> GetAsync()
        {
            return await AppContext.ClientCellPhones
                .ToListAsync();
        }


        public async Task<ClientCellPhone> FindAsync(Guid id)
        {
            return await AppContext.ClientCellPhones.FindAsync(id);
        }

        public async Task<ClientCellPhone> GetAsync(Guid id)
        {
            return await AppContext.ClientCellPhones
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<ClientCellPhone> CheckByPhoneNumberAsync(string phone)
        {
            return await AppContext.ClientCellPhones
                .FirstOrDefaultAsync(a => a.CellPhone == phone);
        }


        public async Task<(bool Success, string Error)> CreateAsync(ClientCellPhone advert)
        {
            await AppContext.ClientCellPhones.AddAsync(advert);

            try
            {
                await AppContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return (false, e.ToString());
            }

            return (true, string.Empty);
        }

        public async Task<(bool Success, string Error)> CreateAsync(IEnumerable<ClientCellPhone> adverts)
        {
            await AppContext.ClientCellPhones.AddRangeAsync(adverts);

            try
            {
                await AppContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return (false, e.Message);
            }

            return (true, string.Empty);
        }

        public async Task<(bool Success, string Error)> UpdateAsync(ClientCellPhone advert)
        {
            AppContext.ClientCellPhones.Update(advert);

            try
            {
                await AppContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return (false, e.Message);
            }

            return (true, string.Empty);
        }

        public async Task<(bool Success, string Error)> UpdateAsync(IEnumerable<ClientCellPhone> adverts)
        {
            AppContext.ClientCellPhones.UpdateRange(adverts);

            try
            {
                await AppContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return (false, e.Message);
            }

            return (true, string.Empty);
        }

        public async Task<(bool Success, string Error)> DeleteAsync(ClientCellPhone advert)
        {
            AppContext.ClientCellPhones.Remove(advert);

            try
            {
                await AppContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return (false, e.Message);
            }

            return (true, string.Empty);
        }

        public async Task<(bool Success, string Error)> DeleteAsync(IEnumerable<ClientCellPhone> adverts)
        {
            AppContext.ClientCellPhones.RemoveRange(adverts);

            try
            {
                await AppContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return (false, e.Message);
            }
            return (true, string.Empty);
        }

        private ApplicationDbContext AppContext => (ApplicationDbContext)_context;
    }
}
