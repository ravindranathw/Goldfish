/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goldfish.Blocks
{
	/// <summary>
	/// The blocks repository.
	/// </summary>
	public class Api : IDisposable
	{
		#region Members
		private Db db;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Api() {
			db = new Db();
		}

		/// <summary>
		/// Gets the block with the given internal id.
		/// </summary>
		/// <param name="internalId">The internal id.</param>
		/// <returns>The block</returns>
		public Block GetByInternalId(string internalId) {
			var block = BlocksModule.GetCache<Block>().Get(internalId);

			if (block == null) {
				block = db.Blocks.Where(b => b.InternalId == internalId).SingleOrDefault();
				if (block != null)
					BlocksModule.GetCache<Block>().Add(block);
			}
			return block;
		}

		/// <summary>
		/// Adds a new or existing block.
		/// </summary>
		/// <param name="block">The block</param>
		public void Add(Block block) {
			var entity = db.Blocks.Where(b => b.Id == block.Id).SingleOrDefault();

			if (entity == null) {
				entity = new Block() {
					Id = Guid.NewGuid()
				};
				db.Blocks.Add(entity);
			}
			entity.InternalId = block.InternalId;
			entity.Name = block.Name;
			entity.Body = block.Body;

			// Remove the current block from the cache.
			BlocksModule.GetCache<Block>().Remove(entity.Id);
		}

		/// <summary>
		/// Removes the block with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		public void Remove(Guid id) {
			var entity = db.Blocks.Where(b => b.Id == id).Single();
			db.Blocks.Remove(entity);

			// Removes the block from the cache
			BlocksModule.GetCache<Block>().Remove(entity.Id);
		}

		/// <summary>
		/// Saves the changes made to the repository.
		/// </summary>
		/// <returns>The number of changes saved</returns>
		public int SaveChanges() {
			return db.SaveChanges();
		}

		/// <summary>
		/// Disposes the repository.
		/// </summary>
		public void Dispose() {
			db.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}