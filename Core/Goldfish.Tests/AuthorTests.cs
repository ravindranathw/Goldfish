using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goldfish.Tests
{
	/// <summary>
	/// Integration tests for the author repository.
	/// </summary>
	[TestClass]
	public class AuthorTests
	{
		public AuthorTests() {
			App.Init();
		}

		[TestMethod, TestCategory("Repositories")]
		public async Task AuthorRepository() {
			var id = Guid.Empty;

			// Insert author
			using (var api = new Api()) {
				var author = new Models.Author() {
					Name = "John Doe",
					Email = "john@doe.com"
				};
				api.Authors.Add(author);

				Assert.AreEqual(author.Id.HasValue, true);
				Assert.IsTrue(api.SaveChanges() > 0);

				id = author.Id.Value;
			}

			// Get by id
			using (var api = new Api()) {
				var author = await api.Authors.GetByIdAsync(id);

				Assert.IsNotNull(author);
				Assert.AreEqual(author.Name, "John Doe");
			}

			// Update param
			using (var api = new Api()) {
				var author = await api.Authors.GetByIdAsync(id);

				Assert.IsNotNull(author);

				author.Name = "John Moe";
				api.Authors.Add(author);

				Assert.IsTrue(api.SaveChanges() > 0);
			}

			// Get by id
			using (var api = new Api()) {
				var author = await api.Authors.GetByIdAsync(id);

				Assert.IsNotNull(author);
				Assert.AreEqual(author.Name, "John Moe");
			}

			// Remove author
			using (var api = new Api()) {
				api.Authors.Remove(id);

				Assert.IsTrue(api.SaveChanges() > 0);
			}

			// Remove system param
			using (var api = new Api()) {
				var param = api.Params.GetByInternalId("ARCHIVE_PAGE_SIZE");

				try {
					api.Params.Remove(param);

					// The above statement should throw an exception and this
					// this assertion should not be reached.
					Assert.IsTrue(false);
				} catch (Exception ex) {
					Assert.IsTrue(ex is UnauthorizedAccessException);
				}
			}
		}
	}
}
