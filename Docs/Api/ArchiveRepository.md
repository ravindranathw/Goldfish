# ArchiveRepository

The archive repository is used to get posts either by date, category or tag. The repository provides read-only access to the posts, however the post repository can be used to store modifications to the posts. The archive repository can be accessed through the API property **Archives**.

## GetArchiveAsync(page = 1, year = null, month = null)

Gets the requested page of the post archive for the specified year and month. All parameters are optional and can be omitted, in which case the first page of the archive is returned. This method is async and requires either the use of the **async** keyword or a **WaitAll** on the returned Task.


	using (var api = new Goldfish.Api()) {
		var posts = await api.Archives.GetArchiveAsync();
	}

## GetCategoryArchiveAsync(slug, page = 1)

Gets the archive for the category with the specified slug. If the optional page is omitted the first page of the archive is returned. This method is async and requires either the use of the **async** keyword or a **WaitAll** on the returned Task.

	using (var api = new Goldfish.Api()) {
		var posts = await api.Archives.GetCategoryArchiveAsync("my-category");
	}

## GetTagArchiveAsync(slug, page = 1)

Gets the archive for the tag with the specified slug. If the optional page is omitted the first page of the archive is returned. This method is async and requires either the use of the **async** keyword or a **WaitAll** on the returned Task.

	using (var api = new Goldfish.Api()) {
		var posts = await api.Archives.GetTagArchiveAsync("my-tag");
	}

