/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goldfish.Tests
{
	/// <summary>
	/// Integration tests for the category repository.
	/// </summary>
	[TestClass]
	public class CategoryTests
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CategoryTests() {
			App.Init();
		}

		[TestMethod, TestCategory("Repositories")]
		public async Task CategoryRepository() {
			var id = Guid.Empty;

			// Insert category
			using (var api = new Api()) {
				var cat = new Models.Category() {
					Name = "My category",
				};
				api.Categories.Add(cat);

				Assert.IsTrue(cat.Id.HasValue);
				Assert.IsTrue(api.SaveChanges() > 0);

				id = cat.Id.Value;
			}

			// Get by slug
			using (var api = new Api()) {
				var cat = await api.Categories.GetBySlugAsync("my-category");

				Assert.IsNotNull(cat);
				Assert.AreEqual(cat.Name, "My category");
			}

			// Update category
			using (var api = new Api()) {
				var cat = await api.Categories.GetBySlugAsync("my-category");

				Assert.IsNotNull(cat);

				cat.Name = "My updated category";
				api.Categories.Add(cat);

				Assert.IsTrue(api.SaveChanges() > 0);
			}

			// Get by id
			using (var api = new Api()) {
				var cat = await api.Categories.GetByIdAsync(id);

				Assert.IsNotNull(cat);
				Assert.AreEqual(cat.Name, "My updated category");
			}

			// Remove category
			using (var api = new Api()) {
				var cat = await api.Categories.GetBySlugAsync("my-category");

				api.Categories.Remove(cat);

				Assert.IsTrue(api.SaveChanges() > 0);
			}
		}
	}
}
