using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goldfish.Tests
{
	/// <summary>
	/// Integration tests for the tag repository.
	/// </summary>
	[TestClass]
	public class TagTests
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TagTests() {
			App.Init();
		}

		[TestMethod, TestCategory("Repositories")]
		public async Task TagRepository() {
			var id = Guid.Empty;

			// Insert tag
			using (var api = new Api()) {
				var tag = new Models.Tag() {
					Name = "My tag",
				};
				api.Tags.Add(tag);

				Assert.AreEqual(tag.Id.HasValue, true);
				Assert.IsTrue(api.SaveChanges() > 0);

				id = tag.Id.Value;
			}

			// Get by slug
			using (var api = new Api()) {
				var tag = await api.Tags.GetBySlugAsync("my-tag");

				Assert.IsNotNull(tag);
				Assert.AreEqual(tag.Name, "My tag");
			}

			// Update tag
			using (var api = new Api()) {
				var tag = await api.Tags.GetBySlugAsync("my-tag");

				Assert.IsNotNull(tag);

				tag.Name = "My updated tag";
				api.Tags.Add(tag);

				Assert.IsTrue(api.SaveChanges() > 0);
			}

			// Get by id
			using (var api = new Api()) {
				var tag = await api.Tags.GetByIdAsync(id);

				Assert.IsNotNull(tag);
				Assert.AreEqual(tag.Name, "My updated tag");
			}

			// Remove tag
			using (var api = new Api()) {
				var tag = await api.Tags.GetBySlugAsync("my-tag");

				api.Tags.Remove(tag);

				Assert.IsTrue(api.SaveChanges() > 0);
			}
		}
	}
}
