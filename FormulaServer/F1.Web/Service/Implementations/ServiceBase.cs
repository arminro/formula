using F1.Data.Interfaces;
using F1Web.DataAccess.Interfaces;
using F1Web.Service.Inerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1Web.Service.Implementations
{
    /// <summary>
    /// An abstract base class for basic CRUD functionality
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ServiceBase<T> : IFormulaService<T>
        where T : class, IDbEntry
    {
        private readonly IRepository<T> _service;

        /// <summary>
        /// The constructor for the class
        /// </summary>
        /// <param name="repository"></param>
        public ServiceBase(IRepository<T> repository)
        {
            _service = repository;
        }


        /// <summary>
        /// Creates an entry asynchronously
        /// </summary>
        /// <param name="newEntry">The netry to be created</param>
        /// <returns>A task representing the operation.</returns>

        public async Task CreateAsync(T newEntry)
        {
            await _service.CreateAsync(newEntry);
        }

        /// <summary>
        /// Deletes an element asynchronously
        /// </summary>
        /// <param name="deletee">The element to be deleted</param>
        /// <returns>A task representing the operation.</returns>
        public async Task DeleteAsync(T deletee)
        {
            await _service.DeleteAsync(deletee);
        }

        /// <summary>
        /// Retrieves the elements asynchronously.
        /// </summary>
        /// <param name="id">The id of the element</param>
        /// <returns>A Task with the element wrapped inside if found, null otherwise.</returns>
        public async Task<T> GetElementAsync(Guid id)
        {
            return await _service.GetElementAsync(id);
        }

        /// <summary>
        /// Retrieves a list of elements asynchronously.
        /// </summary>
        /// <returns>The list if there are any elements present, an empty enumerable otherwise.</returns>
        public async Task<IEnumerable<T>> GetElementsAsync()
        {
            return await _service.GetElementsAsync();
        }

        /// <summary>
        /// Updates an element asynchronously.
        /// </summary>
        /// <param name="updatee">The element to be udpated.</param>
        /// <returns>A task representing the operation.</returns>
        public async Task UpdateAsync(T updatee)
        {
            await _service.UpdateAsync(updatee);
        }
    }
}
