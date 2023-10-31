using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Repository
{
    public interface IRepository<T, K> where T: class, IEntity<K> 
    {
        /// <summary>
        /// Finds all entities
        /// </summary>
        /// <returns></returns>
        Task<List<T>> FindAll();
        /// <summary>
        /// Finds an entity by Id
        /// </summary>
        /// <param name="id">Primary Key</param>
        /// <returns>returns entity</returns>
        Task<T> FindOneById(K id);

        /// <summary>
        /// Saves the Entity 
        /// </summary>
        /// <param name="entity">Entity </param>
        /// <returns>returns persisted entity</returns>
        Task<T> Save(T entity);


        /// <summary>
        /// Deletes Entity by Id 
        /// </summary>
        /// <param name="id">Primary Key</param>
        /// <returns>returns Entity</returns>
        Task<Boolean> DeleteById(K id);

        /// <summary>
        /// Checks if the entity exists by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Boolean> ExistsById(K id);
    }
}