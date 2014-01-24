# API

All interaction with the core entities in Goldfish is handled through the Api and it's included repositories.

# 1. Creating a new API

The API is easily created with the following block of code. Note that the API is disposable and should be created with a **using** statement.

	using (var api = new Goldfish.Api()) {
		// Do your magic here
	}
	
The API is a container for all of the application repositories, but also holds the unit of work for the current instance. This means that no actions made through the API will be persisted to the database before **SaveChanges** has been called on the instance.

	using (var api = new Goldfish.Api()) {
		// Do your magic here
		
		api.SaveChanges();
	}

All get operations in the repositories are available in both asynchronous and synchronous versions.
	
# 2. The available repositories

The API has the following repositories alphabetical order. The repositories are described in their own files.

* ArchiveRepository
* AuthorRepository
* CategoryRepository
* CommentRepository
* ParamRepository
* PostRepository
* TagRepository

# 3. Replacing the API

Goldfish uses an **TinyIoC** internally for resolving all references to the API. This means that the default implementation using Entity Framework can be replaced with a mocking API for unit testing or another custom API implementation.

In order to change how refrences to the API is resolved implement the hook **Init.RegisterApi** with the class you want to register. Remember to set the repository to **AsMultiInstance** if a new instance of the API should be created each time it's resolved.

	Goldfish.Hooks.Init.RegisterApi = (container) => {
	  container.Register<Goldfish.IApi, MyApi>().AsMultiInstance();
	}