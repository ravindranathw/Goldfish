# ParamRepository

Params are used to store configuration values for the core application as well as modules and plugins or other custom functionality. Note that all core configuration can (and should) be accessed through the static class **Goldfish.Config**. The param repository can be accessed through the API property **Params**.

## GetById(id)

Gets the param identified by the given unique id. It is cached and should always be prefered over the generic **Get** method.

	using (var api = new Goldfish.Api()) {
		var p1 = api.Params.GetById(id);
		var p2 = await api.Params.GetByIdAsync(id);
	}

## GetByInternalId(internalId)

Gets the param identified by the given unique internal id. It is cached and should always be prefered over the generic **Get** method.

	using (var api = new Goldfish.Api()) {
		var p1 = api.Params.GetByInternalId("MY_PARAM");
		var p2 = await api.Params.GetByInternalIdAsync("MY_PARAM");
	}

## Get(predicate = null)

Gets all of the params matching the given linq expression predicate. If the predicate is omitted all of the params are returned. 

	using (var api = new Goldfish.Api()) {
		var params = api.Params.Get(p => p.InternalId.StartsWith("CACHE"));
	}

## Add(param)

Adds the given param to the unit of work. If the given param has been loaded through the repository and has an **Id** the param in the database will be updated. If the param is new and has an empty Id a new param will be created.

	using (var api = new Goldfish.Api()) {
		var param = new Goldfish.Models.Param() {
			Name = "My param",
			InternalId = "MY_PARAM"
		};
		api.Params.Add(param);
		api.SaveChanges();
	}

## Remove(param)

Removes the given param from the database.

	using (var api = new Goldfish.Api()) {
		api.Params.Remove(params);
		api.SaveChanges();
	}

## Remove(id)

Removes the param with the given id from the database.

	using (var api = new Goldfish.Api()) {
		api.Params.Remove(id);
		api.SaveChanges();
	}

