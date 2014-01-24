using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Goldfish.Repositories
{
	public interface IParamRepository
	{
		/// <summary>
		/// Gets the param model with the given id. The results of this method
		/// is cached in memory resulting in higher performance than the generic
		/// get method.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		Models.Param GetById(Guid id);

		/// <summary>
		/// Gets the param model with the given id. The results of this method
		/// is cached in memory resulting in higher performance than the generic
		/// get method.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		Task<Models.Param> GetByIdAsync(Guid id);

		/// <summary>
		/// Gets the param model with the given internal id. The results of this method
		/// is cached in memory resulting in higher performance than the generic
		/// get method.
		/// </summary>
		/// <param name="internalId">The unique internal id</param>
		/// <returns>The model</returns>
		Models.Param GetByInternalId(string internalId);

		/// <summary>
		/// Gets the param model with the given internal id. The results of this method
		/// is cached in memory resulting in higher performance than the generic
		/// get method.
		/// </summary>
		/// <param name="internalId">The unique internal id</param>
		/// <returns>The model</returns>
		Task<Models.Param> GetByInternalIdAsync(string internalId);

		/// <summary>
		/// Gets the patam models matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The models</returns>
		IList<Models.Param> Get(Expression<Func<Entities.Param, bool>> predicate = null);

		/// <summary>
		/// Gets the patam models matching the given predicate.
		/// </summary>
		/// <param name="predicate">The predicate</param>
		/// <returns>The models</returns>
		Task<IList<Models.Param>> GetAsync(Expression<Func<Entities.Param, bool>> predicate = null);

		/// <summary>
		/// Adds the given param.
		/// </summary>
		/// <param name="model">The param</param>
		void Add(Models.Param model);

		/// <summary>
		/// Removes the given param.
		/// </summary>
		/// <param name="model">The model</param>
		void Remove(Models.Param model);
	}
}
