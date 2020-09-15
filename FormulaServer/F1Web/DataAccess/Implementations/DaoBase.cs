using Microsoft.EntityFrameworkCore;
using F1.Data.Interfaces;
using F1Web.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace F1Web.DataAccess.Implementations
{
    /// <summary>
    /// An abstract class representing a generic parent to data access.
    /// </summary>
    /// <typeparam name="TDbCtx">Teh datacontext used.</typeparam>
    /// <typeparam name="T">The type of the data entity.</typeparam>
    public abstract class DaoBase<TDbCtx, T> : IRepository<T>, IDisposable
        where TDbCtx : DbContext
        where T : class, IDbEntry
    {
        private TDbCtx _context;

        /// <summary>
        /// The constructor of the class.
        /// </summary>
        /// <param name="context">The data context of the application.</param>
        public DaoBase(TDbCtx context)
        {
            _context = context;
        }

        public async Task CreateAsync(T newEntry)
        {
            if (await EntryFinderAsync(newEntry.Id) != null)
            {
                throw new ApplicationException($"The {typeof(T).Name} element with the same ID is already in the database");
            }

            await _context.Set<T>().AddAsync(newEntry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T deletee)
        {
            T element = await EntryFinderAsync(deletee.Id);
            if (element != null)
            {
                _context.Remove(deletee);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ApplicationException($"There was an error during deleting the {typeof(T).Name} instance");
            }
        }

        public async Task<T> GetElementAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetElementsAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T updatee)
        {
            T element = await EntryFinderAsync(updatee.Id);
            
            if (element != null)
            {
                //_context.Entry(element).State = EntityState.Detached;
                _context.Update(updatee);
                
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ApplicationException($"An error occured during updating the {typeof(T).Name} instance");
            }
        }

        private async Task<T> EntryFinderAsync(Guid id)
        {
            return (T)await _context.Set<T>().FindAsync(id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
