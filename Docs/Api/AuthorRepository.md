# AuthorRepository

Authors are used to represent who has written a certain post and can (but doesn't have to) be attached to a user login. The author repository can be accessed through the API property **Authors**.

## GetById(id)

Gets the author identified by the given unique id.

	using (var api = new Goldfish.Api()) {
		var a1 = api.Authors.GetById(id);
		var a2 = await api.Authors.GetByIdAsync(id);
	}
	
## Get(predicate = null)

Gets all of the authors matching the given linq expression predicate. If the predicate is omitted all of the authors are returned. 

	using (var api = new Goldfish.Api()) {
		var a1 = api.Authors.Get(a => a.Name.StartsWith("P"));
		var a2 = await api.Authors.GetAsync(a => a.Name.StartsWith("P"));
	}
	
## Add(author)

Adds the given author to the unit of work. If the given author has been loaded through the repository and has an **Id** the author in the database will be updated. If the author is new and has an empty Id a new author will be created.

	using (var api = new Goldfish.Api()) {
		var author = new Goldfish.Models.Author() {
			Name = "John Doe",
			Email = "john@doe.com"
		};
		api.Authors.Add(author);
		api.SaveChanges();
	}
	
## Remove(author)

Removes the given author from the database.

	using (var api = new Goldfish.Api()) {
		api.Authors.Remove(author);
		api.SaveChanges();
	}

## Remove(id)

Removes the author with the given id from the database.

	using (var api = new Goldfish.Api()) {
		api.Authors.Remove(id);
		api.SaveChanges();
