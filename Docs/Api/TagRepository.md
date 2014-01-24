# TagRepository

Tags are used to group post together regarding their content. The tag repository can be accessed through the API property **Tags**.

## GetById(id)

Gets the tag identified by the given unique id. It is cached and should always be prefered over the generic **Get** method.

	using (var api = new Goldfish.Api()) {
		var t1 = api.Tags.GetById(id);
		var t2 = await api.Tags.GetByIdAsync(id);
	}

## GetBySlug(slug)

Gets the tag identified by the given unique slug. It is cached and should always be prefered over the generic **Get** method.

	using (var api = new Goldfish.Api()) {
		var t1 = api.Tags.GetBySlug("my-tag");
		var t2 = await api.Tags.GetBySlugAsync("my-tag");
	}

## Get(predicate = null)

Gets all of the tags matching the given linq expression predicate. If the predicate is omitted all of the tags are returned. 

	using (var api = new Goldfish.Api()) {
		var t1 = api.Tags.Get(t => t.Name.StartsWith("C"));
		var t2 = await api.Tags.GetAsync(t => t.Name.StartsWith("C"));
	}

## Add(tag)

Adds the given tag to the unit of work. If the given tag has been loaded through the repository and has an **Id** the tag in the database will be updated. If the tag is new and has an empty Id a new tag will be created.

	using (var api = new Goldfish.Api()) {
		var tag = new Goldfish.Models.Tag()) {
			Name = "C#"
		};
		api.Tags.Add(tag);
		api.SaveChanges();
	}

## Remove(tag)

Removes the given tag from the database.

	using (var api = new Goldfish.Api()) {
		api.Tags.Remove(tag);
		api.SaveChanges();
	}

## Remove(id)

Removes the tag with the given id from the database.

	using (var api = new Goldfish.Api()) {
		api.Tags.Remove(id);
		api.SaveChanges();
	}

