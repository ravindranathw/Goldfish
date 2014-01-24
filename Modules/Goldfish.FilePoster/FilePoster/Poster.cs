using System;
using System.IO;
using System.Text;

namespace Goldfish.FilePoster
{
	/// <summary>
	/// Main class for the file poster.
	/// </summary>
	public static class Poster
	{
		#region Inner classes
		/// <summary>
		/// The data that could be available in the uploaded file.
		/// </summary>
		internal class PostData
		{
			#region Members
			/// <summary>
			/// The main content of the post.
			/// </summary>
			public string Content = "";

			/// <summary>
			/// The post categories.
			/// </summary>
			public string[] Categories = new string[0];

			/// <summary>
			/// The post tags.
			/// </summary>
			public string[] Tags = new string [0];

			/// <summary>
			/// The meta keywords.
			/// </summary>
			public string Keywords = "";

			/// <summary>
			/// The meta description.
			/// </summary>
			public string Description = "";

			/// <summary>
			/// The optional publish date.
			/// </summary>
			public DateTime? Publish;
			#endregion
		}
		#endregion

		/// <summary>
		///  Creates and publishes a new post
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		public static void FileCreated(object sender, FileSystemEventArgs e) {
			using (var reader = new StreamReader(e.FullPath)) {
				var data = GetPost(reader.ReadToEnd());
				var name = GetTitle(e.Name);

				using (var api = App.Instance.IoCContainer.Resolve<IApi>()) {
					// Create pos
					var post = new Models.Post() {
						Title = name,
						Keywords = data.Keywords,
						Description = data.Description,
						Body = data.Content
					};

					AddCategories(api, post, data.Categories);
					AddTags(api, post, data.Tags);

					api.Posts.Add(post);
					api.SaveChanges();

					api.Posts.Publish(post.Id.Value, data.Publish);
					api.SaveChanges();
				}
			}
		}

		/// <summary>
		/// Called when a file in the upload folder is changed.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		public static void FileChanged(object sender, FileSystemEventArgs e) {
			using (var reader = new StreamReader(e.FullPath)) {
				var data = GetPost(reader.ReadToEnd());
				var name = GetTitle(e.Name);
				var isnew = false;

				using (var api = App.Instance.IoCContainer.Resolve<IApi>()) {
					var post = api.Posts.GetBySlug(Utils.GenerateSlug(name));

					if (post == null) {
						post = new Models.Post() {
							Title = name
						};
						isnew = true;
					}
					post.Keywords = data.Keywords;
					post.Description = data.Description;
					post.Body = data.Content;

					AddCategories(api, post, data.Categories);
					AddTags(api, post, data.Tags);

					api.Posts.Add(post);
					api.SaveChanges();

					if (isnew) {
						api.Posts.Publish(post.Id.Value, data.Publish);
						api.SaveChanges();
					}
				}
			}
		}

		/// <summary>
		/// Called when a file in the upload folder is renamed.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		public static void FileRenamed(object sender, RenamedEventArgs e) {
			var oldname = GetTitle(e.OldName);
			var name = GetTitle(e.Name);

			using (var api = App.Instance.IoCContainer.Resolve<IApi>()) {
				var post = api.Posts.GetBySlug(Utils.GenerateSlug(oldname));
				if (post != null) {
					post.Title = name;
					api.Posts.Add(post);
					api.SaveChanges();
				}
			}
		}

		/// <summary>
		/// Called when a file in the upload folder is deleted.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		public static void FileDeleted(object sender, FileSystemEventArgs e) {
			var name = GetTitle(e.Name);

			using (var api = App.Instance.IoCContainer.Resolve<IApi>()) {
				var post = api.Posts.GetBySlug(Utils.GenerateSlug(name));
				if (post != null) {
					api.Posts.Remove(post);
					api.SaveChanges();
				}
			}
		}

		#region Private methods
		/// <summary>
		/// Gets the post title from the given filename.
		/// </summary>
		/// <param name="filename">The filename</param>
		/// <returns>The post title</returns>
		private static string GetTitle(string filename) { 
			return filename.Substring(0, filename.LastIndexOf('.'));
		}

		/// <summary>
		/// Gets the post data from the given file body.
		/// </summary>
		/// <param name="body">The body</param>
		/// <returns>The post data</returns>
		private static PostData GetPost(string body) {
			var post = new PostData();
			var rows = body.Split(new char[] { '\n' });
			var pos = 0;

			for (var n = 0; n < Math.Min(6, rows.Length); n++) { 
				var segments = rows[n].Split(new char[] { ':' });
				if (segments.Length > 1) {
					var param = segments[0].Trim().ToLower();
					var value = segments[1].Trim();

					if (param == "categories") {
						post.Categories = value.Split(new char[] { ',' });
						for (var i = 0; i < post.Categories.Length; i++)
							post.Categories[i] = post.Categories[i].Trim();
						pos++;
					} else if (param == "tags") {
						post.Tags = value.Split(new char[] { ',' });
						for (var i = 0; i < post.Tags.Length; i++)
							post.Tags[i] = post.Tags[i].Trim();
						pos++;
					} else if (param == "publish") {
						try {
							post.Publish = DateTime.Parse(value);
						} catch { }
						pos++;
					} else if (param == "keywords") {
						post.Keywords = value;
						pos++;
					} else if (param == "description") {
						post.Description = value;
						pos++;
					}
				}
			}

			if (pos > 0) {
				var sb = new StringBuilder();
				for (var n = pos; n < rows.Length; n++)
					sb.Append(rows[n] + "\n");
				post.Content = sb.ToString().Trim();

			} else post.Content = body;

			return post;
		}

		/// <summary>
		/// Adds the categories to the given post.
		/// </summary>
		/// <param name="api">The current api</param>
		/// <param name="post">The post</param>
		/// <param name="categories">The categories</param>
		private static void AddCategories(IApi api, Models.Post post, string[] categories) {
			post.Categories.Clear();

			// Add categories
			foreach (var cat in categories) {
				var c = api.Categories.GetBySlug(Utils.GenerateSlug(cat));
				if (c == null)
					c = new Models.Category() { 
						Name = cat
					};
				post.Categories.Add(c);
			}
		}

		/// <summary>
		/// Adds the tags to the given post.
		/// </summary>
		/// <param name="api">The current api</param>
		/// <param name="post">The post</param>
		/// <param name="tags">The tags</param>
		private static void AddTags(IApi api, Models.Post post, string[] tags) {
			post.Tags.Clear();

			foreach (var tag in tags) {
				var t = api.Tags.GetBySlug(Utils.GenerateSlug(tag));
				if (t == null)
					t = new Models.Tag() { 
						Name = tag
					};
				post.Tags.Add(t);
			}
		}
		#endregion
	}
}