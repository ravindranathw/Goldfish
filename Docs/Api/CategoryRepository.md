# CategoryRepository

Categories are used to group posts of the same type together. The category repository can be accessed through the API property **Categories**.

## GetById(id)

Gets the category identified by the given unique id. It is cached and should always be prefered over the generic **Get** method. 

	using (var api = new Goldfish.Api()) {
		var c1 = api.Categories.GetById(id);
		var c2 = await api.Categories.GetByIdAsync(id);
	}

## GetBySlug(slug)

Gets the category identified by the given unique slug. It is cached and should always be prefered over the generic **Get** method. 

	using (var api = new Goldfish.Api()) {
		var c1 = api.Categories.GetBySlug("my-category");
		var c2 = await api.Categories.GetBySlugAsync("my-category");
	}

## Get(predicate = null)

Gets all of the categories matching the given linq expression predicate. If the predicate is omitted all of the categories are returned. 

	using (var api = new Goldfish.Api()) {
		var c1 = api.Categories.Get(c => c.Name.StartsWith("Dev"));
		var c2 = await api.Categories.GetAsync(c => c.Name.StartsWith("Dev"));
	}

## Add(category)

Adds the given category to the unit of work. If the given category has been loaded through the repository and has an **Id** the category in the database will be updated. If the category is new and has an empty Id a new category will be created.

	using (var api = new Goldfish.Api()) {
		var category = new Goldfish.Models.Category() {
			Name = "Development"
		};
		api.Categories.Add(category);
		api.SaveChanges();
	}

## Remove(category)

Removes the given category from the database.

	using (var api = new Goldfish.Api()) {
		api.Categories.Remove(category);
		api.SaveChanges();
	}

## Remove(id)

Removes the category with the given id from the database.

	using (var api = new Goldfish.Api()) {
		api.Categories.Remove(id);
		api.SaveChanges();
	}
