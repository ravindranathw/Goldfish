using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goldfish.Tests
{
	/// <summary>
	/// Integration tests for the post repository.
	/// </summary>
	[TestClass]
	public class PostTests
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public PostTests() {
			App.Init();
		}

		[TestMethod, TestCategory("Repositories")]
		public async Task PostRepository() {
			var id = Guid.Empty;

			// Insert post
			using (var api = new Api()) {
				var post = new Models.Post() {
					Title = "My post",
					Body = "<p>This is the body</p>"
				};
				post.Categories.Add(new Models.Category() {
					Name = "My category"
				});
				post.Tags.Add(new Models.Tag() {
					Name = "My tag"
				});
				api.Posts.Add(post);

				Assert.IsTrue(post.Id.HasValue);
				Assert.IsTrue(api.SaveChanges() > 0);

				id = post.Id.Value;
			}

			// Get by slug
			using (var api = new Api()) {
				var post = await api.Posts.GetBySlugAsync("my-post");

				Assert.IsNotNull(post);
				Assert.AreEqual(post.Title, "My post");
				Assert.AreEqual(post.Categories.Count, 1);
				Assert.AreEqual(post.Tags.Count, 1);
			}

			// Update post
			using (var api = new Api()) {
				var post = await api.Posts.GetBySlugAsync("my-post");

				Assert.IsNotNull(post);

				post.Title = "My updated post";
				post.Tags.Add(new Models.Tag() {
					Name = "My second tag"
				});
				api.Posts.Add(post);

				Assert.IsTrue(api.SaveChanges() > 0);
			}

			// Get by id
			using (var api = new Api()) {
				var post = await api.Posts.GetByIdAsync(id);

				Assert.IsNotNull(post);
				Assert.AreEqual(post.Title, "My updated post");
				Assert.AreEqual(post.Categories.Count, 1);
				Assert.AreEqual(post.Tags.Count, 2);
			}

			// Remove post
			using (var api = new Api()) {
				var post = await api.Posts.GetBySlugAsync("my-post");

				// Remove categories & tags
				foreach (var c in post.Categories)
					api.Categories.Remove(c);
				foreach (var t in post.Tags)
					api.Tags.Remove(t);

				api.Posts.Remove(post);

				Assert.IsTrue(api.SaveChanges() > 0);
			}
		}
	}
}
