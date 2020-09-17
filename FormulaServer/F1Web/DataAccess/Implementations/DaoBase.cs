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

        /// <summary>
        /// Creates an entry asynchronously
        /// </summary>
        /// <param name="newEntry">The netry to be created</param>
        /// <returns>A task representing the operation.</returns>
        public async Task CreateAsync(T newEntry)
        {
            if (await EntryFinderAsync(newEntry.Id) != null)
            {
                throw new ApplicationException($"The {typeof(T).Name} element with the same ID is already in the database");
            }

            await _context.Set<T>().AddAsync(newEntry);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an element asynchronously
        /// </summary>
        /// <param name="deletee">The element to be deleted</param>
        /// <returns>A task representing the operation.</returns>
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

        /// <summary>
        /// Retrieves the elements asynchronously.
        /// </summary>
        /// <param name="id">The id of the element</param>
        /// <returns>A Task with the element wrapped inside if found, null otherwise.</returns>
        public async Task<T> GetElementAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Retrieves a list of elements asynchronously.
        /// </summary>
        /// <returns>The list if there are any elements present, an empty enumerable otherwise.</returns>
        public async Task<IEnumerable<T>> GetElementsAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Updates an element asynchronously.
        /// </summary>
        /// <param name="updatee">The element to be udpated.</param>
        /// <returns>A task representing the operation.</returns>
        public async Task UpdateAsync(T updatee)
        {
            T element = await EntryFinderAsync(updatee.Id);
            
            if (element != null)
            {
                // either this or AsNoTracking() would work in real-life scenarios
                //_context.Entry(element).State = EntityState.Detached;
                //_context.Update(updatee);

                this.ExchangeProperties(element, updatee);
                //_context.Entry(element).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ApplicationException($"An error occured during updating the {typeof(T).Name} instance");
            }
        }

        private async Task<T> EntryFinderAsync(Guid id)
        {
            return (T)await _context.Set<T>()
                //.AsNoTracking()
                .FirstOrDefaultAsync(en => en.Id == id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private void ExchangeProperties(T oldEntity, T newEntity)
        {
            /* Neither using AsNoTracking() nor manually detaching the entity prevented EF to keep track of the data
             probably due to the dbcontext and the sqlite connection being retained in the memory.
            In real life scenarios, you would rather have a persitant db or use inmemory SqlLite in an inline using
            during tests, which both would work. In this scenario, only adding the values one by one utilizing EF tracking
            worked.*/

            foreach (var prop in oldEntity.GetType().GetProperties())
            {
                var newValue = newEntity.GetType()
                    .GetProperty(prop.Name)
                    .GetValue(newEntity);
                prop.SetValue(oldEntity, newValue);
            }
        }
    }
}
