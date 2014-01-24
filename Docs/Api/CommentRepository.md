# CommentRepository

Comments are used to create discussions around posts. The comment repository can be accessed through the API property **Comments**.

## GetById(id)

Gets the comment identified by the given unique id. 

	using (var api = new Goldfish.Api()) {
		var c1 = api.Comments.GetById(id);
		var c2 = await api.Comments.GetByIdAsync(id);
	}

## GetByPostId(id)

Gets the comments available for the post with the given unique id. 

	using (var api = new Goldfish.Api()) {
		var c1 = await api.Comments.GetByPostId(id);
		var c2 = await api.Comments.GetByPostIdAsync(id);
	}
		
## GetAsync(predicate = null)

Gets all of the comments matching the given linq expression predicate. If the predicate is omitted all of the comments are returned. 

	using (var api = new Goldfish.Api()) {
		var c1 = api.Comments.Get(c => c.PostId != id);
		var c2 = await api.Comments.GetAsync(c => c.PostId != id);
	}
	
## Add(comment)

Adds the given comment to the unit of work. If the given comment has been loaded through the repository and has an **Id** the comment in the database will be updated. If the comment is new and has an empty Id a new comment will be created.

	using (var api = new Goldfish.Api()) {
		var comment = new Goldfish.Models.Comment()) {
			PostId = postId,
			Author = "John Doe",
			Email = "john@doe.com",
			Body = "Great post!"
		};
		api.Comments.Add(comment);
		api.SaveChanges();
	}
	
## Remove(comment)

Removes the given comment from the database.

	using (var api = new Goldfish.Api()) {
		api.Comments.Remove(comment);
		api.SaveChanges();
	}

## Remove(id)

Removes the comment with the given id from the database.

	using (var api = new Goldfish.Api()) {
		api.Comments.Remove(id);
		api.SaveChanges();
	}

