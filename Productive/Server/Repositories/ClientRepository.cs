using System;
using Microsoft.EntityFrameworkCore;
using Productive.Server.Data;
using Productive.Server.Repositories.Interfaces;

namespace Productive.Server.Repositories
{
	public class ClientRepository: IClientRepository
    {
        protected readonly ApplicationDbContext _context;
        public ClientRepository(ApplicationDbContext context) 
        {
            _context = context;

        }
        public async Task<IEnumerable<Models.Client>> GetAsync()
        {
            return await AppContext.Clients.Include(x => x.ClientCellPhone)
                .ToListAsync();
        }

        
        public async Task<Models.Client> FindAsync(Guid id)
        {
            return await AppContext.Clients.FindAsync(id);
        }

        public async Task<Models.Client> GetAsync(Guid id)
        {
            return await AppContext.Clients.Include(x=>x.ClientCellPhone)
                .FirstOrDefaultAsync(a => a.Id == id);
        }


        public async Task<(bool Success, string Error)> CreateAsync(Models.Client advert)
        {
            await AppContext.Clients.AddAsync(advert);

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

        public async Task<(bool Success, string Error)> CreateAsync(IEnumerable<Models.Client> adverts)
        {
            await AppContext.Clients.AddRangeAsync(adverts);

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

        public async Task<(bool Success, string Error)> UpdateAsync(Models.Client advert)
        {
            AppContext.Clients.Update(advert);

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

        public async Task<(bool Success, string Error)> UpdateAsync(IEnumerable<Models.Client> adverts)
        {
            AppContext.Clients.UpdateRange(adverts);

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

        public async Task<(bool Success, string Error)> DeleteAsync(Models.Client advert)
        {
            AppContext.Clients.Remove(advert);

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

        public async Task<(bool Success, string Error)> DeleteAsync(IEnumerable<Models.Client> adverts)
        {
            AppContext.Clients.RemoveRange(adverts);

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

        public async Task<bool> CheckByEmail(string email)
        {
           var clients = await AppContext.Clients.Where(x=>x.Email==email).FirstOrDefaultAsync();
            if (clients == null)
            {
                return false;
            } else
                return true;
        }

        public async  Task<bool> RemoveRelationShipAsync(Guid clientId, Guid phoneNumber)
        {
            try
            {

                var existingClient = await AppContext.Clients.FindAsync(clientId);
                AppContext.Entry(existingClient).State = EntityState.Modified;

                AppContext.Clients.Remove(existingClient);
                existingClient.ClientCellPhoneId = phoneNumber;
                AppContext.Clients.Add(existingClient);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private ApplicationDbContext AppContext => (ApplicationDbContext) _context;
    }
}
