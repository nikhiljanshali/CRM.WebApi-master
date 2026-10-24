using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRM.Core.FrameworkCore {
    /// <summary>
    /// This is generic contract that defines most generic behavior
    /// </summary>
    public interface IRepository {

        #region Insert

        /// <summary>
        /// Insert single document 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>Id of the generated document</returns>
        Task<string> Insert<T>(T entity) where T : IEntity;

        /// <summary>
        /// Batch Insert; suitable for a batch of 100 or less docs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns>List of IDs of the generated documents</returns>
        Task<IList<string>> Insert<T>(IList<T> entities) where T : IEntity;

        /// <summary>
        /// Bulk Insert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns>List of IDs of the generated document</returns>
        Task<IList<string>> BulkInsert<T>(IList<T> entities) where T : IEntity;

        #endregion

        #region Get

        /// <summary>
        /// Get one document by Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns>T</returns>
        Task<T> GetOne<T>(string id) where T : IEntity;

        /// <summary>
        /// Find one by Predicate
        /// </summary>
        /// <typeparam name="T">Target class</typeparam>
        /// <param name="predicate">predicate for select some field likr (x => new { X = x.X, Y = x.Y })</param>
        /// <returns>T</returns>
        Task<T> GetOne<T>(Expression<Func<T, bool>> predicate) where T : IEntity;

        /// <summary>
        /// Find one By predicate
        /// also use predicate for select some field likr (x => new { X = x.X, Y = x.Y })
        /// </summary>
        /// <typeparam name="TResult">Selector class</typeparam>
        /// <typeparam name="T">Target Class</typeparam>
        /// <param name="predicate"></param>
        /// <param name="project">predicate for select some field likr (x => new { X = x.X, Y = x.Y })</param>
        /// <returns>TResult</returns>
        Task<TResult> GetOne<T, TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> project)
            where T : IEntity
            where TResult : class;

        /// <summary>
        /// Get many documents
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="predicate">predicate</param>
        /// <param name="take">"-1" here would actually default of 256 records</param>
        /// <param name="skip">skip count</param>
        /// <returns>IList<T></returns>
        Task<IList<T>> GetMany<T>(Expression<Func<T, bool>> predicate = null, int take = -1, int skip = 0) where T : IEntity;

        /// <summary>
        /// Get many documents
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="project"></param>
        /// <returns>TResult</returns>
        Task<IList<TResult>> GetMany<T, TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> project, int take = -1, int skip = 0)
            where T : IEntity
            where TResult : class;

        #endregion

        #region Update

        /// <summary>
        /// Updating a document. Please note that the version number is mandatory to be passed.
        /// Check the implemention in BizMongoRepository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>True|False</returns>
        Task<bool> Update<T>(T entity) where T : IEntity;

        #endregion

        #region Delete

        /// <summary>
        /// Delete by Ids
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns>True|False</returns>
        Task<bool> Delete<T>(string id) where T : IEntity;

        /// <summary>
        /// Delete by Ids for a smaller batch size; 100 or so.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns>True|False</returns>
        Task<bool> Delete<T>(IList<string> ids) where T : IEntity;

        #endregion

        #region Count

        /// <summary>
        /// Returns the count of records in a collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">Optinal value is null.</param>
        /// <returns>Count of Documents</returns>
        Task<int> GetCount<T>(Expression<Func<T, bool>> predicate = null) where T : IEntity;

        #endregion

    }
}