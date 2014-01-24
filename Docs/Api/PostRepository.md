# PostRepository

Posts are the main entity used to write content with. The category repository can be accessed through the API property **Posts**.

## GetById(id)

Gets the post identified by the given unique id. It is cached and should always be prefered over the generic **Get** method. 

	using (var api = new Goldfish.Api()) {
		var p1 = api.Posts.GetById(id);
		var p2 = await api.Posts.GetByIdAsync(id);
	}

## GetBySlug(slug)

Gets the post identified by the given unique slug. It is cached and should always be prefered over the generic **Get** method.

	using (var api = new Goldfish.Api()) {
		var p1 = api.Posts.GetBySlug("my-first-post");
		var p2 = await api.Posts.GetBySlugAsync("my-first-post");
	}

## Get(predicate = null)

Gets all of the posts matching the given linq expression predicate. If the predicate is omitted all of the posts are returned. 

	using (var api = new Goldfish.Api()) {
		var p1 = api.Posts.Get(p => p.Title.StartsWith("My"));
		var p2 = await api.Posts.GetAsync(p => p.Title.StartsWith("My"));
	}

## Add(post)

Adds the given post to the unit of work. If the given post has been loaded through the repository and has an **Id** the post in the database will be updated. If the post is new and has an empty Id a new post will be created.

	using (var api = new Goldfish.Api()) {
		var post = new Goldfish.Models.Post() {
			Title = "My first post",
			Author = author,
			Body = "This is my first post!"
		};
		api.Posts.Add(post);
		api.SaveChanges();
	}

## Publish(id, date = null)

Publishes the post with the given unique id. If the optional date parameter is omitted the post is published instantly.

	using (var api = new Goldfish.Api()) {
		api.Posts.Publish(id);
		api.SaveChanges();
	}

## Remove(post)

Removes the given post from the database.

	using (var api = new Goldfish.Api()) {
		api.Posts.Remove(category);
		api.SaveChanges();
	}

## Remove(id)

Removes the post with the given id from the database.

	using (var api = new Goldfish.Api()) {
		api.Posts.Remove(id);
		api.SaveChanges();
	}

